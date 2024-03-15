export interface CreateProjectMethodId {
	methodId: string
}

export interface CreateProjectParameters {
	propertyId: string
	value: number
}

export interface Group {
	x: number
	xMin: number
	xMax: number
}

type ConstantParams = { values: number[] }
type ToComputeParams = { first?: Group; second?: Group }

export interface GetParams {
	paramName: string
	methodParams: ConstantParams | ToComputeParams
	projectParams: number | null
}
