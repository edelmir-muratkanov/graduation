import type { NestMiddleware } from '@nestjs/common'
import { Injectable, Logger } from '@nestjs/common'
import type { NextFunction, Request, Response } from 'express'

@Injectable()
export class LoggingMiddleware implements NestMiddleware {
	private readonly logger = new Logger('Http')

	use(req: Request, res: Response, next: NextFunction) {
		res.on('finish', () => {
			const { method, originalUrl } = req
			const { statusCode, statusMessage } = res

			const message = `${method} ${originalUrl} ${statusCode} ${statusMessage}`

			if (statusCode >= 500) {
				return this.logger.error(message)
			}
			if (statusCode >= 400) {
				return this.logger.warn(message)
			}

			return this.logger.log(message)
		})

		next()
	}
}
