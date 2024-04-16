import { useParams } from '@tanstack/react-router'

import { useGetProjectCalculationsQuery, useGetProjectQuery } from '@/lib/api'

export const useProjectCalculations = () => {
  const { projectId } = useParams({ from: '/projects/$projectId/' })
  const getProjectCalculationsQuery = useGetProjectCalculationsQuery(projectId)
  const getProjectQuery = useGetProjectQuery(projectId)

  const acceptableParameters = getProjectCalculationsQuery.data.data.map(c => ({
    allParameters: c.items.length,
    acceptableParameters: c.items.filter(i => i.ratio >= 0.25).length,
    methodName: c.method.name,
  }))

  return {
    calculations: getProjectCalculationsQuery.data.data,
    properties: getProjectQuery.data.data.parameters,
    acceptableParameters,
  }
}
