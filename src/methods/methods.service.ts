import {
	ConflictException,
	Injectable,
	Logger,
	NotFoundException,
} from '@nestjs/common'
import type { CollectorType } from '@prisma/client'
import { Prisma } from '@prisma/client'
import { I18nContext, I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { PrismaErrors, PrismaService } from 'src/shared/prisma'

import type { MethodParametersData } from './methods.interface'

@Injectable()
export class MethodsService {
	private logger = new Logger(MethodsService.name)

	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async create(
		name: string,
		collectorTypes: CollectorType[],
		parameters: MethodParametersData[],
	) {
		try {
			return await this.prisma.methods.create({
				data: {
					name,
					collectorTypes,
					parameters: {
						createMany: {
							data: parameters,
							skipDuplicates: true,
						},
					},
				},
			})
		} catch (e) {
			this.logger.error(e)
			if (e instanceof Prisma.PrismaClientKnownRequestError) {
				if (e.code === PrismaErrors.ForeignKeyConstraintViolated) {
					throw new ConflictException(
						this.i18n.t('exceptions.method.InvalidProperties', {
							lang: I18nContext.current().lang,
						}),
					)
				}
				if (e.code === PrismaErrors.UniqueConstraintViolated) {
					throw new ConflictException(
						this.i18n.t('exceptions.method.MethodExists', {
							lang: I18nContext.current().lang,
						}),
					)
				}
			}
		}
	}

	async update(
		id: string,
		name: string,
		collectorTypes: CollectorType[],
		parameters: MethodParametersData[],
	) {
		try {
			await this.prisma.methods.update({
				where: { id },
				data: {
					name,
					collectorTypes,
					parameters: {
						upsert: parameters.map(({ parameters, propertyId }) => ({
							where: {
								methodId_propertyId: {
									methodId: id,
									propertyId,
								},
							},
							create: {
								parameters,
								propertyId,
							},
							update: { parameters },
						})),
					},
				},
			})
		} catch (e) {
			this.logger.error(e)

			if (e instanceof Prisma.PrismaClientKnownRequestError) {
				if (e.code === PrismaErrors.RecordDoesNotExist) {
					throw new NotFoundException(
						this.i18n.t('exceptions.method.NotFound', {
							lang: I18nContext.current().lang,
						}),
					)
				}

				if (e.code === PrismaErrors.ForeignKeyConstraintViolated) {
					throw new ConflictException(
						this.i18n.t('exceptions.method.InvalidProperties', {
							lang: I18nContext.current().lang,
						}),
					)
				}
			}
		}
	}

	async getAll(
		limit?: number,
		offset?: number,
		lastCursorId?: string,
		search?: string,
		collectorType?: CollectorType,
	) {
		const where: Prisma.MethodsWhereInput[] = []

		if (search) {
			where.push({
				name: {
					contains: search,
					mode: 'insensitive',
				},
			})
		}

		if (collectorType) {
			where.push({
				collectorTypes: {
					has: collectorType,
				},
			})
		}

		const [count, items] = await this.prisma.$transaction([
			this.prisma.methods.count({
				where: { AND: where },
			}),
			this.prisma.methods.findMany({
				where: { AND: where },
				include: {
					_count: {
						select: {
							projects: true,
						},
					},
				},
				orderBy: { name: 'asc' },
				take: limit,
				...(lastCursorId
					? {
							skip: 1,
							cursor: { id: lastCursorId },
						}
					: {
							skip: offset,
						}),
			}),
		])

		return { count, items }
	}

	async getById(id: string) {
		const method = await this.prisma.methods.findUnique({
			where: { id },
			include: {
				parameters: {
					select: {
						propertyId: true,
						parameters: true,
					},
				},
			},
		})

		if (!method) {
			throw new NotFoundException(
				this.i18n.t('exceptions.method.NotFound', {
					lang: I18nContext.current().lang,
				}),
			)
		}

		return method
	}

	async delete(id: string) {
		try {
			await this.prisma.methods.delete({
				where: { id },
			})
		} catch (e) {
			this.logger.error(e)
			if (
				e instanceof Prisma.PrismaClientKnownRequestError &&
				e.code === PrismaErrors.RecordDoesNotExist
			) {
				throw new NotFoundException(
					this.i18n.t('exceptions.method.NotFound', {
						lang: I18nContext.current().lang,
					}),
				)
			}
		}
	}
}
