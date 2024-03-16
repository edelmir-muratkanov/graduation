import {
	BadRequestException,
	ConflictException,
	Injectable,
	NotFoundException,
} from '@nestjs/common'
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

	async create(name: string, parameters: MethodParametersData[]) {
		if (
			parameters.some(
				p =>
					'values' in p.parameters &&
					('first' in p.parameters || 'second' in p.parameters),
			)
		) {
			return new BadRequestException(
				this.i18n.t('exceptions.method.ValuesOrGroups', {
					lang: I18nContext.current().lang,
				}),
			)
		}

		try {
			return await this.prisma.methods.create({
				data: {
					name,
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
					return new ConflictException(
						this.i18n.t('exceptions.method.InvalidProperties', {
							lang: I18nContext.current().lang,
						}),
					)
				}
				if (e.code === PrismaErrors.UniqueConstraintViolated) {
					return new ConflictException(
						this.i18n.t('exceptions.method.MethodExists', {
							lang: I18nContext.current().lang,
						}),
					)
				}
			}
		}
	}

	async getAll(limit?: number, offset?: number, lastCursorId?: string) {
		const [count, items] = await this.prisma.$transaction([
			this.prisma.methods.count(),
			this.prisma.methods.findMany({
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
