import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  Table,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
  TabsContent,
  Text,
} from '@/components/ui'

import { useProjectInfo } from './useProjectInfo'

export const ProjectInfo = () => {
  const { data } = useProjectInfo()
  return (
    <TabsContent value='info' className='space-y-5'>
      <Card className='h-fit'>
        <CardHeader>
          <CardTitle>Base Info</CardTitle>
        </CardHeader>

        <CardContent>
          <div className='flex flex-col space-y-0'>
            <Text>Name: {data.data.name}</Text>
            <Text>Operator: {data.data.operator}</Text>
            <Text>Country: {data.data.country}</Text>
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Parameters</CardTitle>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableHead>Name</TableHead>
              <TableHead>Value</TableHead>
            </TableHeader>

            {data.data.parameters.map(parameter => (
              <TableRow>
                <TableCell>{parameter.property.name}</TableCell>
                <TableCell>{parameter.value}</TableCell>
              </TableRow>
            ))}
          </Table>
        </CardContent>
      </Card>
    </TabsContent>
  )
}
