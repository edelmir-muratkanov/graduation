import { Link } from '@tanstack/react-router'

import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbSeparator,
  Tabs,
  TabsList,
  TabsTrigger,
} from '@/components/ui'

import { ProjectCalculationsLoading } from './components/project-calculations/project-calculations-loading'
import { ProjectInfoLoading } from './components/project-info/project-info-loading'

export const ProjectLoading = () => {
  return (
    <Tabs defaultValue='info' className='w-full' orientation='vertical'>
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
