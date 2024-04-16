import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui'

import { useProjectCalculations } from '../useProjectCalculations'

export const ParametersApplecabilityTable = () => {
  const { calculations, properties } = useProjectCalculations()

  return (
    <Table className='my-5 table-fixed '>
      <colgroup>
        <col />
        <col />
      </colgroup>
      <TableHeader>
        <TableRow>
          <TableHead rowSpan={2}>Свойство</TableHead>
          <TableHead colSpan={calculations.length} className='text-center'>
            Коэфицент применимости
          </TableHead>
        </TableRow>
        <TableRow>
          {calculations.map(c => (
            <TableHead key={`Head-${c.method.name}`} className='text-center'>
              {c.method.name}
            </TableHead>
          ))}
        </TableRow>
      </TableHeader>
      <TableBody>
        <TableRow>
          <TableCell>Тип коллектора</TableCell>
          {calculations.map(c => {
            const item = c.items.find(i => i.collectorType)
            return (
              <TableCell
                key={`${c.method.name}-${item?.property?.name || 'Тип коллектора'}`}
                className='text-center'
              >
                {item?.ratio}
              </TableCell>
            )
          })}
        </TableRow>
        {properties.map(({ property }) => (
          <TableRow key={property.name}>
            <TableCell>{property.name}</TableCell>
            {calculations.map(c => {
              const item = c.items.find(
                i => i.property && i.property.name === property.name,
              )

              return (
                <TableCell
                  key={`${property.name}-${c.method.name}`}
                  className='text-center'
                >
                  {item ? item.ratio.toLocaleString('ru-KZ') : '-'}
                </TableCell>
              )
            })}
          </TableRow>
        ))}
      </TableBody>
    </Table>
  )
}
