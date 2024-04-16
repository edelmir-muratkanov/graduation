import { Heading } from '@/components/ui'

import { CreateProjectForm } from './create-project-form/create-project-form'

export const CreateProjectPage = () => {
  return (
    <div className='mx-auto flex flex-col w-full justify-center space-y-6 sm:w-[650px]'>
      <div className='flex flex-col space-y-2 text-center'>
        <Heading as='h1' className='text-2xl font-semibold tracking-tight'>
          Создать новый проект
        </Heading>
      </div>
      <CreateProjectForm />
    </div>
  )
}
