import { z } from 'zod'

export const createPropertyFormSchema = z.object({
  name: z.string().min(1),
  unit: z.string().min(1),
})

export type CreatePropertyFormSchema = z.infer<typeof createPropertyFormSchema>
