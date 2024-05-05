import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Skeleton } from '@/components/ui/skeleton'
import { TabsContent } from '@/components/ui/tabs'

export const ProjectInfoLoading = () => {
  return (
    <TabsContent value='info' className='space-y-5'>
      <Card className='h-fit'>
        <CardHeader>
          <CardTitle>Базовая информация</CardTitle>
        </CardHeader>

        <CardContent>
          <div className='flex flex-col space-y-3'>
            {Array(3)
              .fill(1)
              .map(v => (
                <Skeleton key={v} className='h-[27px]' />
              ))}
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Список параметров</CardTitle>
        </CardHeader>
        <CardContent className='space-y-3'>
          {Array(10)
            .fill(1)
            .map(v => (
              <Skeleton key={v} className='h-[27px]' />
            ))}
        </CardContent>
      </Card>
    </TabsContent>
  )
}
