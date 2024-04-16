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
      <div className='flex items-center justify-between'>
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
              <BreadcrumbPage>{state.project}</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
        <TabsList className=''>
          <TabsTrigger value='info'>Информация</TabsTrigger>
          <TabsTrigger value='calculations'>Расчеты</TabsTrigger>
        </TabsList>
      </div>

      <ProjectInfoLoading />
      <ProjectCalculationsLoading />
    </Tabs>
  )
}
