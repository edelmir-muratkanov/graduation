import { z } from 'zod'

export const updateProjectBaseFormSchema = z.object({
  name: z.string().min(1).nullable(),
  country: z.string().min(1).nullable(),
  operator: z.string().min(1).nullable(),
  type: z.number().nullable(),
  collectorType: z.number().nullable(),
})

export type UpdateProjectBaseFormSchema = z.infer<
  typeof updateProjectBaseFormSchema
>
