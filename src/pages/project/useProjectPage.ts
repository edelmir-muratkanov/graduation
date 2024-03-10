import { getRouteApi, useNavigate } from '@tanstack/react-router'

export type Tab = 'info' | 'calculations'

export const useProjectPage = () => {
  const { useSearch, useParams } = getRouteApi('/projects/$projectId/')
  const navigate = useNavigate()

  const { tab } = useSearch()
  const params = useParams()

  const hanleTabsValueChange = (value: string) =>
    navigate({
      to: '/projects/$projectId',
      params,
      search: {
        tab: value as Tab,
      },
    })

  return {
    state: { tab: tab as Tab },
    functions: { hanleTabsValueChange },
  }
}
