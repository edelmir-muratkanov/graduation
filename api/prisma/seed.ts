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
	const p1 = await prisma.properties.create({
		data: { name: 'Пластовое давление', unit: 'МПа' },
	})
	const p2 = await prisma.properties.create({
		data: { name: 'Средняя глубина залегания', unit: 'м' },
	})
	const p3 = await prisma.properties.create({
		data: { name: 'Толщина пласта', unit: 'м' },
	})
	const p4 = await prisma.properties.create({
		data: { name: 'Пористость', unit: 'д.ед' },
	})
	const p5 = await prisma.properties.create({
		data: { name: 'Средняя нефтенасыщенность', unit: 'д.ед' },
	})
	const p6 = await prisma.properties.create({
		data: { name: 'Проницаемость', unit: 'мД' },
	})
	const p7 = await prisma.properties.create({
		data: { name: 'Пластовая температура', unit: '°С' },
	})
	const p8 = await prisma.properties.create({
		data: { name: 'Давление насыщения нефти газом', unit: 'МПа' },
	})
	const p9 = await prisma.properties.create({
		data: { name: 'Газосодержание', unit: 'м3/т' },
	})
	const p10 = await prisma.properties.create({
		data: { name: 'Средняя продуктивность', unit: 'т/сут*МПа' },
	})
	const p11 = await prisma.properties.create({
		data: { name: 'Вязкость нефти', unit: 'мПа*с' },
	})
	const p12 = await prisma.properties.create({
		data: { name: 'Плотность нефти', unit: 'кг/м3' },
	})
	const p13 = await prisma.properties.create({
		data: { name: 'Объемный коэффициент нефти', unit: 'д.ед' },
	})
	const p14 = await prisma.properties.create({
		data: { name: 'Плотность пластовой воды', unit: 'кг/м3' },
	})

	const co2 = await prisma.methods.create({
		data: {
			name: 'Закачка УВ газа',
			collectorTypes: ['Terrigen', 'Carbonate'],
			parameters: {
				create: [
					{
						propertyId: p6.id,
						parameters: {
							first: { x: 1, xMin: 0.1, xMax: 1.5 },
							second: { x: 3000, xMin: 726, xMax: 5000 },
						},
					},
					{
						propertyId: p4.id,
						parameters: { first: { x: 0.1, xMin: 0.04, xMax: 0.15 } },
					},
					{
						propertyId: p5.id,
						parameters: { first: { x: 0.35, xMin: 0.3, xMax: 0.4 } },
					},
					{
						propertyId: p3.id,
						parameters: {
							first: { x: 8, xMin: 5.9999, xMax: 10 },
							second: { x: 35, xMin: 30, xMax: 40 },
						},
					},
					{
						propertyId: p2.id,
						parameters: {
							first: { x: 1350, xMax: 1500, xMin: 1219 },
							second: { x: 4700, xMin: 4500, xMax: 4850 },
						},
					},
					{
						propertyId: p7.id,
						parameters: {
							first: { x: 32, xMin: 29, xMax: 35 },
							second: { x: 120, xMin: 100, xMax: 121 },
						},
					},
					{
						propertyId: p12.id,
						parameters: {
							first: { x: 770, xMin: 750, xMax: 780 },
							second: { x: 910, xMin: 900, xMax: 916 },
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
	const uvgas = await prisma.methods.create({
		data: {
			name: 'Закачка СО2',
			collectorTypes: ['Terrigen', 'Carbonate'],
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
							first: { x: 5, xMin: 0.04, xMax: 10 },
							second: { x: 400, xMin: 286, xMax: 18000 },
						},
					},
				],
			},
		},
	})

	const vgv = await prisma.methods.create({
		data: {
			name: 'Водогазовое воздействие',
			collectorTypes: ['Terrigen', 'Carbonate'],
			parameters: {
				create: [
					{
						propertyId: p6.id,
						parameters: {
							first: { x: 150, xMin: 130, xMax: 200 },
							second: { x: 900, xMin: 800, xMax: 1000 },
						},
					},
					{
						propertyId: p4.id,
						parameters: {
							first: { x: 0.105, xMin: 0.1, xMax: 0.11 },
							second: { xMax: 0.3, xMin: 0.24, x: 0.28 },
						},
					},
					{
						propertyId: p5.id,
						parameters: { first: { x: 0.42, xMin: 0.4, xMax: 0.42 } },
					},
					{
						propertyId: p3.id,
						parameters: {
							first: { x: 8, xMin: 6, xMax: 10 },
							second: { x: 35, xMin: 30, xMax: 40 },
						},
					},
					{
						propertyId: p2.id,
						parameters: {
							first: { x: 2350, xMax: 2400, xMin: 2300 },
							second: { x: 2700, xMin: 2650, xMax: 2708 },
						},
					},
					{
						propertyId: p7.id,
						parameters: {
							first: { x: 92, xMin: 90, xMax: 95 },
							second: { x: 122, xMin: 120, xMax: 123 },
						},
					},
					{
						propertyId: p12.id,
						parameters: {
							first: { x: 832, xMin: 829, xMax: 835 },
							second: { x: 855, xMin: 850, xMax: 860 },
						},
					},
					{
						propertyId: p11.id,
						parameters: {
							second: { x: 1, xMin: 0.3, xMax: 0.5 },
						},
					},
				],
			},
		},
	})

	/** Projects */
	const project = await prisma.projects.create({
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
				create: [
					{ methodId: co2.id },
					{ methodId: vgv.id },
					{ methodId: uvgas.id },
				],
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
