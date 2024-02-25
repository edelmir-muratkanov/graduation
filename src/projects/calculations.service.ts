import { Injectable } from '@nestjs/common'
import { PrismaService } from 'src/shared/prisma'

import { ProjectsService } from './projects.service'

type Group = {
	x: number
	xMin: number
	xMax: number
}

type GetParamsFnRes = {
	paramname: string
	methodparams: { values: number[] } | { first?: Group; second?: Group }
	projectparams: number
}

@Injectable()
export class CalculationsService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly projectsService: ProjectsService,
	) {}

	async calculate(projectId: string) {
		const { id, methods } = await this.projectsService.getById(projectId)

		const res = await Promise.all(
			methods.map(async method => {
				const params = await this.prisma.$queryRaw<
					GetParamsFnRes[]
				>`SELECT * FROM get_params(${id}, ${method.id})`

				const paramsRatios = await Promise.all(
					params.map(async param => {
						const ratio = await this.calculatePerParam(param)

						return {
							ratio,
							name: param.paramname,
						}
					}),
				)

				const totalRatio =
					(1 / params.length) *
					paramsRatios.reduce((sum, paramsRatio) => sum + paramsRatio.ratio, 0)

				return {
					name: method.name,
					totalRatio,
					paramsRatios,
				}
			}),
		)

		return res
	}

	async calculatePerParam(param: GetParamsFnRes): Promise<number> {
		const { methodparams: methodParams, projectparams: x } = param

		if ('values' in methodParams) {
			return methodParams.values.includes(x) ? 1 : -1
		}

		const { first, second } = methodParams

		if (first && second) {
			if (x <= first.xMin) {
				return -1
			}

			if (x <= first.xMax) {
				return (
					(1 +
						(((first.xMax - first.x) / (first.x - first.xMin)) ** 2 *
							((x - first.xMin) / (first.xMax - x)) ** 2) **
							((-1) ** 1)) **
					-1
				)
			}

			if (x <= second.xMin) {
				return 1
			}

			if (x <= second.xMax) {
				return (
					(1 +
						(((second.xMax - second.x) / (second.x - second.xMin)) ** 2 *
							((x - second.xMin) / (second.xMax - x)) ** 2) **
							((-1) ** 2)) **
					-1
				)
			}

			return -1
		}

		if (first) {
			if (x <= first.xMin) {
				return -1
			}

			if (x <= first.xMax) {
				return (
					(1 *
						(((first.xMax - first.x) / (first.x - first.xMin)) ** 2 *
							((x - first.xMin) / (first.xMax - x)) ** 2) **
							((-1) ** 1)) **
					-1
				)
			}

			return 1
		}

		if (second) {
			if (x <= second.xMin) return -1

			if (x <= second.xMax) {
				return (
					(1 +
						(((second.xMax - second.x) / (second.x - second.xMin)) ** 2 *
							((x - second.xMin) / (second.xMax - x)) ** 2) **
							((-1) ** 2)) **
					-1
				)
			}

			return 1
		}
	}
}
