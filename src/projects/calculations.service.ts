import { Injectable } from '@nestjs/common'
import { Prisma } from '@prisma/client'
import { PrismaService } from 'src/shared/prisma'

import type { GetParams } from './projects.interface'
import { ProjectsService } from './projects.service'

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
				const params = await this.getParams(id, method.id)

				const paramsRatios = params.map(param => {
					const ratio = this.calculateParamRatio(param)

					return {
						ratio,
						name: param.paramname,
					}
				})

				const totalRatio = this.calculateTotalRatio(params.length, paramsRatios)

				return {
					name: method.name,
					totalRatio,
					paramsRatios,
				}
			}),
		)

		return res
	}

	async getParams(projectId: string, methodId: string): Promise<GetParams[]> {
		const query = Prisma.sql`SELECT * FROM get_params(${projectId}, ${methodId})`

		return this.prisma.$queryRaw<GetParams[]>(query)
	}

	calculateParamRatio(param: GetParams): number {
		const { methodparams, projectparams } = param

		if (projectparams === null) {
			return -1
		}

		if ('values' in methodparams) {
			return methodparams.values.includes(projectparams) ? 1 : -1
		}

		const { first, second } = methodparams

		const calculateHelper = (params, index) => {
			if (projectparams <= params.xMin) {
				return -1
			}

			if (projectparams <= params.xMax) {
				return (
					(1 +
						((params.xMax - params.x) / (params.x - params.xMin)) ** 2 *
							((projectparams - params.xMin) / (params.xMax - projectparams)) **
								2 *
							(-1) ** index) *
					-1
				)
			}

			return 1
		}

		if (first && second) {
			if (projectparams <= first.xMin) {
				return -1
			}

			if (projectparams <= first.xMax) {
				return calculateHelper(first, 1)
			}

			if (projectparams <= second.xMin) {
				return 1
			}

			if (projectparams <= second.xMax) {
				return calculateHelper(second, 2)
			}

			return -1
		}

		if (first) {
			if (projectparams <= first.xMin) {
				return -1
			}

			if (projectparams <= first.xMax) {
				return calculateHelper(first, 1)
			}

			return 1
		}

		if (second) {
			if (projectparams <= second.xMin) return -1

			if (projectparams <= second.xMax) {
				return calculateHelper(second, 2)
			}

			return 1
		}
	}

	calculateTotalRatio(
		paramsCount: number,
		paramsRatios: { ratio: number; name: string }[],
	): number {
		return (
			(1 / paramsCount) *
			paramsRatios.reduce((sum, paramsRatio) => sum + paramsRatio.ratio, 0)
		)
	}
}
