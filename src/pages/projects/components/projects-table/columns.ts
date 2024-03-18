import type { ColumnDef } from '@tanstack/react-table'

export const COLUMNS: ColumnDef<Project & ProjectStatistic>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'country',
    header: 'Country',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'operator',
    header: 'Operator',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'collectorType',
    header: 'Collector Type',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'type',
    header: 'Project type',
    footer: props => props.column.id,
  },
  {
    header: 'Methods',
    accessorFn: row => row._count.methods,
    cell: info => info.getValue(),
    footer: props => props.column.id,
  },

  {
    header: 'Properties',
    accessorFn: row => row._count.parameters,
    cell: info => info.getValue(),
    footer: props => props.column.id,
  },
  {
    header: 'Users',
    accessorFn: row => row._count.users,
    cell: info => info.getValue(),
    footer: props => props.column.id,
  },
]
