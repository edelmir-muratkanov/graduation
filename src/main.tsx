import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'

import '@/assets/index.css'

const root = document.getElementById('root')!

createRoot(root).render(
  <StrictMode>
    <div>Hello world</div>
  </StrictMode>,
)
