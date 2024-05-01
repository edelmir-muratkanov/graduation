import { z } from 'zod'

const required = 'Обязательное поле'

export const projectType = z.number()
export const collectorType = z.number()

export const createProjectFormSchema = z.object({
  name: z.string().trim().min(1, { message: required }),
  country: z.string().trim().min(1, { message: required }),
  operator: z.string().trim().min(1, { message: required }),
  type: projectType,
  collectorType,
  methodIds: z
    .array(z.string().trim().uuid(), {
      errorMap: () => ({ message: required }),
    })
    .nonempty({ message: required }),
  parameters: z
    .array(
      z.object({
        propertyId: z.string().trim().uuid({ message: required }),
        value: z.number({
          required_error: required,
          invalid_type_error: required,
        }),
      }),
    )
    .nonempty({ message: required }),
})

export type CreateProjectFormSchema = z.infer<typeof createProjectFormSchema>
