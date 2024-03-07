import { Button } from '@/components/ui'

import { ProjectsTable } from './components/projects-table/projects-table'

export const ProjectsPage = () => {
  return (
    <div className='w-full h-full space-y-4'>
      <div className='flex justify-between items-center'>
        <span>Projects</span>
        <Button>Create new project</Button>
      </div>
      <ProjectsTable />
    </div>
  )
}
