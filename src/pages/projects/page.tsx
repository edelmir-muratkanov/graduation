import { Link } from '@tanstack/react-router'

import { Button, Heading } from '@/components/ui'

import { ProjectsTable } from './components/projects-table/projects-table'

export const ProjectsPage = () => {
  return (
    <div className='w-full h-full space-y-4'>
      <div className='flex justify-between items-center'>
        <Heading as='h1'>Projects</Heading>
        <Link to='/projects/new'>
          <Button variant='secondary'>Create new project</Button>
        </Link>
      </div>
      <ProjectsTable />
    </div>
  )
}
