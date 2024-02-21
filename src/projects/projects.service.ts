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

import type { ProjectMethodParameter } from './dto/project.response'
import { ProjectResponse } from './dto/project.response'
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
					throw new ConflictException(
						this.i18n.t('exceptions.project.Exists', {
							lang: I18nContext.current().lang,
						}),
					)
				}

				if (e.code === PrismaErrors.ForeignKeyConstraintViolated) {
					throw new BadRequestException(
						this.i18n.t('exceptions.project.InvalidProperties', {
							lang: I18nContext.current().lang,
						}),
					)
				}
			}
		}
	}

	async getAll(
		userId?: string,
		limit?: number,
		offset?: number,
		lastCursorId?: string,
	) {
		const [count, items] = await this.prisma.$transaction([
			this.prisma.project.count({
				where: {
					users: {
						some: {
							userId,
						},
					},
				},
			}),
			this.prisma.project.findMany({
				where: {
					users: {
						some: {
							userId,
						},
					},
				},
				include: {
					_count: {
						select: {
							methods: true,
							parameters: true,
							users: true,
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
		const project = await this.prisma.project.findUnique({
			where: {
				id,
			},
			select: {
				id: true,
				name: true,
				country: true,
				operator: true,
				users: {
					select: {
						user: {
							select: {
								id: true,
								email: true,
							},
						},
					},
				},
				parameters: {
					select: {
						value: true,
						property: {
							select: {
								id: true,
								name: true,
							},
						},
					},
				},
				methods: {
					select: {
						method: {
							select: {
								id: true,
								name: true,
								parameters: {
									select: {
										propertyId: true,
										parameters: true,
									},
								},
							},
						},
					},
				},
			},
		})

		if (!project) {
			throw new NotFoundException(
				this.i18n.t('exceptions.project.NotFound', {
					lang: I18nContext.current().lang,
				}),
			)
		}

		const res = new ProjectResponse()

		res.id = project.id
		res.country = project.country
		res.name = project.name
		res.operator = project.operator
		res.methods = project.methods.map(m => ({
			id: m.method.id,
			name: m.method.name,
			parameters: m.method.parameters.map(p => p as ProjectMethodParameter),
		}))
		res.parameters = project.parameters
		res.users = project.users.map(u => ({ id: u.user.id, email: u.user.email }))

		return res
	}
}
