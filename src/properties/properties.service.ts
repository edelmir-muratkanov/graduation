import { ConflictException, Injectable } from '@nestjs/common'
import { Prisma } from '@prisma/client'
import { I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated/i18n'
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
				e.code === 'P2002'
			) {
				throw new ConflictException(this.i18n.t('exceptions'))
			}
		}
	}
}
