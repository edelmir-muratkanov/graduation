import { getRouteApi } from '@tanstack/react-router'

import { useGetMethodQuery, useGetPropertiesQuery } from '@/lib/api'

const route = getRouteApi('/methods/$methodId/')
export const useMethodPage = () => {
  const { methodId } = route.useParams()
  const methodQuery = useGetMethodQuery(methodId)
  const propertiesQuery = useGetPropertiesQuery()

  return { method: methodQuery.data, properties: propertiesQuery.data }
}
