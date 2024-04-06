import {
	BadRequestException,
	ConflictException,
	ForbiddenException,
	Injectable,
	Logger,
	NotFoundException,
} from '@nestjs/common'
import type { CollectorType, ProjectType } from '@prisma/client'
import { Prisma } from '@prisma/client'
import { I18nContext, I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { PrismaErrors, PrismaService } from 'src/shared/prisma'

import { ProjectResponse } from './dto/project.response'
import type { CreateProjectParameters } from './projects.interface'

@Injectable()
export class ProjectsService {
	private logger = new Logger(ProjectsService.name)

	constructor(
		private readonly prisma: PrismaService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async create(
		name: string,
		country: string,
		type: ProjectType,
		operator: string,
		methodIds: string[],
		parameters: CreateProjectParameters[],
		userId: string,
		collectorType?: CollectorType,
	) {
		try {
			return await this.prisma.projects.create({
				data: {
					name,
					country,
					operator,
					type,
					collectorType,
					methods: {
						createMany: {
							data: methodIds.map(id => ({ methodId: id })),
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
			this.logger.error(e)

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

			throw e
		}
	}

	async getAll(
		userId?: string,
		limit?: number,
		offset?: number,
		lastCursorId?: string,
		search?: string,
		projectType?: ProjectType,
		collectorType?: CollectorType,
	) {
		const where: Prisma.ProjectsWhereInput[] = []

		if (userId) {
			where.push({
				users: { some: { userId } },
			})
		}

		if (search) {
			where.push({
				OR: [
					{ name: { contains: search, mode: 'insensitive' } },
					{ country: { contains: search, mode: 'insensitive' } },
					{ operator: { contains: search, mode: 'insensitive' } },
				],
			})
		}

		if (projectType) {
			where.push({ type: projectType })
		}

		if (collectorType) {
			where.push({ collectorType })
		}

		const [count, items] = await this.prisma.$transaction([
			this.prisma.projects.count({
				where: { AND: where },
			}),
			this.prisma.projects.findMany({
				where: { AND: where },
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
		const project = await this.prisma.projects.findUnique({
			where: {
				id,
			},
			include: {
				users: {
					select: {
						userId: true,
					},
				},
				parameters: {
					select: {
						value: true,
						property: {
							select: {
								id: true,
								name: true,
								unit: true,
							},
						},
					},
				},
				methods: {
					select: {
						methodId: true,
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
		res.collectorType = project.collectorType
		res.type = project.type
		res.parameters = project.parameters
		res.methodIds = project.methods.map(m => m.methodId)
		res.userIds = project.users.map(u => u.userId)

		return res
	}

	async update(
		id: string,
		userId: string,
		name: string,
		operator: string,
		country: string,
		collectorType: CollectorType,
		projectType: ProjectType,
		methodIds: string[],
		parameters: CreateProjectParameters[],
	) {
		const { users } = await this.prisma.projects.findUnique({
			where: { id },
			select: { users: true },
		})

		if (!users.some(up => up.userId === userId)) {
			throw new ForbiddenException()
		}

		try {
			await this.prisma.$transaction([
				this.prisma.projectsMethods.deleteMany({
					where: { projectId: id },
				}),
				this.prisma.projectsMethods.createMany({
					data: methodIds.map(methodId => ({ projectId: id, methodId })),
					skipDuplicates: true,
				}),
				this.prisma.projectsProperties.deleteMany({
					where: { projectId: id },
				}),
				this.prisma.projectsProperties.createMany({
					data: parameters.map(p => ({
						projectId: id,
						propertyId: p.propertyId,
						value: p.value,
					})),
					skipDuplicates: true,
				}),
				this.prisma.projects.update({
					where: { id },
					data: {
						name,
						operator,
						country,
						collectorType,
						type: projectType,
					},
				}),
			])
		} catch (e) {
			this.logger.error(e)

			if (e instanceof Prisma.PrismaClientKnownRequestError) {
				if (e.code === PrismaErrors.RecordDoesNotExist) {
					throw new NotFoundException(
						this.i18n.t('exceptions.project.NotFound', {
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
}
