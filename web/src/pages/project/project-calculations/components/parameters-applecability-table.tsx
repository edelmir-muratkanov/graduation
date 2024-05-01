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
  const { calculations } = useProjectCalculations()

  const properties = calculations
    .flatMap(c => c.items.map(i => i.name))
    .filter((el, index, self) => index === self.indexOf(el))

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
            <TableHead key={`Head-${c.name}`} className='text-center'>
              {c.name}
            </TableHead>
          ))}
        </TableRow>
      </TableHeader>
      <TableBody>
        {properties.map(name => (
          <TableRow key={name}>
            <TableCell>{name}</TableCell>
            {calculations.map(c => {
              const item = c.items.find(i => i.name === name)

              return (
                <TableCell key={`${name}-${c.name}`} className='text-center'>
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
