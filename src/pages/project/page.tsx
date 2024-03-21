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

import { ProjectCalculations } from './project-calculations/project-calculations'
import { ProjectInfo } from './project-info/project-info'
import { useProjectPage } from './useProjectPage'

export const ProjectPage = () => {
  const { state, functions } = useProjectPage()

  return (
    <Tabs
      className='w-full space-y-4'
      value={state.tab}
      defaultValue='info'
      onValueChange={functions.hanleTabsValueChange}
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

      <ProjectInfo />
      <ProjectCalculations />
    </Tabs>
  )
}
