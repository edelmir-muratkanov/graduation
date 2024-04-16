import cookie from '@fastify/cookie'
import helmet from '@fastify/helmet'
import type { LogLevel } from '@nestjs/common'
import { Logger } from '@nestjs/common'
import { NestFactory } from '@nestjs/core'
import {
	FastifyAdapter,
	type NestFastifyApplication,
} from '@nestjs/platform-fastify'
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger'

import { COOKIE } from './auth/auth.constants'
import { AppModule } from './app.module'

async function bootstrap() {
	const logger = new Logger('Bootstrap')
	const port = process.env.PORT
	const isProd =
		process.env.NODE_ENV === 'production' || process.env.NODE_ENV === 'prod'
	const logLevels: LogLevel[] = isProd
		? ['error', 'warn', 'log']
		: ['error', 'warn', 'log', 'debug', 'verbose']

	const app = await NestFactory.create<NestFastifyApplication>(
		AppModule,
		new FastifyAdapter(),
		{
			logger: logLevels,
		},
	)

	app.register(cookie)
	app.register(helmet, {
		contentSecurityPolicy: {
			directives: {
				defaultSrc: [`'self'`],
				styleSrc: [`'self'`, `'unsafe-inline'`],
				imgSrc: [`'self'`, 'data:', 'validator.swagger.io'],
				scriptSrc: [`'self'`, `https: 'unsafe-inline'`],
			},
		},
	})
	app.setGlobalPrefix('api')

	const config = new DocumentBuilder()
		.setTitle('Graduation')
		.setDescription('The Graduation API description')
		.setVersion('0.1')
		.addCookieAuth(COOKIE.AccessToken, {
			type: 'http',
			in: 'Cookie',
		})
		.addGlobalParameters({
			in: 'query',
			name: 'lang',
			schema: {
				default: 'en',
				enum: ['en', 'ru', 'kz'],
			},
		})
		.build()

	const document = SwaggerModule.createDocument(app, config)
	SwaggerModule.setup('api/docs', app, document, {
		swaggerOptions: {
			withCredentials: true,
		},
	})

	await app.listen(port, '0.0.0.0', (err, address) => {
		if (err) {
			logger.error(err.message, err.stack, err.name)
			process.exit(1)
		}
		logger.log(`Application started at ${address}/api`)
		logger.log(`Swagger started at ${address}/api/docs`)
	})
}

bootstrap()
