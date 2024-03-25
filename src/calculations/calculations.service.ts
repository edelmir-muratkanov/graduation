import { Injectable } from '@nestjs/common'
import { OnEvent } from '@nestjs/event-emitter'
import type { CollectorType } from '@prisma/client'
import { I18nContext, I18nService } from 'nestjs-i18n'
import { ProjectCreatedEvent } from 'src/projects/project-created.event'
import type { Group } from 'src/projects/projects.interface'
import type { I18nTranslations } from 'src/shared/generated'
import { PrismaService } from 'src/shared/prisma'

import type { MethodParams } from './calculations.interface'

@Injectable()
export class CalculationsService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async getByProject(projectId: string) {
		return this.prisma.calculation.findMany({
			where: {
				projectId,
			},
			include: {
				items: {
					select: {
						collectorType: true,
						property: {
							select: {
								name: true,
							},
						},
						ratio: true,
					},
				},
			},
		})
	}

	async getByMethod(methodId: string) {
		const calculations = await this.prisma.calculation.findMany({
			where: {
				methodId,
			},
			include: {
				items: {
					select: {
						collectorType: true,
						property: {
							select: {
								name: true,
							},
						},
						ratio: true,
					},
				},
			},
		})

		const res = calculations.map(c => ({
			...c,
			result: this.getResult(c.ratio),
		}))

		return res
	}

	@OnEvent('project.created')
	private async handleProjectCreatedEvent({ projectId }: ProjectCreatedEvent) {
		const project = await this.prisma.projects.findUnique({
			where: { id: projectId },
			include: {
				parameters: {
					select: {
						propertyId: true,
						value: true,
					},
				},
				methods: {
					include: {
						method: {
							include: {
								parameters: {
									include: {
										property: true,
									},
								},
							},
						},
					},
				},
			},
		})

		const projectParameters = project.parameters
		const methods = project.methods.map(m => m.method)

		methods.map(async method => {
			const paramsRatios: {
				methodId: string
				ratio: number
				propertyId?: string
				collectorType?: CollectorType
			}[] = await Promise.all(
				method.parameters.map(async ({ parameters, property }) => {
					const projectParam = projectParameters.find(
						p => p.propertyId === property.id,
					)

					const ratio = await this.calculateParamRatio(
						projectParam ? projectParam.value : null,
						parameters as MethodParams,
					)

					return {
						ratio,
						methodId: method.id,
						propertyId: property.id,
					}
				}),
			)

			paramsRatios.push({
				methodId: method.id,
				ratio: method.collectorTypes.includes(project.collectorType) ? 1 : -1,
				collectorType: project.collectorType,
			})

			const totalRatio = this.calculateTotalRatio(paramsRatios)

			await this.prisma.calculation.create({
				data: {
					ratio: totalRatio,
					methodId: method.id,
					projectId,
					items: {
						createMany: {
							data: paramsRatios.map(p => ({
								ratio: p.ratio,
								collectorType: p.collectorType,
								propertyId: p.propertyId,
							})),
							skipDuplicates: true,
						},
					},
				},
			})
		})
	}

	private getResult(totalRatio: number) {
		const { lang } = I18nContext.current()
		if (totalRatio < -0.75)
			return this.i18n.t('results.calculations.NotApplicable', { lang })

		if (totalRatio < -0.25)
			return this.i18n.t('results.calculations.NotSuitable', { lang })

		if (totalRatio < 0.25)
			return this.i18n.t('results.calculations.LowEffieciency', { lang })

		if (totalRatio < 0.75)
			return this.i18n.t('results.calculations.Applicable', { lang })

		return this.i18n.t('results.calculations.Favorable', { lang })
	}

	private calculateTotalRatio(
		ratios: {
			ratio: number
		}[],
	) {
		return (
			(1 / ratios.length) *
			ratios.reduce((sum, paramsRatio) => sum + paramsRatio.ratio, 0)
		)
	}

	private calculateParamRatio(
		projectParam: number | null,
		methodParams: { first?: Group; second?: Group },
	) {
		if (projectParam === null) {
			return -1
		}

		const { first, second } = methodParams

		const calculateHelper = (params: Group, index: number) => {
			if (projectParam <= params.xMin) {
				return -1
			}

			if (projectParam <= params.xMax) {
				return (
					(1 +
						((params.xMax - params.x) / (params.x - params.xMin)) ** 2 *
							((projectParam - params.xMin) / (params.xMax - projectParam)) **
								2 *
							(-1) ** index) *
					-1
				)
			}

			return 1
		}

		if (first && second) {
			if (projectParam <= first.xMin) {
				return -1
			}

			if (projectParam <= first.xMax) {
				return calculateHelper(first, 1)
			}

			if (projectParam <= second.xMin) {
				return 1
			}

			if (projectParam <= second.xMax) {
				return calculateHelper(second, 2)
			}

			return -1
		}

		if (first) {
			if (projectParam <= first.xMin) {
				return -1
			}

			if (projectParam <= first.xMax) {
				return calculateHelper(first, 1)
			}

			return 1
		}

		if (second) {
			if (projectParam <= second.xMin) return -1

			if (projectParam <= second.xMax) {
				return calculateHelper(second, 2)
			}

			return 1
		}
	}
}
