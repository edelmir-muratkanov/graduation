import { z } from 'zod'

export const updatePropertyFormSchema = z.object({
  name: z.string().min(1).nullable(),
  unit: z.string().min(1).nullable(),
})

export type UpdatePropertyFormSchema = z.infer<typeof updatePropertyFormSchema>
