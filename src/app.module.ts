import { BullModule } from '@nestjs/bull'
import type { CacheStore } from '@nestjs/cache-manager'
import { CacheModule } from '@nestjs/cache-manager'
import { Module } from '@nestjs/common'
import { ConfigModule } from '@nestjs/config'
import { APP_FILTER, APP_INTERCEPTOR, APP_PIPE } from '@nestjs/core'
import { redisStore } from 'cache-manager-redis-store'
import * as Joi from 'joi'
import {
	HeaderResolver,
	I18nModule,
	I18nValidationExceptionFilter,
	I18nValidationPipe,
	QueryResolver,
} from 'nestjs-i18n'
import { join } from 'path'
import type { RedisClientOptions } from 'redis'

import { AuthModule } from './auth/auth.module'
import { CalculationsModule } from './calculations/calculations.module'
import { MethodsModule } from './methods/methods.module'
import { ProjectsModule } from './projects/projects.module'
import { PropertiesModule } from './properties/properties.module'
import { PrismaModule } from './shared/prisma'
import { LoggingInterceptor } from './shared/services'
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
				REDIS_HOST: Joi.string().required(),
				REDIS_PORT: Joi.number().required(),
			}),
		}),
		I18nModule.forRoot({
			fallbackLanguage: 'ru',
			fallbacks: {
				en: 'en',
				ru: 'ru',
				kz: 'kz',
			},
			loaderOptions: {
				path: join(process.cwd(), '/assets/i18n/'),
				watch: true,
			},
			resolvers: [
				{
					use: QueryResolver,
					options: ['lang'],
				},
				new HeaderResolver(['x-lang']),
			],
			typesOutputPath: join(process.cwd(), '/src/shared/generated/i18n.ts'),
		}),
		BullModule.forRoot({
			redis: {
				host: process.env.REDIS_HOST,
				port: +process.env.REDIS_PORT,
			},
		}),
		CacheModule.registerAsync<RedisClientOptions>({
			isGlobal: true,
			useFactory: async () => {
				const store = await redisStore({
					socket: {
						host: process.env.REDIS_HOST,
						port: parseInt(process.env.REDIS_PORT, 10),
					},
					ttl: 30_000,
				})

				return {
					store: store as unknown as CacheStore,
				}
			},
		}),
		PrismaModule,
		AuthModule,
		UsersModule,
		PropertiesModule,
		MethodsModule,
		ProjectsModule,
		CalculationsModule,
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
		{
			provide: APP_INTERCEPTOR,
			useValue: new LoggingInterceptor(),
		},
	],
})
export class AppModule {}
