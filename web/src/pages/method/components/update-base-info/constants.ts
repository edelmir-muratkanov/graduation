import { z } from 'zod'

export const updateMethodBaseFormSchema = z.object({
  name: z.string().min(1).nullable(),
  collectorTypes: z.array(z.number()).nonempty().nullable(),
})

export type UpdateMethodBaseFormSchema = z.infer<
  typeof updateMethodBaseFormSchema
>
