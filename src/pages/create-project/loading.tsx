import { Delete, Plus } from 'lucide-react'

import { Button, Heading, Skeleton } from '@/components/ui'

export const CreateProjectLoading = () => (
  <div className='mx-auto flex flex-col w-full justify-center space-y-6 sm:w-[650px]'>
    <div className='flex flex-col space-y-2 text-center'>
      <Heading as='h1' className='text-2xl font-semibold tracking-tight'>
        Create new project
      </Heading>
    </div>

    <form className='space-y-12'>
      <div className='space-y-2'>
        <Heading as='h2'>Base Info</Heading>
        <Skeleton className='w-full h-9' />
        <Skeleton className='w-full h-9' />
        <Skeleton className='w-full h-9' />
        <Skeleton className='w-full h-9' />
      </div>

      <div className='flex flex-col gap-2'>
        <Heading as='h2'>Parameters</Heading>
        <ul>
          <li className='flex gap-2 items-center justify-center'>
            <Skeleton className='h-9 w-full' />
            <Skeleton className='h-9 w-[190px]' />
            <Button type='button' variant='ghost' size='icon' className='mt-2'>
              <Delete className='size-5 my-auto' />
            </Button>
          </li>
        </ul>

        <Button
          type='button'
          variant='ghost'
          size='icon'
          className='self-center mt-2'
        >
          <Plus className='size-4' />
        </Button>
      </div>

      <Button className='w-full' size='lg' type='submit'>
        Create
      </Button>
    </form>
  </div>
)
