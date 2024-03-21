import { createColumnHelper } from '@tanstack/react-table'

import { Badge } from '@/components/ui'
import { CollectorTypeTranslates } from '@/lib/constants'

const columnHelper =
  createColumnHelper<
    Pick<Method & MethodStatistic, 'id' | 'name' | '_count' | 'collectorTypes'>
  >()

export const COLUMNS = [
  columnHelper.accessor('name', {
    header: 'Название',
    cell: info => info.getValue(),
    footer: props => props.column.id,
  }),

  columnHelper.accessor('collectorTypes', {
    header: 'Тип коллектора',
    cell: info => (
      <div className='space-x-2'>
        {info.renderValue()?.map(t => (
          <Badge className='gap-2 bg-primary/90' key={t}>
            {CollectorTypeTranslates[t]}
          </Badge>
        ))}
      </div>
    ),
    footer: props => props.column.id,
  }),

  columnHelper.accessor('_count.projects', {
    header: '№ Проектов',
    cell: info => info.getValue(),
    footer: props => props.column.id,
  }),
]
