import { Skeleton } from '@/components/ui/skeleton'

export const AuthLoading = () => {
  return (
    <div className='mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px]'>
      <div className='flex flex-col space-y-2 text-center'>
        <h1 className='text-2xl font-semibold tracking-tight'>
          Login to your account
        </h1>
        <p className='text-sm text-muted-foreground'>
          Enter your email and password
        </p>
      </div>
      <div>
        <div className='flex flex-col gap-4'>
          <Skeleton className='h-9' />
          <Skeleton className='h-9' />
          <Skeleton className='h-9' />
        </div>
        <div className='flex justify-center py-3'>
          <Skeleton className='h-4 w-[200px]' />
        </div>
      </div>
    </div>
  )
}
