import type { MiddlewareConsumer } from '@nestjs/common'
import { Module } from '@nestjs/common'
import { ConfigModule } from '@nestjs/config'
import { APP_FILTER, APP_PIPE } from '@nestjs/core'
import * as Joi from 'joi'
import {
	HeaderResolver,
	I18nModule,
	I18nValidationExceptionFilter,
	I18nValidationPipe,
	QueryResolver,
} from 'nestjs-i18n'
import { join } from 'path'
import { PrismaModule } from 'src/shared/prisma/prisma.module'

import { AuthModule } from './auth/auth.module'
import { PropertiesModule } from './properties/properties.module'
import { LoggingMiddleware } from './shared/middleware/logging.middleware'
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
				en: 'en',
				ru: 'ru',
				kz: 'kz',
			},
			loaderOptions: {
				path: join(__dirname, '/shared/i18n/'),
				watch: true,
			},
			resolvers: [
				{
					use: QueryResolver,
					options: ['lang'],
				},
				new HeaderResolver(['x-lang']),
			],
			typesOutputPath: join(__dirname, '../src/shared/generated/i18n.ts'),
		}),
		AuthModule,
		UsersModule,
		PrismaModule,
		PropertiesModule,
	],
	providers: [
		{
			provide: APP_PIPE,
			useValue: new I18nValidationPipe({
				transform: true,
				whitelist: true,
			}),
		},
		{
			provide: APP_FILTER,
			useValue: new I18nValidationExceptionFilter(),
		},
	],
})
export class AppModule {
	configure(consumer: MiddlewareConsumer) {
		consumer.apply(LoggingMiddleware).forRoutes('*')
	}
}
