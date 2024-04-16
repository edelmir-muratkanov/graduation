import type { OnModuleDestroy, OnModuleInit } from '@nestjs/common'
import { Injectable, Logger } from '@nestjs/common'
import type { Prisma } from '@prisma/client'
import { PrismaClient } from '@prisma/client'

@Injectable()
export class PrismaService
	extends PrismaClient<Prisma.PrismaClientOptions, Prisma.LogLevel>
	implements OnModuleInit, OnModuleDestroy
{
	private readonly logger = new Logger(PrismaService.name)

	constructor() {
		super({
			log: [
				{
					emit: 'event',
					level: 'query',
				},
				{
					emit: 'event',
					level: 'error',
				},
				{
					emit: 'event',
					level: 'info',
				},
				{
					emit: 'event',
					level: 'warn',
				},
			],
		})
	}

	async onModuleInit() {
		await this.$connect()

		this.$on('query', ({ query, params, target, duration }) => {
			this.logger.log(`QUERY: ${query}`)
			this.logger.log(`PARAMS: ${params}`)
			this.logger.log(`TARGET: ${target}`)
			this.logger.log(`DURATION: ${duration}`)
		})
		this.$on('error', ({ message, target }) => {
			this.logger.error(`MESSAGE: ${message}`)
			this.logger.error(`TARGET: ${target}`)
		})
		this.$on('warn', ({ message, target }) => {
			this.logger.warn(`MESSAGE: ${message}`)
			this.logger.warn(`TARGET: ${target}`)
		})
		this.$on('info', ({ message, target }) => {
			this.logger.debug(`MESSAGE: ${message}`)
			this.logger.debug(`TARGET: ${target}`)
		})
	}

	async onModuleDestroy() {
		await this.$disconnect()
	}
}
