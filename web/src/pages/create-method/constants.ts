import { z } from 'zod'

const zeroToUndefind = z.literal(0).transform(() => '')

const required = 'Обязательное поле'

const groupSchema = z.object({
  avg: z.number({ invalid_type_error: required }).or(zeroToUndefind),
  min: z.number({ invalid_type_error: required }).or(zeroToUndefind),
  max: z.number({ invalid_type_error: required }).or(zeroToUndefind),
})

const collectorType = z.number({ invalid_type_error: required })

export const createMethodFormSchema = z.object({
  name: z.string().trim().min(1, required),
  collectorTypes: z.array(collectorType).nonempty({ message: required }),
  parameters: z
    .object({
      propertyId: z.string().trim().min(1, required),
      first: groupSchema.optional(),
      second: groupSchema.optional(),
    })
    .refine(v => v.first || v.second, {
      message: 'Должно иметь x1 или x2',
    })
    .array()
    .nonempty({ message: 'Должен иметь хотя бы один параметр' }),
})

export type CreateMethodFormSchema = z.infer<typeof createMethodFormSchema>
