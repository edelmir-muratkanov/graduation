import type { OnModuleInit } from '@nestjs/common'
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
export class CalculationsService implements OnModuleInit {
	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async onModuleInit() {
		const calcs = await this.prisma.calculation.count()

		if (calcs === 0) {
			const projects = await this.prisma.projects.findMany()

			await Promise.all(
				projects.map(project =>
					this.handleProjectCreatedEvent(new ProjectCreatedEvent(project.id)),
				),
			)
		}
	}

	async getByProject(projectId: string) {
		return this.prisma
			.$extends({
				result: {
					calculation: {
						applicability: {
							needs: { ratio: true },
							compute: calculation => this.getApplicability(calculation.ratio),
						},
					},
				},
			})
			.calculation.findMany({
				where: {
					projectId,
				},
				select: {
					ratio: true,
					applicability: true,
					method: {
						select: {
							id: true,
							name: true,
						},
					},
					items: {
						select: {
							collectorType: true,
							ratio: true,
							property: {
								select: {
									name: true,
								},
							},
						},
					},
				},
			})
	}

	async getByMethod(methodId: string) {
		return this.prisma
			.$extends({
				result: {
					calculation: {
						applicability: {
							needs: { ratio: true },
							compute: calculation => this.getApplicability(calculation.ratio),
						},
					},
				},
			})
			.calculation.findMany({
				where: {
					methodId,
				},
				select: {
					ratio: true,
					applicability: true,
					method: {
						select: {
							id: true,
							name: true,
						},
					},
					items: {
						select: {
							collectorType: true,
							ratio: true,
							property: {
								select: {
									name: true,
								},
							},
						},
					},
				},
			})
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
						propertyId: property.id,
					}
				}),
			)

			paramsRatios.push({
				ratio: method.collectorTypes.includes(project.collectorType) ? 1 : -1,
				collectorType: project.collectorType,
			})

			const totalRatio = this.calculateTotalRatio(paramsRatios)

			const calculation = await this.prisma.calculation.create({
				data: {
					ratio: totalRatio,
					methodId: method.id,
					projectId,
				},
			})

			paramsRatios.map(async ({ ratio, propertyId, collectorType }) => {
				await this.prisma.calculationItem.create({
					data: {
						calculationId: calculation.id,
						ratio,
						collectorType,
						propertyId,
					},
				})
			})
		})
	}

	private getApplicability(totalRatio: number) {
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
