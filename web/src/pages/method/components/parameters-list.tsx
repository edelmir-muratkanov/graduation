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
} from '@/components/ui'
import type { Method } from '@/types'

import { DeleteParameterButton } from './delete-parameter-button'

export const ParametersList = ({
  method,
  isAdmin = false,
}: {
  method: Method
  isAdmin: boolean
}) => {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Список параметров</CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Свойство</TableHead>
              <TableHead>Минимальное значение</TableHead>
              <TableHead>Максимальное значение</TableHead>
              <TableHead>Единицы измерения</TableHead>
            </TableRow>
          </TableHeader>

          <TableBody>
            {method.parameters.map(parameter => (
              <TableRow key={parameter.id}>
                <TableCell>{parameter.propertyName}</TableCell>
                <TableCell>
                  {parameter.first?.min
                    ? parameter.first.min
                    : parameter.second?.min}
                </TableCell>
                <TableCell>
                  {parameter.second?.max
                    ? parameter.second.max
                    : parameter.first?.max}
                </TableCell>
                <TableCell>{parameter.propertyUnit}</TableCell>
                {isAdmin && (
                  <TableCell>
                    <DeleteParameterButton
                      methodId={method.id}
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
  )
}
