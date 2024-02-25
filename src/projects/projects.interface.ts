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
	paramname: string
	methodparams: ConstantParams | ToComputeParams
	projectparams: number | null
}
