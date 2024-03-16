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

export interface GetParams {
	paramName: string
	methodParams: { first?: Group; second?: Group }
	projectParams: number | null
}
