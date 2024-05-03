import { Skeleton, TabsContent } from '@/components/ui'

export const ProjectCalculationsLoading = () => {
  return (
    <TabsContent value='calculations'>
      <Skeleton className='h-[400px]' />
    </TabsContent>
  )
}
