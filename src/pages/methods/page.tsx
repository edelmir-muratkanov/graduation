import { Link } from '@tanstack/react-router'

import { Button, Heading } from '@/components/ui'

import { MethodsTable } from './methods-table/methods-table'

export const MethodsPage = () => {
  return (
    <div className='w-full h-full space-y-4'>
      <div className='flex justify-between items-center'>
        <Heading as='h1'>Methods</Heading>
        <Link to='/methods/new'>
          <Button variant='secondary'>Create new method</Button>
        </Link>
      </div>

      <MethodsTable />
    </div>
  )
}
