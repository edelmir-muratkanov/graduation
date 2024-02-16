import {
	BadRequestException,
	ConflictException,
	Injectable,
} from '@nestjs/common'
import { Prisma } from '@prisma/client'
import { I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { PrismaErrors, PrismaService } from 'src/shared/prisma'

import type {
	CreateProjectMethodId,
	CreateProjectParameters,
} from './projects.interface'

@Injectable()
export class ProjectsService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async create(
		name: string,
		country: string,
		operator: string,
		methodIds: CreateProjectMethodId[],
		parameters: CreateProjectParameters[],
		userId: string,
	) {
		try {
			return await this.prisma.project.create({
				data: {
					name,
					country,
					operator,
					methods: {
						createMany: {
							data: methodIds,
							skipDuplicates: true,
						},
					},
					parameters: {
						createMany: {
							skipDuplicates: true,
							data: parameters,
						},
					},
					users: {
						create: {
							userId,
						},
					},
				},
			})
		} catch (e) {
			if (e instanceof Prisma.PrismaClientKnownRequestError) {
				if (e.code === PrismaErrors.UniqueConstraintViolated) {
					throw new ConflictException(this.i18n.t('exceptions.project.Exists'))
				}

				if (e.code === PrismaErrors.ForeignKeyConstraintViolated) {
					throw new BadRequestException(
						this.i18n.t('exceptions.project.InvalidProperties'),
					)
				}
			}
		}
	}
}
