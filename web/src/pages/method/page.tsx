import { Link } from '@tanstack/react-router'

import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui'
import { cn } from '@/lib/cn'
import { useProfile } from '@/lib/contexts'

import { BaseInfo } from './components/base-info'
import { ParametersList } from './components/parameters-list'
import { UpdateBaseInfo } from './components/update-base-info/update-base-info'
import { useMethodPage } from './useMethodPage'

export const MethodPage = () => {
  const { methodData } = useMethodPage()
  const { user } = useProfile()
  const isAdmin = user?.role === 'Admin'

  return (
    <div className='w-full space-y-4'>
      <Breadcrumb className='p-2'>
        <BreadcrumbList>
          <BreadcrumbItem>
            <BreadcrumbLink asChild>
              <Link to='/'>Главная</Link>
            </BreadcrumbLink>
          </BreadcrumbItem>
          <BreadcrumbSeparator />
          <BreadcrumbItem>
            <BreadcrumbLink asChild>
              <Link to='/methods'>Методы</Link>
            </BreadcrumbLink>
          </BreadcrumbItem>
          <BreadcrumbSeparator />
          <BreadcrumbItem>
            <BreadcrumbPage>{methodData.data.name}</BreadcrumbPage>
          </BreadcrumbItem>
        </BreadcrumbList>
      </Breadcrumb>

      <div className='w-full flex space-x-6'>
        <div className={cn('w-full space-y-5', isAdmin && 'w-[80%]')}>
          <BaseInfo method={methodData.data} />
          <ParametersList method={methodData.data} />
        </div>

        {isAdmin && (
          <div>
            <UpdateBaseInfo />
          </div>
        )}
      </div>
    </div>
  )
}
