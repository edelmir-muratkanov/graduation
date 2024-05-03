import type { InputHTMLAttributes, ReactNode } from 'react'
import { forwardRef } from 'react'

import { cn } from '@/lib/cn'

export interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  startIcon?: ReactNode
  endIcon?: ReactNode
}

const Input = forwardRef<HTMLInputElement, InputProps>(
  ({ className, type, startIcon, endIcon, ...props }, ref) => {
    const hasIcon = Boolean(startIcon) || Boolean(endIcon)
    return (
      <>
        {hasIcon ? (
          <div
            className='flex items-center justify-center gap-2  h-10 rounded-md border border-input bg-transparent ring-offset-background focus-within:ring-1 focus-within:ring-ring focus-within:ring-offset-2 data-[disabled=true]:cursor-not-allowed data-[disabled=true]:opacity-50'
            data-disabled={props.disabled}
          >
            {startIcon && (
              <div className={cn('text-muted-foreground pl-3')}>
                {startIcon}
              </div>
            )}
            <input
              type={type}
              className={cn(
                'flex h-full w-full rounded-md bg-transparent px-4 py-2 text-sm file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground shadow-none outline-none border-none focus-visible:outline-none focus-visible:border-none focus-visible:shadow-none',
                className,
              )}
              ref={ref}
              {...props}
            />
            {endIcon && (
              <div className={cn('text-muted-foreground pr-3')}>{endIcon}</div>
            )}
          </div>
        ) : (
          <input
            type={type}
            className={cn(
              'flex h-9 w-full rounded-md border border-input bg-transparent px-4 py-2 text-sm ring-offset-background file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50',
              className,
            )}
            ref={ref}
            {...props}
          />
        )}
      </>
    )
  },
)
Input.displayName = 'Input'

export { Input }
