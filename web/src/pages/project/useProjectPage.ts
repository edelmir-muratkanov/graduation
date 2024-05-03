import { getRouteApi, useNavigate } from '@tanstack/react-router'

import { useGetProjectQuery } from '@/lib/api'

export type Tab = 'info' | 'calculations'

export const useProjectPage = () => {
  const { useSearch, useParams } = getRouteApi('/projects/$projectId/')
  const navigate = useNavigate()

  const { tab = 'info' } = useSearch()
  const params = useParams()
  const getProjectQuery = useGetProjectQuery(params.projectId)

  const hanleTabsValueChange = (value: string) =>
    navigate({
      to: '/projects/$projectId',
      params,
      search: {
        tab: value as Tab,
      },
    })

  return {
    state: { tab, project: getProjectQuery.data.data },
    functions: { hanleTabsValueChange },
  }
}
