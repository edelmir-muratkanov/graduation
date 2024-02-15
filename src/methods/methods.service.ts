import {
	BadRequestException,
	ConflictException,
	Injectable,
} from '@nestjs/common'
import { Prisma } from '@prisma/client'
import { I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated/i18n'
import { PrismaErrors } from 'src/shared/prisma/prisma.errors'
import { PrismaService } from 'src/shared/prisma/prisma.service'

import type { MethodParametersData } from './methods.interface'

@Injectable()
export class MethodsService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async create(name: string, parameters: MethodParametersData[]) {
		try {
			await this.prisma.method.create({
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
					return new BadRequestException(
						this.i18n.t('exceptions.method.PropertyNotFound'),
					)
				}
				if (e.code === PrismaErrors.UniqueConstraintViolated) {
					return new ConflictException(
						this.i18n.t('exceptions.method.MethodExists', { args: { name } }),
					)
				}
			}
		}
	}

	async getAll(limit?: number, offset?: number, lastCursorId?: string) {
		const [count, items] = await this.prisma.$transaction([
			this.prisma.method.count(),
			this.prisma.method.findMany({
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
}
