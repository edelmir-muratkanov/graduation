import { getRouteApi } from '@tanstack/react-router'

import { useGetMethodQuery } from '@/lib/api'

const route = getRouteApi('/methods/$methodId/')
export const useMethodPage = () => {
  const { methodId } = route.useParams()
  const methodQuery = useGetMethodQuery(methodId)

  return { methodId, methodData: methodQuery.data }
}
