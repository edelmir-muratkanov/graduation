import { Injectable } from '@nestjs/common'
import type { CollectorType } from '@prisma/client'
import { I18nContext, I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { PrismaService } from 'src/shared/prisma'

import type { Group, MethodParams } from './calculations.interface'

@Injectable()
export class CalculationsService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

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

	async calculate(projectId: string, methodId: string) {
		const projectParams = await this.prisma.projectsProperties.findMany({
			where: {
				projectId,
			},
			select: {
				propertyId: true,
				value: true,
			},
		})

		const { collectorTypes: methodCollectorTypes } =
			await this.prisma.methods.findUnique({
				where: { id: methodId },
				select: {
					collectorTypes: true,
				},
			})

		const { collectorType: projectCollectorType } =
			await this.prisma.projects.findUnique({
				where: { id: projectId },
				select: { collectorType: true },
			})

		const methodParams = await this.prisma.methodsProperties.findMany({
			where: {
				methodId,
			},
			select: {
				property: {
					select: {
						id: true,
						name: true,
					},
				},
				parameters: true,
			},
		})

		const paramsRatios: {
			ratio: number
			propertyId?: string
			collectorType?: CollectorType
		}[] = await Promise.all(
			methodParams.map(({ parameters, property }) => {
				const projectParam = projectParams.find(
					p => p.propertyId === property.id,
				)
				const ratio = this.calculateParamRatio(
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
			ratio: methodCollectorTypes.includes(projectCollectorType) ? 1 : -1,
			collectorType: projectCollectorType,
		})

		const totalRatio = this.calculateTotalRatio(paramsRatios)

		const calculation = await this.prisma.calculation.create({
			data: {
				ratio: totalRatio,
				methodId,
				projectId,
			},
		})

		await Promise.all(
			paramsRatios.map(({ ratio, propertyId, collectorType }) => {
				return this.prisma.calculationItem.create({
					data: {
						calculationId: calculation.id,
						ratio,
						collectorType,
						propertyId,
					},
				})
			}),
		)

		return calculation.id
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
