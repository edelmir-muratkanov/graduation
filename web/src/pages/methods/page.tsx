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
import { useProfile } from '@/lib/contexts'

import { MethodsTable } from './methods-table/methods-table'

export const MethodsPage = () => {
  const { user } = useProfile()
  return (
    <div className='w-full space-y-4'>
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
              <BreadcrumbPage>Методы</BreadcrumbPage>
            </BreadcrumbItem>
          </BreadcrumbList>
        </Breadcrumb>
        {user?.role === 'Admin' ? (
          <Link to='/methods/new'>
            <Button variant='secondary'>Создать новый метод</Button>
          </Link>
        ) : null}
      </div>

      <MethodsTable />
    </div>
  )
}
