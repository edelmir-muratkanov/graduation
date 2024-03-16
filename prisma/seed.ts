/* eslint-disable eslint-comments/disable-enable-pair */
/* eslint-disable @typescript-eslint/no-unused-vars */

import { PrismaClient } from '@prisma/client'
import { hash } from 'bcrypt'

const prisma = new PrismaClient()

async function main() {
	/** Users */
	const user = await prisma.users.create({
		data: {
			email: 'admin@gmail.com',
			passwordHash: await hash('123456', 10),
			role: 'Admin',
		},
	})

	/** Properties */
	const p2 = await prisma.properties.create({
		data: { name: 'Средняя глубина залегания' },
	})
	const p3 = await prisma.properties.create({
		data: { name: 'Толщина пласта' },
	})
	const p4 = await prisma.properties.create({ data: { name: 'Пористость' } })
	const p5 = await prisma.properties.create({
		data: { name: 'Средняя нефтенасыщенность' },
	})
	const p6 = await prisma.properties.create({
		data: { name: 'Проницаемость' },
	})
	const p7 = await prisma.properties.create({
		data: { name: 'Пластовая температура' },
	})
	const p8 = await prisma.properties.create({
		data: { name: 'Давление насыщения нефти газом' },
	})
	const p9 = await prisma.properties.create({
		data: { name: 'Газосодержание' },
	})
	const p10 = await prisma.properties.create({
		data: { name: 'Средняя продуктивность' },
	})
	const p11 = await prisma.properties.create({
		data: { name: 'Вязкость нефти' },
	})
	const p12 = await prisma.properties.create({
		data: { name: 'Плотность нефти' },
	})
	const p13 = await prisma.properties.create({
		data: { name: 'Объемный коэффициент нефти' },
	})
	const p14 = await prisma.properties.create({
		data: { name: 'Плотность пластовой воды' },
	})

	const method = await prisma.methods.create({
		data: {
			name: 'Закачка СО2',
			collectorType: ['Terrigen'],
			parameters: {
				create: [
					{
						propertyId: p6.id,
						parameters: {
							first: { x: 5, xMin: 1.5, xMax: 10 },
							second: { x: 4000, xMin: 200, xMax: 4500 },
						},
					},
					{
						propertyId: p4.id,
						parameters: { first: { x: 0.07, xMin: 0.03, xMax: 0.1 } },
					},
					{
						propertyId: p5.id,
						parameters: { first: { x: 0.17, xMin: 0.15, xMax: 0.2 } },
					},
					{
						propertyId: p3.id,
						parameters: {
							first: { x: 8, xMin: 6, xMax: 10 },
							second: { x: 110, xMin: 100, xMax: 120 },
						},
					},
					{
						propertyId: p2.id,
						parameters: {
							first: { x: 800, xMax: 1000, xMin: 457 },
							second: { x: 4000, xMin: 3800, xMax: 4075 },
						},
					},
					{
						propertyId: p7.id,
						parameters: {
							first: { x: 30, xMin: 28, xMax: 35 },
							second: { x: 110, xMin: 100, xMax: 121 },
						},
					},
					{
						propertyId: p12.id,
						parameters: {
							first: { x: 810, xMin: 801, xMax: 820 },
							second: { x: 910, xMin: 900, xMax: 922 },
						},
					},
					{
						propertyId: p11.id,
						parameters: {
							second: { x: 36, xMin: 35, xMax: 37 },
						},
					},
				],
			},
		},
	})

	/** Projects */
	await prisma.projects.create({
		data: {
			name: 'Бурмаша',
			country: 'Казахстан',
			operator: 'АО "Мангистаумунайгаз"',
			type: 'Ground',
			collectorType: 'Terrigen',
			parameters: {
				createMany: {
					data: [
						{
							propertyId: p2.id,
							value: 1861,
						},
						{
							propertyId: p3.id,
							value: 34,
						},
						{
							propertyId: p4.id,
							value: 0.19,
						},
						{
							propertyId: p5.id,
							value: 0.67,
						},
						{
							propertyId: p6.id,
							value: 21.5,
						},
						{
							propertyId: p7.id,
							value: 85,
						},
						{
							propertyId: p8.id,
							value: 18.2,
						},
						{
							propertyId: p11.id,
							value: 2.31,
						},
						{
							propertyId: p12.id,
							value: 783,
						},
					],
				},
			},
			methods: {
				create: {
					methodId: method.id,
				},
			},
			users: {
				create: {
					userId: user.id,
				},
			},
		},
	})
}

main()
	.then(async () => {
		await prisma.$disconnect()
	})
	.catch(async e => {
		console.error(e)
		await prisma.$disconnect()
		process.exit(1)
	})
