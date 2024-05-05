import { Skeleton } from '@/components/ui/skeleton'
import { TabsContent } from '@/components/ui/tabs'

export const ProjectCalculationsLoading = () => {
  return (
    <TabsContent value='calculations'>
      <Skeleton className='h-[400px]' />
    </TabsContent>
  )
}
