import { useRouter } from '@tanstack/react-router'

import { Button, Heading, Text } from '../ui'

export const NotFound = () => {
  const router = useRouter()
  return (
    <div className='w-full flex flex-col gap-4 justify-center items-center h-screen'>
      <Heading
        as='h1'
        className='text-7xl border-1 font-mono font-semibold proportional-nums'
      >
        404
      </Heading>
      <Text className='text-xl m-0 uppercase tracking-wide'>
        Oops! Nothing was found
      </Text>
      <Button
        variant='link'
        className='text-lg uppercase'
        onClick={() => router.history.back()}
      >
        Back
      </Button>
    </div>
  )
}
