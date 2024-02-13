import { Logger } from '@nestjs/common'
import { NestFactory } from '@nestjs/core'
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger'
import { I18nValidationExceptionFilter, I18nValidationPipe } from 'nestjs-i18n'

import { AppModule } from './app.module'

async function bootstrap() {
	const logger = new Logger('Bootstrap')
	const app = await NestFactory.create(AppModule)
	const port = process.env.PORT

	const config = new DocumentBuilder()
		.setTitle('Graduation')
		.setDescription('The Graduation API description')
		.setVersion('0.1')
		.build()

	const document = SwaggerModule.createDocument(app, config)
	SwaggerModule.setup('api', app, document)

	app.useGlobalPipes(
		new I18nValidationPipe({
			transform: true,
			whitelist: true,
		}),
	)

	app.useGlobalFilters(
		new I18nValidationExceptionFilter({ detailedErrors: false }),
	)

	await app.listen(port, () => {
		logger.log(`Application started at http://localhost:${port}`)
		logger.log(`Swagger started at http;//localhost:${port}/api`)
	})
}

bootstrap()
