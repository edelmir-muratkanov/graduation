import { createColumnHelper } from '@tanstack/react-table'

import { Badge } from '@/components/ui'

const columnHelper =
  createColumnHelper<
    Pick<Method & MethodStatistic, 'id' | 'name' | '_count' | 'collectorTypes'>
  >()

export const COLUMNS = [
  columnHelper.accessor('name', {
    header: 'Name',
    cell: info => info.getValue(),
    footer: props => props.column.id,
  }),

  columnHelper.accessor('collectorTypes', {
    header: 'Collector types',
    cell: info => (
      <div className='space-x-2'>
        {info.renderValue()?.map(t => (
          <Badge className='gap-2 ' key={t}>
            {t}
          </Badge>
        ))}
      </div>
    ),
    footer: props => props.column.id,
  }),

  columnHelper.accessor('_count.projects', {
    header: 'Projects number',
    cell: info => info.getValue(),
    footer: props => props.column.id,
  }),
]
