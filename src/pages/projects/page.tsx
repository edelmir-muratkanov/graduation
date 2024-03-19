import { Link } from '@tanstack/react-router'

import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
  Button,
} from '@/components/ui'

import { ProjectsTable } from './components/projects-table/projects-table'

export const ProjectsPage = () => {
  return (
    <div className='w-full space-y-6'>
      <div className='flex justify-between items-center'>
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbLink asChild>
                <Link to='/'>Home</Link>
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbPage>Projects</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
        <Link to='/projects/new'>
          <Button variant='secondary'>Create new project</Button>
        </Link>
      </div>
      <ProjectsTable />
    </div>
  )
}
