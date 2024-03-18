import { Heading, Skeleton } from '@/components/ui'

export const CreateMethodLoading = () => {
  return (
    <div className='mx-auto flex flex-col w-full justify-center space-y-6 sm:w-[650px]'>
      <div className='flex flex-col space-y-2 text-center'>
        <Heading as='h1' className='text-2xl font-semibold tracking-tight'>
          Create new method
        </Heading>
      </div>

      <div className='space-y-10'>
        <div className='space-y-2'>
          <Skeleton className='h-9' />
          <Skeleton className='h-9' />
        </div>

        <div className='flex flex-col'>
          <Heading>Parameters</Heading>
        </div>

        <div className='flex justify-between items-center gap-2 *:w-full'>
          <Skeleton className='h-9' />
          <Skeleton className='h-9' />
        </div>
      </div>
    </div>
  )
}
