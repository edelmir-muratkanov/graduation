import { z } from 'zod'

export const addMethodsFormSchema = z.object({
  data: z.string().uuid().array().nonempty(),
})

export type AddMethodsFormSchema = z.infer<typeof addMethodsFormSchema>
