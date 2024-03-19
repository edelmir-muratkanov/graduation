import {
  Badge,
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
  Text,
} from '@/components/ui'

import { useMethodPage } from './useMethodPage'

export const MethodPage = () => {
  const { method, properties } = useMethodPage()

  return (
    <div className='w-full space-y-5'>
      <Card className='h-fit'>
        <CardHeader>
          <CardTitle>Base info</CardTitle>
        </CardHeader>
        <CardContent className='space-y-0'>
          <div className='space-x-2'>
            <Text>Name:</Text>
            <Text>{method.data.name}</Text>
          </div>
          <div className='space-x-2'>
            <Text>Collector types:</Text>
            {method.data.collectorTypes.map(type => (
              <Badge key={type}>{type}</Badge>
            ))}
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
                <TableHead>Property</TableHead>
                <TableHead>Min value</TableHead>
                <TableHead>Max value</TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              {method.data.parameters.map(parameter => (
                <TableRow key={parameter.propertyId}>
                  <TableCell>
                    {
                      properties.data.items.find(
                        item => item.id === parameter.propertyId,
                      )?.name
                    }
                  </TableCell>
                  <TableCell>
                    {parameter.parameters.first?.xMin
                      ? parameter.parameters.first.xMin
                      : parameter.parameters.second?.xMin}
                  </TableCell>
                  <TableCell>
                    {parameter.parameters.second?.xMax
                      ? parameter.parameters.second.xMax
                      : parameter.parameters.first?.xMax}
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
