import { Tabs, TabsList, TabsTrigger } from '@/components/ui'

import { ProjectCalculationsLoading } from './project-calculations/project-calculations-loading'
import { ProjectInfoLoading } from './project-info/project-info-loading'
import { useProjectPage } from './useProjectPage'

export const ProjectLoading = () => {
  const { state, functions } = useProjectPage()
  return (
    <Tabs
      value={state.tab}
      defaultValue='info'
      onValueChange={functions.hanleTabsValueChange}
      className='w-full'
      orientation='vertical'
    >
      <TabsList>
        <TabsTrigger value='info'>Info</TabsTrigger>
        <TabsTrigger value='calculations'>Calculations</TabsTrigger>
      </TabsList>

      <ProjectInfoLoading />
      <ProjectCalculationsLoading />
    </Tabs>
  )
}
