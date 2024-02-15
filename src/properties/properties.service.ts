import {
	ConflictException,
	Injectable,
	NotFoundException,
} from '@nestjs/common'
import { Prisma } from '@prisma/client'
import { I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated/i18n'
import { PrismaErrors } from 'src/shared/prisma/prisma.errors'
import { PrismaService } from 'src/shared/prisma/prisma.service'

@Injectable()
export class PropertiesService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async create(name: string) {
		try {
			return await this.prisma.property.create({ data: { name } })
		} catch (e) {
			if (
				e instanceof Prisma.PrismaClientKnownRequestError &&
				e.code === PrismaErrors.UniqueConstraintViolated
			) {
				throw new ConflictException(
					this.i18n.t('exceptions.property.NameExists', { args: { name } }),
				)
			}
		}
	}

	async getAll(limit?: number, offset?: number, lastCursorId?: string) {
		const [count, items] = await this.prisma.$transaction([
			this.prisma.property.count(),
			this.prisma.property.findMany({
				include: {
					_count: {
						select: {
							methods: true,
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

	async update(id: string, name: string) {
		try {
			await this.prisma.property.update({
				where: { id },
				data: { name },
			})
		} catch (e) {
			if (
				e instanceof Prisma.PrismaClientKnownRequestError &&
				e.code === PrismaErrors.RecordDoesNotExist
			) {
				throw new NotFoundException(
					this.i18n.t('exceptions.property.NotFound', { args: { id } }),
				)
			}
		}
	}
}
