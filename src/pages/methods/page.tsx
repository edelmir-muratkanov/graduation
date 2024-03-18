import { Link } from '@tanstack/react-router'

import { Button, Heading } from '@/components/ui'
import { useProfile } from '@/lib/contexts'

import { MethodsTable } from './methods-table/methods-table'

export const MethodsPage = () => {
  const { user } = useProfile()
  return (
    <div className='w-full h-full space-y-4'>
      <div className='flex justify-between items-center'>
        <Heading as='h1'>Methods</Heading>
        {user?.role === 'Admin' ? (
          <Link to='/methods/new'>
            <Button variant='secondary'>Create new method</Button>
          </Link>
        ) : null}
      </div>

      <MethodsTable />
    </div>
  )
}
