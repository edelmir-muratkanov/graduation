import type { LogLevel } from '@nestjs/common'
import { Logger } from '@nestjs/common'
import { NestFactory } from '@nestjs/core'
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger'
import cookieParser from 'cookie-parser'
import helmet from 'helmet'

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

	const app = await NestFactory.create(AppModule, { logger: logLevels })

	app.setGlobalPrefix('api')
	app.use(cookieParser())
	app.use(helmet())

	const config = new DocumentBuilder()
		.setTitle('Graduation')
		.setDescription('The Graduation API description')
		.setVersion('0.1')
		.addCookieAuth(COOKIE.AccessToken, {
			type: 'http',
			in: 'Cookie',
			scheme: 'Bearer',
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

	await app.listen(port, () => {
		logger.log(`Application started at http://localhost:${port}/api`)
		logger.log(`Swagger started at http;//localhost:${port}/api/docs`)
	})
}

bootstrap()
