import { Badge } from '@/components/ui/badge'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Text } from '@/components/ui/typography'
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
