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
  const { method } = useMethodPage()

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
                  <TableHead>Единицы измерения</TableHead>
                </TableRow>
              </TableHeader>

              <TableBody>
                {method.data.parameters.map(parameter => (
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
