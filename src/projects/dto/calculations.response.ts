export class ParamsRatio {
	name: string

	ratio: number
}

export class CalculationsResponse {
	name: string

	result: string

	totalRatio: number

	paramsRations: ParamsRatio[]
}
