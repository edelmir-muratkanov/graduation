import { useParams } from '@tanstack/react-router'

import { useGetProjectCalculationsQuery } from '@/lib/api'

import { useProjectPage } from '../../useProjectPage'

export const useProjectCalculations = () => {
  const { projectId } = useParams({ from: '/projects/$projectId/' })
  const getProjectCalculationsQuery = useGetProjectCalculationsQuery(projectId)
  const { state } = useProjectPage()

  const acceptableParameters = getProjectCalculationsQuery.data.data.map(c => ({
    allParameters: c.items.length,
    acceptableParameters: c.items.filter(i => i.ratio >= 0.25).length,
    methodName: c.name,
  }))

  return {
    calculations: getProjectCalculationsQuery.data.data,
    acceptableParameters,
    project: state.project,
  }
}
