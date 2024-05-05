import { useRouter } from '@tanstack/react-router'

import { Button } from '../ui/button'
import { Heading, Text } from '../ui/typography'

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
        Упс! Ничего не найдено.
      </Text>
      <Button
        variant='link'
        className='text-base uppercase'
        onClick={() => router.history.back()}
      >
        Вернуться назад
      </Button>
    </div>
  )
}
