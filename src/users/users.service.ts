import { ConflictException, Injectable } from '@nestjs/common'
import { Prisma } from '@prisma/client'
import { PrismaService } from 'src/prisma/prisma.service'
import { PasswordService } from 'src/shared/services/password.service'

@Injectable()
export class UsersService {
	constructor(
		private prisma: PrismaService,
		private passwordService: PasswordService,
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
				// TODO: add i18n
				throw new ConflictException()
			}

			throw new Error(e)
		}
	}
}
