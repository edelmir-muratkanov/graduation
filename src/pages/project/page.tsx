import { Tabs, TabsList, TabsTrigger } from '@/components/ui'

import { ProjectCalculations } from './project-calculations/project-calculations'
import { ProjectInfo } from './project-info/project-info'

export const ProjectPage = () => {
  return (
    <Tabs className='w-full' defaultValue='info' orientation='vertical'>
      <TabsList>
        <TabsTrigger value='info'>Info</TabsTrigger>
        <TabsTrigger value='calculations'>Calculations</TabsTrigger>
      </TabsList>

      <ProjectInfo />
      <ProjectCalculations />
    </Tabs>
  )
}
