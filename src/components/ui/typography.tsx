import type { ComponentPropsWithoutRef, ElementRef } from 'react'
import { forwardRef } from 'react'
import { Slot } from '@radix-ui/react-slot'
import type { VariantProps } from 'class-variance-authority'
import { cva } from 'class-variance-authority'

import { cn } from '@/lib/cn'

type TextAsChildProps = {
  asChild: true
  as?: never
} & ComponentPropsWithoutRef<'span'>

type TextSpanProps = {
  as?: 'span'
  asChild?: false
} & ComponentPropsWithoutRef<'span'>

type TextDivProps = {
  as: 'div'
  asChild?: false
} & ComponentPropsWithoutRef<'div'>

type TextLabelProps = {
  as: 'label'
  asChild?: false
} & ComponentPropsWithoutRef<'label'>

type TextPProps = { as: 'p'; asChild?: false } & ComponentPropsWithoutRef<'p'>

const textVariants = cva('leading-7 [&:not(:first-child)]:mt-6')

type TextProps = VariantProps<typeof textVariants> &
  (
    | TextAsChildProps
    | TextSpanProps
    | TextDivProps
    | TextLabelProps
    | TextPProps
  )

const Text = forwardRef<ElementRef<'span'>, TextProps>(
  ({ asChild, as: Tag = 'span', className, children, ...props }, ref) => (
    <Slot className={cn(textVariants(), className)} ref={ref} {...props}>
      {asChild ? children : <Tag>{children}</Tag>}
    </Slot>
  ),
)

const headingVariants = cva('scroll-m-20 font-semibold tracking-tight', {
  variants: {
    variant: {
      muted: 'text-sm text-muted-foreground',
    },
    size: {
      default: 'text-base font-medium',
      large: 'text-2xl',
      small: 'text-sm font-medium leading-none',
    },
  },
  defaultVariants: {
    size: 'default',
  },
})

type HeadingAsChildProps = {
  asChild: true
  as?: never
} & ComponentPropsWithoutRef<'h1'>

type HeadingAsProps = {
  as?: 'h1' | 'h2' | 'h3' | 'h4' | 'h5' | 'h6'
  asChild?: false
} & ComponentPropsWithoutRef<'h1'>

type HeadingProps = VariantProps<typeof headingVariants> &
  (HeadingAsChildProps | HeadingAsProps)

const Heading = forwardRef<ElementRef<'h1'>, HeadingProps>(
  ({ className, asChild, children, as: Tag = 'h1', ...props }, ref) => (
    <Slot className={cn(headingVariants(), className)} ref={ref} {...props}>
      {asChild ? children : <Tag>{children}</Tag>}
    </Slot>
  ),
)

export { Heading, Text }
