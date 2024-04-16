import { z } from 'zod'

const required = 'Обязательное поле'

export const createProjectFormSchema = z.object({
  name: z.string().trim().min(1, { message: required }),
  country: z.string().trim().min(1, { message: required }),
  operator: z.string().trim().min(1, { message: required }),
  projectType: z.union([
    z.literal('Ground', { errorMap: () => ({ message: required }) }),
    z.literal('Shelf', { errorMap: () => ({ message: required }) }),
  ]),
  collectorType: z.union([
    z.literal('Terrigen', { errorMap: () => ({ message: required }) }),
    z.literal('Carbonate', { errorMap: () => ({ message: required }) }),
  ]),
  methodIds: z
    .array(
      z.object({
        methodId: z.string().trim().cuid(),
      }),
      { errorMap: () => ({ message: required }) },
    )
    .nonempty({ message: required }),
  parameters: z
    .array(
      z.object({
        propertyId: z.string().trim().cuid({ message: required }),
        value: z.number({
          required_error: required,
          invalid_type_error: required,
        }),
      }),
    )
    .nonempty({ message: required }),
})

export type CreateProjectFormSchema = z.infer<typeof createProjectFormSchema>
