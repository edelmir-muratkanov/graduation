import { Link } from '@tanstack/react-router'

import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb'
import { Button } from '@/components/ui/button'
import { Skeleton } from '@/components/ui/skeleton'

export const ProjectsLoading = () => {
  return (
    <div className='w-full space-y-6'>
      <div className='flex justify-between items-center'>
        <Breadcrumb>
          <BreadcrumbList>
            <BreadcrumbItem>
              <BreadcrumbLink asChild>
                <Link to='/'>Главная</Link>
              </BreadcrumbLink>
            </BreadcrumbItem>
            <BreadcrumbSeparator />
            <BreadcrumbItem>
              <BreadcrumbPage>Проекты</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
        <Link to='/projects/new'>
          <Button variant='secondary'>Создать новый проект</Button>
        </Link>
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
