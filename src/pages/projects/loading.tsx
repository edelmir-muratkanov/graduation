import { Button, Skeleton } from '@/components/ui'

export const ProjectsLoading = () => {
  return (
    <div className='w-full h-full space-y-4'>
      <div className='flex justify-between items-center'>
        <span>Projects</span>
        <Button>Create new project</Button>
      </div>

      <div className='flex flex-col gap-4 w-full '>
        <div className='flex justify-between'>
          <Skeleton className='w-[200px] h-9' />
          <Skeleton className='w-[160px] h-9' />
        </div>
        <div className='rounded-md border'>
          <Skeleton className='w-full h-[410px]' />
        </div>
        <div className='flex items-center justify-center'>
          <div className='flex gap-2 mx-auto'>
            <Skeleton className='h-8 w-12' />
            <Skeleton className='h-8 w-12' />
            <Skeleton className='h-8 w-12' />
            <Skeleton className='h-8 w-12' />
          </div>
          <Skeleton className='self-end h-8 w-12 rounded-md' />
        </div>
      </div>
    </div>
  )
}
