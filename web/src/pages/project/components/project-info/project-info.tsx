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

import { useProjectPage } from '../../useProjectPage'

import { DeleteMethodButton } from './delete-method-button'
import { DeleteParameterButton } from './delete-parameter-button'

export const ProjectInfo = ({
  isOwnerOrMember,
}: {
  isOwnerOrMember: boolean
}) => {
  const { state } = useProjectPage()
  return (
    <TabsContent value='info' className='space-y-5'>
      <Card className='h-fit'>
        <CardHeader>
          <CardTitle>Базовая информация</CardTitle>
        </CardHeader>

        <CardContent>
          <div className='flex flex-col space-y-0'>
            <Text>Название: {state.project.name}</Text>
            <Text>Оператор: {state.project.operator}</Text>
            <Text>Страна: {state.project.country}</Text>
            <Text>
              Тип проекта: {ProjectTypeTranslates[state.project.type]}
            </Text>
            <Text>
              Тип коллектора:{' '}
              {CollectorTypeTranslates[state.project.collectorType]}
            </Text>
          </div>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Список используемых методов</CardTitle>
        </CardHeader>
        <CardContent>
          <ul>
            {state.project.methods.map(method => (
              <li className='flex items-center w-full gap-x-4' key={method.id}>
                <Text>{method.name}</Text>
                {isOwnerOrMember && (
                  <DeleteMethodButton
                    projectId={state.project.id}
                    methodId={method.id}
                  />
                )}
              </li>
            ))}
          </ul>
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
                <TableHead>Единицы измерения</TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              {state.project.parameters.map(parameter => (
                <TableRow key={parameter.id}>
                  <TableCell>{parameter.name}</TableCell>
                  <TableCell>{parameter.value}</TableCell>
                  <TableCell>{parameter.unit}</TableCell>
                  {isOwnerOrMember && (
                    <TableCell>
                      <DeleteParameterButton
                        projectId={state.project.id}
                        parameterId={parameter.id}
                      />
                    </TableCell>
                  )}
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </TabsContent>
  )
}
