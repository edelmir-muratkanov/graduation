import { createLazyFileRoute } from '@tanstack/react-router'

import { useProfile } from '@/lib/contexts'

const Home = () => {
  const { user } = useProfile()
  return <div>{user?.email}</div>
}

export const Route = createLazyFileRoute('/')({
  component: () => <Home />,
})
