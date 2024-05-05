import { useState } from 'react'
import { Cell, Pie, PieChart, ResponsiveContainer, Sector } from 'recharts'

import { Text } from '@/components/ui/typography'

import { useProjectCalculations } from '../useProjectCalculations'

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042']

const renderActiveShape = (props: any) => {
  const RADIAN = Math.PI / 180
  const {
    cx,
    cy,
    midAngle,
    innerRadius,
    outerRadius,
    startAngle,
    endAngle,
    payload,
    percent,
    fill,
  } = props
  const sin = Math.sin(-RADIAN * midAngle)
  const cos = Math.cos(-RADIAN * midAngle)
  const sx = cx + (outerRadius + 5) * cos
  const sy = cy + (outerRadius + 5) * sin
  const mx = cx + (outerRadius + 15) * cos
  const my = cy + (outerRadius + 15) * sin
  const ex = mx + (cos >= 0 ? 1 : -1) * 22
  const ey = my
  const textAnchor = cos >= 0 ? 'start' : 'end'

  return (
    <g>
      <text x={cx} y={cy} dy={8} textAnchor='middle' fill={fill}>
        {payload.name}
      </text>
      <Sector
        cx={cx}
        cy={cy}
        innerRadius={innerRadius}
        outerRadius={outerRadius}
        startAngle={startAngle}
        endAngle={endAngle}
        fill={fill}
      />
      <Sector
        cx={cx}
        cy={cy}
        startAngle={startAngle}
        endAngle={endAngle}
        innerRadius={outerRadius + 6}
        outerRadius={outerRadius + 10}
        fill={fill}
      />
      <path
        d={`M${sx},${sy}L${mx},${my}L${ex},${ey}`}
        stroke={fill}
        fill='none'
      />
      <circle cx={ex} cy={ey} r={2} fill={fill} stroke='none' />

      <text
        x={ex + (cos >= 0 ? 1 : -1) * 6}
        y={ey}
        textAnchor={textAnchor}
        fill='#999'
        className='text-sm'
      >
        {`${(percent * 100).toFixed(2)}%`}
      </text>
    </g>
  )
}

export const AcceptableParametersChart = () => {
  const [activeIndex, setActiveIndex] = useState(0)
  const { acceptableParameters } = useProjectCalculations()

  return (
    <>
      <Text>Процент подходящих параметров</Text>
      <ResponsiveContainer>
        <PieChart title='' margin={{ left: 8 }}>
          <Pie
            activeIndex={activeIndex}
            activeShape={renderActiveShape}
            data={acceptableParameters}
            cx='50%'
            cy='50%'
            innerRadius={80}
            outerRadius={90}
            fill='#8884d8'
            dataKey='acceptableParameters'
            onMouseEnter={(_, index) => setActiveIndex(index)}
          >
            {acceptableParameters.map((p, index) => (
              <Cell
                key={p.methodName}
                name={p.methodName}
                fill={COLORS[index % COLORS.length]}
              />
            ))}
          </Pie>
        </PieChart>
      </ResponsiveContainer>
    </>
  )
}
