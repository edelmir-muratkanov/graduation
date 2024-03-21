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
import { CollectorTypeTranslates, ProjectTypeTranslates } from '@/lib/constants'

import { useProjectInfo } from './useProjectInfo'

export const ProjectInfo = () => {
  const { data } = useProjectInfo()
  return (
    <TabsContent value='info' className='space-y-5'>
      <Card className='h-fit'>
        <CardHeader>
          <CardTitle>Базовая информация</CardTitle>
        </CardHeader>

        <CardContent>
          <div className='flex flex-col space-y-0'>
            <Text>Название: {data.data.name}</Text>
            <Text>Оператор: {data.data.operator}</Text>
            <Text>Страна: {data.data.country}</Text>
            <Text>Тип проекта: {ProjectTypeTranslates[data.data.type]}</Text>
            <Text>
              Тип коллектора: {CollectorTypeTranslates[data.data.collectorType]}
            </Text>
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Список параметров</CardTitle>
        </CardHeader>
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Название</TableHead>
                <TableHead>Значение</TableHead>
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
