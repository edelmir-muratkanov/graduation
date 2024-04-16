import { Link } from '@tanstack/react-router'

import { Button } from '@/components/ui'

export const HomePage = () => {
  return (
    <div className='w-full flex  items-center gap-7'>
      <div className='w-[900px]'>
        <h1 className='text-6xl font-poppins mb-9'>Добро пожаловать!</h1>

        <h2 className='text-4xl font-poppins mb-9'>
          Сервис по подбору методов повышения нефтеотдачи
        </h2>

        <p className='mb-4 text-xl font-poppins'>
          Начните с создания проекта и выберите интерсующие вас методы
        </p>

        <Link to='/projects/new'>
          <Button size='lg' variant='secondary' className='w-full'>
            Начать проект
          </Button>
        </Link>
      </div>
    </div>
  )
}
