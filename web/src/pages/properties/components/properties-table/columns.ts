import type { ColumnDef } from '@tanstack/react-table'

import type { Property } from '@/types'

export const COLUMNS: ColumnDef<Property>[] = [
  {
    accessorKey: 'name',
    header: 'Название',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'unit',
    header: 'Единицы измерения',
    footer: props => props.column.id,
  },
]
