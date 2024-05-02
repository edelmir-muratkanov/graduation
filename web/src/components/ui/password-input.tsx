import { forwardRef, useState } from 'react'
import { EyeIcon, EyeOffIcon } from 'lucide-react'

import { cn } from '@/lib/cn'

import { Button } from './button'
import type { InputProps } from './input'
import { Input } from './input'

const PasswordInput = forwardRef<HTMLInputElement, InputProps>(
  ({ className, ...props }, ref) => {
    const [showPassword, setShowPassword] = useState(false)

    return (
      <Input
        type={showPassword ? 'text' : 'password'}
        className={cn('pr-10 h-9', className)}
        ref={ref}
        endIcon={
          <Button
            type='button'
            variant='ghost'
            size='sm'
            className=' h-full px-3 py-2 hover:bg-transparent'
            onClick={() => setShowPassword(!showPassword)}
            disabled={!props.value || props.disabled}
          >
            {showPassword ? (
              <EyeOffIcon className='h-4 w-4' aria-hidden='true' />
            ) : (
              <EyeIcon className='h-4 w-4' aria-hidden='true' />
            )}
          </Button>
        }
        {...props}
      />
    )
  },
)

PasswordInput.displayName = 'PasswordInput'

export { PasswordInput }
