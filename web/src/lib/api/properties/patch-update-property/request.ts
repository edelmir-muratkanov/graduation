import { API } from '../../instance'

export interface PatchUpdatePropertyParams {
  name: string | null
  unit: string | null
}

export type PatchUpdatePropertyRequestConfig =
  RequestConfig<PatchUpdatePropertyParams>

export const patchUpdateProperty = (
  propertyId: string,
  { params, config }: PatchUpdatePropertyRequestConfig,
) => API.patch(`properties/${propertyId}`, params, config)
