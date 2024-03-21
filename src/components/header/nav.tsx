import { Link } from '@tanstack/react-router'

import { LogoIcon } from '../icons'

export const Nav = () => {
  return (
    <div className='mr-4 flex w-full'>
      <Link to='/' className='mr-6 flex items-center space-x-2'>
        <LogoIcon className='size-10' />
        <div>
          <span className='font-oswald font-semibold text-xl tracking-widest uppercase p-0'>
            Eor
          </span>
          <p className='text-sm font-oswald tracking-wide leading-none'>
            Selection
          </p>
        </div>
      </Link>

      <nav className='flex items-center'>
        <ul className='flex items-center gap-6 text-sm text-foreground/60 transition-all'>
          <li>
            <Link
              to='/projects'
              className='hover:text-foreground/80'
              activeProps={{
                className: 'text-foreground/90 font-semibold',
              }}
            >
              Projects
            </Link>
          </li>
          <li>
            <Link
              to='/methods'
              className='hover:text-foreground/80'
              activeProps={{
                className: 'text-foreground/90 font-semibold',
              }}
            >
              Methods
            </Link>
          </li>
        </ul>
      </nav>
    </div>
  )
}
