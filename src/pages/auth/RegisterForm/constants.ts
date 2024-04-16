import { z } from 'zod'

export const registerFormSchema = z
  .object({
    email: z.string().email(),
    password: z.string().min(6),
    confirmPassword: z.string(),
  })
  .refine(data => data.password === data.confirmPassword, {
    message: 'Passwords should match',
    path: ['confirmPassword'],
  })
