export const Footer = () => {
  return (
    <footer className='border-t'>
      <div className='container flex flex-col items-center justify-between gap-4 md:h-24 md:flex-row'>
        <p className='text-balance text-center text-sm leading-loose text-muted-foreground md:text-left'>
          Built by{' '}
          <a
            href='https://github.com/edelmir-muratkanov'
            target='_blank'
            rel='noreferrer'
            className='font-medium underline underline-offset-4'
          >
            Edelmir
          </a>
          . The source code is available on{' '}
          <a
            href='https://github.com/edelmir-muratkanov/graduation-web'
            target='_blank'
            rel='noreferrer'
            className='font-medium underline underline-offset-4'
          >
            GitHub
          </a>
          .
        </p>
      </div>
    </footer>
  )
}
