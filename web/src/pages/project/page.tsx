import { Link } from '@tanstack/react-router'

import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
  Tabs,
  TabsList,
  TabsTrigger,
} from '@/components/ui'
import { useProfile } from '@/lib/contexts'

import { AddProjectMethods } from './components/add-methods/add-project-methods'
import { AddProjectParameters } from './components/add-project-parameters/add-project-parameters'
import { DeleteProject } from './components/delete-project'
import { ProjectCalculations } from './components/project-calculations/project-calculations'
import { ProjectInfo } from './components/project-info/project-info'
import { useProjectPage } from './useProjectPage'

export const ProjectPage = () => {
  const { state, functions } = useProjectPage()
  const { user } = useProfile()

  const isOwner = state.project.ownerId === user?.id
  const isMember = state.project.members.some(m => m.id === user?.id)
  const isOwnerOrMember = isOwner || isMember

  return (
    <div className='w-full space-y-4'>
      <Breadcrumb>
        <BreadcrumbList>
          <BreadcrumbItem>
            <BreadcrumbLink asChild>
              <Link to='/'>Главная</Link>
            </BreadcrumbLink>
          </BreadcrumbItem>
          <BreadcrumbSeparator />
          <BreadcrumbItem>
            <BreadcrumbLink asChild>
              <Link to='/projects'>Проекты</Link>
            </BreadcrumbLink>
          </BreadcrumbItem>
          <BreadcrumbSeparator />
          <BreadcrumbItem>
            <BreadcrumbPage>{state.project.name}</BreadcrumbPage>
          </BreadcrumbItem>
        </BreadcrumbList>
      </Breadcrumb>

      <div className='w-full flex gap-x-4'>
        <Tabs
          className='w-full space-y-4'
          value={state.tab}
          defaultValue='info'
          onValueChange={functions.hanleTabsValueChange}
          orientation='vertical'
        >
          <div className='flex items-center justify-between mb-8'>
            <TabsList className=''>
              <TabsTrigger value='info'>Информация</TabsTrigger>
              <TabsTrigger value='calculations'>Расчеты</TabsTrigger>
            </TabsList>
          </div>

          <ProjectInfo isOwnerOrMember={isOwnerOrMember} />
          <ProjectCalculations />
        </Tabs>

        {isOwnerOrMember && (
          <div className='flex flex-col h-full space-y-4'>
            {isOwnerOrMember && (
              <>
                <AddProjectParameters />
                <AddProjectMethods />
              </>
            )}
            {isOwner && <DeleteProject projectId={state.project.id} />}
          </div>
        )}
      </div>
    </div>
  )
}
