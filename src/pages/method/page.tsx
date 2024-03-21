import { Link } from '@tanstack/react-router'

import {
  Badge,
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
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
import { CollectorTypeTranslates } from '@/lib/constants'

import { useMethodPage } from './useMethodPage'

export const MethodPage = () => {
  const { method, properties } = useMethodPage()

  return (
    <div className='w-full space-y-4'>
      <Breadcrumb className='p-2'>
        <BreadcrumbList>
          <BreadcrumbItem>
            <BreadcrumbLink asChild>
              <Link to='/'>Главная</Link>
            </BreadcrumbLink>
          </BreadcrumbItem>
          <BreadcrumbSeparator />
          <BreadcrumbItem>
            <BreadcrumbLink asChild>
              <Link to='/methods'>Методы</Link>
            </BreadcrumbLink>
          </BreadcrumbItem>
          <BreadcrumbSeparator />
          <BreadcrumbItem>
            <BreadcrumbPage>{method.data.name}</BreadcrumbPage>
          </BreadcrumbItem>
        </BreadcrumbList>
      </Breadcrumb>

      <div className='w-full space-y-5'>
        <Card className='h-fit'>
          <CardHeader>
            <CardTitle>Базовая информация</CardTitle>
          </CardHeader>
          <CardContent className='space-y-0'>
            <div className='space-x-2'>
              <Text>Название:</Text>
              <Text>{method.data.name}</Text>
            </div>
            <div className='space-x-2'>
              <Text>Типы коллекторов: </Text>
              {method.data.collectorTypes.map(type => (
                <Badge key={type}>{CollectorTypeTranslates[type]}</Badge>
              ))}
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
                  <TableHead>Свойство</TableHead>
                  <TableHead>Минимальное значение</TableHead>
                  <TableHead>Максимальное значение</TableHead>
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
    </div>
  )
}
