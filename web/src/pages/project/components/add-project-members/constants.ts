import { z } from 'zod'

export const addMembersFormSchema = z.object({
  data: z.string().uuid().array().nonempty(),
})

export type AddMembersFormSchema = z.infer<typeof addMembersFormSchema>
