import { z } from 'zod'

export const createProjectFormSchema = z.object({
  name: z.string().trim().min(1, { message: 'required' }),
  country: z.string().trim().min(1, { message: 'required' }),
  operator: z.string().trim().min(1, { message: 'required' }),
  projectType: z.union([z.literal('Ground'), z.literal('Shelf')]),
  collectorType: z.union([z.literal('Terrigen'), z.literal('Carbonate')]),
  methodIds: z
    .array(
      z.object({
        methodId: z.string().trim().cuid(),
      }),
    )
    .nonempty({ message: 'required' }),
  parameters: z
    .array(
      z.object({
        propertyId: z.string().trim().cuid(),
        value: z.number(),
      }),
    )
    .nonempty({ message: 'required' }),
})

export type CreateProjectFormSchema = z.infer<typeof createProjectFormSchema>
