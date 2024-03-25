import {
  CartesianGrid,
  Line,
  LineChart,
  ReferenceArea,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts'

import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  TabsContent,
} from '@/components/ui'

import { useProjectCalculations } from './useProjectCalculations'

const CustomTooltip = ({ active, payload: payloads, label }: any) => {
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

export const ProjectCalculations = () => {
  const { state } = useProjectCalculations()

  return (
    <TabsContent value='calculations'>
      <ResponsiveContainer height={400}>
        <LineChart data={state.calculations}>
          <XAxis dataKey='method.name' tickMargin={10} />
          <YAxis
            domain={[-1.25, 1.25]}
            ticks={[-1, -0.75, -0.5, -0.25, 0, 0.25, 0.5, 0.75, 1]}
          />
          <Tooltip content={<CustomTooltip />} />
          <CartesianGrid strokeDasharray='5 5' />

          <ReferenceArea label='Не применим' y2={-0.75} fill='#FF0D0D' />
          <ReferenceArea label='Применим' y1={0.25} y2={0.75} fill='#ACB334' />
          <ReferenceArea
            label='Не благоприятен для применения'
            y1={-0.75}
            y2={-0.25}
            fill='#FF4E11'
          />
          <ReferenceArea
            label='Применим с низкой эффективностью'
            y1={-0.25}
            y2={0.25}
            fill='#FF8E15'
          />
          <ReferenceArea
            label='Благоприятен для применения'
            y1={0.75}
            fill='#69B34C'
          />

          <Line dataKey='ratio' activeDot={{ r: 10 }} />
        </LineChart>
      </ResponsiveContainer>
    </TabsContent>
  )
}
