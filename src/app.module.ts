import { Module } from '@nestjs/common'
import { ConfigModule } from '@nestjs/config'
import * as Joi from 'joi'
import {
	AcceptLanguageResolver,
	HeaderResolver,
	I18nModule,
	QueryResolver,
} from 'nestjs-i18n'
import { join } from 'path'

import { AuthModule } from './auth/auth.module'
import { PrismaModule } from './prisma/prisma.module'
import { UsersModule } from './users/users.module'

@Module({
	imports: [
		ConfigModule.forRoot({
			isGlobal: true,
			validationSchema: Joi.object({
				PORT: Joi.number().required(),
				ACCESS_TOKEN_SECRET: Joi.string().required(),
				ACCESS_TOKEN_EXPIRATION: Joi.string().required(),
				REFRESH_TOKEN_SECRET: Joi.string().required(),
				REFRESH_TOKEN_EXPIRATION: Joi.string().required(),
			}),
		}),
		I18nModule.forRoot({
			fallbackLanguage: 'en',
			fallbacks: {
				'en-*': 'en',
				'ru-*': 'ru',
				'kz-*': 'kz',
			},
			loaderOptions: {
				path: join(__dirname, '/i18n/'),
				watch: true,
			},
			resolvers: [
				{
					use: QueryResolver,
					options: ['lang'],
				},
				AcceptLanguageResolver,
				new HeaderResolver(['x-lang']),
			],
			typesOutputPath: join(__dirname, '../src/shared/generated/i18n.ts'),
		}),
		AuthModule,
		UsersModule,
		PrismaModule,
	],
})
export class AppModule {}
