import {
  Badge,
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  Text,
} from '@/components/ui'
import { CollectorTypeTranslates } from '@/lib/constants'
import type { Method } from '@/types'

export const BaseInfo = ({ method }: { method: Method }) => {
  return (
    <Card className='h-fit'>
      <CardHeader>
        <CardTitle>Базовая информация</CardTitle>
      </CardHeader>
      <CardContent className='space-y-0'>
        <div className='space-x-2'>
          <Text>Название:</Text>
          <Text>{method.name}</Text>
        </div>
        <div className='space-x-2'>
          <Text>Типы коллекторов: </Text>
          {method.collectorTypes.map(type => (
            <Badge key={type}>{CollectorTypeTranslates[type]}</Badge>
          ))}
        </div>
      </CardContent>
    </Card>
  )
}
