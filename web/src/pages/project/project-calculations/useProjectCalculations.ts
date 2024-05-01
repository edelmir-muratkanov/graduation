import { useParams } from '@tanstack/react-router'

import { useGetProjectCalculationsQuery, useGetProjectQuery } from '@/lib/api'

export const useProjectCalculations = () => {
  const { projectId } = useParams({ from: '/projects/$projectId/' })
  const getProjectCalculationsQuery = useGetProjectCalculationsQuery(projectId)
  const projectQuery = useGetProjectQuery(projectId)

  const acceptableParameters = getProjectCalculationsQuery.data.data.map(c => ({
    allParameters: c.items.length,
    acceptableParameters: c.items.filter(i => i.ratio >= 0.25).length,
    methodName: c.name,
  }))

  return {
    calculations: getProjectCalculationsQuery.data.data,
    acceptableParameters,
    project: projectQuery.data.data,
  }
}
