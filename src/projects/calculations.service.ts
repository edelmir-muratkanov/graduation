import { Injectable } from '@nestjs/common'
import { I18nContext, I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { PrismaService } from 'src/shared/prisma'

import type { GetParams, Group } from './projects.interface'
import { ProjectsService } from './projects.service'

@Injectable()
export class CalculationsService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly projectsService: ProjectsService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async calculate(projectId: string) {
		const project = await this.projectsService.getById(projectId)

		const res = await Promise.all(
			project.methods.map(async method => {
				const paramsRatios = await Promise.all(
					method.parameters.map(async methodParam => {
						const { name } = await this.prisma.properties.findUnique({
							where: { id: methodParam.propertyId },
							select: {
								name: true,
							},
						})
						const projectParam = project.parameters.filter(
							p => p.property.id === methodParam.propertyId,
						)[0]

						const ratio = this.calculateParamRatio({
							methodParams: methodParam.parameters,
							paramName: name,
							projectParams: projectParam ? projectParam.value : null,
						})

						return {
							ratio,
							name,
						}
					}),
				)

				paramsRatios.push({
					name: 'Collector Type',
					ratio: method.collectorType.includes(project.collectorType) ? 1 : -1,
				})

				const totalRatio = this.calculateTotalRatio(paramsRatios)

				return {
					name: method.name,
					result: this.getResult(totalRatio),
					totalRatio,
					paramsRatios,
				}
			}),
		)

		return res
	}

	getResult(totalRatio: number) {
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

	calculateParamRatio(param: GetParams): number {
		const { methodParams, projectParams } = param

		if (projectParams === null) {
			return -1
		}

		const { first, second } = methodParams

		const calculateHelper = (params: Group, index: number) => {
			if (projectParams <= params.xMin) {
				return -1
			}

			if (projectParams <= params.xMax) {
				return (
					(1 +
						((params.xMax - params.x) / (params.x - params.xMin)) ** 2 *
							((projectParams - params.xMin) / (params.xMax - projectParams)) **
								2 *
							(-1) ** index) *
					-1
				)
			}

			return 1
		}

		if (first && second) {
			if (projectParams <= first.xMin) {
				return -1
			}

			if (projectParams <= first.xMax) {
				return calculateHelper(first, 1)
			}

			if (projectParams <= second.xMin) {
				return 1
			}

			if (projectParams <= second.xMax) {
				return calculateHelper(second, 2)
			}

			return -1
		}

		if (first) {
			if (projectParams <= first.xMin) {
				return -1
			}

			if (projectParams <= first.xMax) {
				return calculateHelper(first, 1)
			}

			return 1
		}

		if (second) {
			if (projectParams <= second.xMin) return -1

			if (projectParams <= second.xMax) {
				return calculateHelper(second, 2)
			}

			return 1
		}
	}

	calculateTotalRatio(ratios: { ratio: number; name: string }[]): number {
		return (
			(1 / ratios.length) *
			ratios.reduce((sum, paramsRatio) => sum + paramsRatio.ratio, 0)
		)
	}
}
