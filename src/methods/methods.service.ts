import {
	ConflictException,
	Injectable,
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

	async getAll(
		limit?: number,
		offset?: number,
		lastCursorId?: string,
		search?: string,
	) {
		const [count, items] = await this.prisma.$transaction([
			this.prisma.methods.count({
				where: {
					name: {
						contains: search,
						mode: 'insensitive',
					},
				},
			}),
			this.prisma.methods.findMany({
				where: {
					name: {
						contains: search,
						mode: 'insensitive',
					},
				},
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
}
