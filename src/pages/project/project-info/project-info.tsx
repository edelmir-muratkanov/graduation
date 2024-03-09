import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  Table,
  TableBody,
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
              <TableRow>
                <TableHead>Name</TableHead>
                <TableHead>Value</TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              {data.data.parameters.map(parameter => (
                <TableRow key={parameter.property.id}>
                  <TableCell>{parameter.property.name}</TableCell>
                  <TableCell>{parameter.value}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </TabsContent>
  )
}
