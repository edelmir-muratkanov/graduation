import { getRouteApi, useNavigate } from '@tanstack/react-router'

import { useGetProjectQuery } from '@/lib/api'

export type Tab = 'info' | 'calculations'

export const useProjectPage = () => {
  const { useSearch, useParams } = getRouteApi('/projects/$projectId/')
  const navigate = useNavigate()

  const { tab = 'info' } = useSearch()
  const params = useParams()
  const {
    data: {
      data: { name },
    },
  } = useGetProjectQuery(params.projectId)

  const hanleTabsValueChange = (value: Tab) =>
    navigate({
      to: '/projects/$projectId',
      params,
      search: {
        tab: value,
      },
    })

  return {
    state: { tab, project: name },
    functions: { hanleTabsValueChange },
  }
}
