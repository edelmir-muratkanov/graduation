import { useParams } from '@tanstack/react-router'

import { useGetProjectQuery } from '@/lib/api'

export const useProjectInfo = () => {
  const { projectId } = useParams({ from: '/projects/$projectId/' })
  const getProjectQuery = useGetProjectQuery(projectId)

  return { data: getProjectQuery.data }
}
