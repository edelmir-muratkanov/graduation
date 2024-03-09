import { useParams } from '@tanstack/react-router'

import { useGetProjectCalculationsQuery } from '@/lib/api'

export const useProjectCalculations = () => {
  const { projectId } = useParams({ from: '/projects/$projectId/' })
  const getProjectCalculationsQuery = useGetProjectCalculationsQuery(projectId)

  return { state: { data: getProjectCalculationsQuery.data } }
}
