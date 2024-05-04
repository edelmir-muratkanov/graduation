import { Link } from '@tanstack/react-router'

import { Button, Text } from '@/components/ui'

export const HomePage = () => {
  return (
    <div className='w-full flex items-center justify-between gap-x-7'>
      <div className='w-[700px] flex flex-col'>
        <h1 className='text-6xl font-semibold font-poppins'>
          Подбор МУН - просто!
        </h1>

        <Text className='font-poppins mb-10'>
          Исследуйте инновационные технологии и методы для повышения
          эффективности добычи нефти на нашем сайте
        </Text>

        <Link to='/projects/new'>
          <Button size='lg' variant='secondary' className='w-full'>
            Начать проект
          </Button>
        </Link>
      </div>

      <img src='/home-background.png' alt='Background' />
    </div>
  )
}
