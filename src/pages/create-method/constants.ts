import { z } from 'zod'

const zeroToUndefind = z.literal(0).transform(() => '')

const required = 'Обязательное поле'

const groupSchema = z.object({
  x: z.number({ invalid_type_error: required }).or(zeroToUndefind),
  xMin: z.number({ invalid_type_error: required }).or(zeroToUndefind),
  xMax: z.number({ invalid_type_error: required }).or(zeroToUndefind),
})

const parameterSchema = z
  .object({
    first: groupSchema.optional(),
    second: groupSchema.optional(),
  })
  .refine(v => v.first || v.second, {
    message: 'Должно иметь x1 или x2',
  })

const collectorType = z.union([z.literal('Terrigen'), z.literal('Carbonate')])

export const createMethodFormSchema = z.object({
  name: z.string().min(1, { message: required }),
  collectorTypes: z.array(collectorType).nonempty({ message: required }),
  data: z
    .object({
      propertyId: z.string().cuid({ message: required }),
      parameters: parameterSchema,
    })
    .array()
    .nonempty({ message: 'Должен иметь хотя бы один параметр' }),
})

export type CreateMethodFormSchema = z.infer<typeof createMethodFormSchema>
