import type { ColumnDef } from '@tanstack/react-table'

import type { Property } from '@/types'

import { UpdateProperty } from '../update-property/update-property'

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
  {
    id: 'actions',
    cell: ({ row }) => {
      const property = row.original

      return <UpdateProperty propertyId={property.id} />
    },
  },
]
