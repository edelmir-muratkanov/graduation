import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  Skeleton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui'

export const MethodLoading = () => {
  return (
    <div className='w-full space-y-5'>
      <Card className='h-fit'>
        <CardHeader>
          <CardTitle>Base info</CardTitle>
        </CardHeader>
        <CardContent className='space-y-1'>
          <Skeleton className='h-7' />
          <Skeleton className='h-7' />
        </CardContent>
      </Card>
      <Card>
        <CardHeader>
          <CardTitle>Parameters</CardTitle>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Property</TableHead>
                <TableHead>Min value</TableHead>
                <TableHead>Max value</TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              {Array.from({ length: 6 }, (_, index) => 0 + index).map(e => (
                <TableRow key={e}>
                  <TableCell>
                    <Skeleton className='h-8 w-[360px]' />
                  </TableCell>
                  <TableCell>
                    <Skeleton className='h-8 w-[136px]' />
                  </TableCell>
                  <TableCell>
                    <Skeleton className='h-8 w-[136px]' />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </div>
  )
}
