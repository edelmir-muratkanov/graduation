import { z } from 'zod'

export const addProrjectParametersFormSchema = z.object({
  data: z
    .object({
      propertyId: z.string().uuid(),
      value: z.number().refine(v => v !== 0, {
        message: 'Значение не может быть 0',
      }),
    })
    .array()
    .nonempty(),
})

export type AddProjectParametersFormSchema = z.infer<
  typeof addProrjectParametersFormSchema
>
