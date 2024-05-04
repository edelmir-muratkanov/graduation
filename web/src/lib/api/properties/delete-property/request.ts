import { API } from '../../instance'

export type DeletePropertyRequestConfig = RequestConfig

export const deleteProperty = (
  propertyId: string,
  { config }: DeletePropertyRequestConfig,
) => API.delete(`properties/${propertyId}`, config)
