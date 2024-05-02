import { z } from 'zod'

const parameterSchema = z.object({
  min: z.number(),
  avg: z.number(),
  max: z.number(),
})

export const addParametersFormSchema = z.object({
  data: z
    .object({
      propertyId: z.string().uuid(),
      first: parameterSchema.nullable(),
      second: parameterSchema.nullable(),
    })
    .refine(v => v.first || v.second, {
      message: 'Должено иметь x1 или x2',
    })
    .array()
    .nonempty(),
})
export type AddParametersFormSchema = z.infer<typeof addParametersFormSchema>
