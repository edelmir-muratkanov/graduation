import { getRouteApi, useNavigate } from '@tanstack/react-router'

export const useProjectPage = () => {
  const { useSearch } = getRouteApi('/projects/$projectId/')
  const navigate = useNavigate()

  const { tab } = useSearch()

  const hanleTabsValueChange = (value: string) =>
    navigate({
      search: {
        tab: value,
      },
    })

  return { state: { tab }, functions: { hanleTabsValueChange } }
}
