import type { ColumnDef } from '@tanstack/react-table'

import { useProfile } from '@/lib/contexts'
import type { Property } from '@/types'

import { DeleteProperty } from '../delete-property'
import { UpdateProperty } from '../update-property/update-property'

export const useColumns = () => {
  const { user } = useProfile()
  const isAdmin = user?.role === 'Admin'

  const COLUMNS: ColumnDef<Property>[] = [
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

        if (isAdmin) {
          return (
            <div className='flex gap-x-4 p-1'>
              <UpdateProperty propertyId={property.id} />
              <DeleteProperty propertyId={property.id} />
            </div>
          )
        }
      },
    },
  ]

  return { COLUMNS }
}
