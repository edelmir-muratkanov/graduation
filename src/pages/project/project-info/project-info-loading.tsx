import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  Skeleton,
  TabsContent,
} from '@/components/ui'

export const ProjectInfoLoading = () => {
  return (
    <TabsContent value='info' className='space-y-5'>
      <Card className='h-fit'>
        <CardHeader>
          <CardTitle>Base Info</CardTitle>
        </CardHeader>

        <CardContent>
          <div className='flex flex-col space-y-3'>
            {Array(3)
              .fill(1)
              .map(() => (
                <Skeleton className='h-[27px]' />
              ))}
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Parameters</CardTitle>
        </CardHeader>
        <CardContent className='space-y-3'>
          {Array(10)
            .fill(1)
            .map(() => (
              <Skeleton className='h-[27px]' />
            ))}
        </CardContent>
      </Card>
    </TabsContent>
  )
}
