import { z } from 'zod'

const zeroToUndefind = z.literal(0).transform(() => '')

const groupSchema = z.object({
  x: z.number({ invalid_type_error: 'required' }).or(zeroToUndefind),
  xMin: z.number({ invalid_type_error: 'required' }).or(zeroToUndefind),
  xMax: z.number({ invalid_type_error: 'required' }).or(zeroToUndefind),
})

const parameterSchema = z
  .object({
    first: groupSchema.optional(),
    second: groupSchema.optional(),
  })
  .refine(v => v.first || v.second, {
    message: 'First or second should be defined',
  })

const collectorType = z.union([z.literal('Terrigen'), z.literal('Carbonate')])

export const createMethodFormSchema = z.object({
  name: z.string().min(1, { message: 'required' }),
  collectorTypes: z.array(collectorType).nonempty({ message: 'required' }),
  data: z
    .object({
      propertyId: z.string().cuid(),
      parameters: parameterSchema,
    })
    .array(),
})

export type CreateMethodFormSchema = z.infer<typeof createMethodFormSchema>
