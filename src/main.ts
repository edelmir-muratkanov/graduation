import { Logger, ValidationPipe } from '@nestjs/common'
import { NestFactory } from '@nestjs/core'
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger'

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
		new ValidationPipe({
			transform: true,
			whitelist: true,
		}),
	)

	await app.listen(port, () => {
		logger.log(`Application started at http://localhost:${port}`)
		logger.log(`Swagger started at http;//localhost:${port}/api`)
	})
}

bootstrap()
