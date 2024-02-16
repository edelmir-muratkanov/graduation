import {
	ConflictException,
	Injectable,
	NotFoundException,
} from '@nestjs/common'
import { Prisma } from '@prisma/client'
import { I18nContext, I18nService } from 'nestjs-i18n'
import type { I18nTranslations } from 'src/shared/generated'
import { PrismaService } from 'src/shared/prisma'
import { PasswordService } from 'src/shared/services'

@Injectable()
export class UsersService {
	constructor(
		private readonly prisma: PrismaService,
		private readonly passwordService: PasswordService,
		private readonly i18n: I18nService<I18nTranslations>,
	) {}

	async createUser(email: string, password: string) {
		const hashedPassword = await this.passwordService.hashPassword(password)

		try {
			return await this.prisma.user.create({
				data: {
					email,
					passwordHash: hashedPassword,
				},
				select: {
					id: true,
					email: true,
					role: true,
				},
			})
		} catch (e) {
			if (
				e instanceof Prisma.PrismaClientKnownRequestError &&
				e.code === 'P2002'
			) {
				throw new ConflictException(
					this.i18n.t('exceptions.user.EmailExists', {
						lang: I18nContext.current().lang,
					}),
				)
			}

			throw new Error(e)
		}
	}

	async findByEmail(email: string) {
		const user = await this.prisma.user.findUnique({ where: { email } })

		if (!user) {
			throw new NotFoundException(
				this.i18n.t('exceptions.user.NotFound', {
					lang: I18nContext.current().lang,
				}),
			)
		}

		return user
	}

	async findById(id: string) {
		const user = await this.prisma.user.findUnique({ where: { id } })
		if (!user) {
			throw new NotFoundException(
				this.i18n.t('exceptions.user.NotFound', {
					lang: I18nContext.current().lang,
				}),
			)
		}

		return user
	}
}
