import {
  CartesianGrid,
  Line,
  LineChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts'

import { Card, CardContent, CardHeader, CardTitle, Text } from '@/components/ui'

import { useProjectCalculations } from '../useProjectCalculations'

const LineTooltip = ({ active, payload: payloads, label }: any) => {
  if (active && payloads && payloads.length) {
    const { payload } = payloads[0]
    const { applicability, ratio } = payload
    return (
      <Card>
        <CardHeader className='p-3'>
          <CardTitle>{label}</CardTitle>
        </CardHeader>
        <CardContent className='p-3'>
          <p>
            Коэфицент применимости (от -1 до 1): {ratio.toLocaleString('ru-KZ')}
          </p>
          <p>Применимость: {applicability}</p>
        </CardContent>
      </Card>
    )
  }

  return null
}
export const MethodsApplecabilityChart = () => {
  const { calculations } = useProjectCalculations()
  return (
    <>
      <Text>Применимость методов</Text>
      <ResponsiveContainer className='flex-grow'>
        <LineChart data={calculations}>
          <XAxis
            type='category'
            dataKey='name'
            tick={{ fontSize: 12 }}
            tickMargin={10}
            padding={{ left: 30, right: 30 }}
          />
          <YAxis
            domain={[-1.25, 1.25]}
            ticks={[-1, -0.75, -0.5, -0.25, 0, 0.25, 0.5, 0.75, 1]}
          />
          <Tooltip content={<LineTooltip />} />
          <CartesianGrid strokeDasharray='5 5' />

          <Line type='monotone' dataKey='ratio' activeDot={{ r: 10 }} />
        </LineChart>
      </ResponsiveContainer>
    </>
  )
}
