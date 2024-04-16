import type { ColumnDef } from '@tanstack/react-table'

import { CollectorTypeTranslates, ProjectTypeTranslates } from '@/lib/constants'

export const COLUMNS: ColumnDef<Project & ProjectStatistic>[] = [
  {
    accessorKey: 'name',
    header: 'Название',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'country',
    header: 'Страна',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'operator',
    header: 'Оператор',
    footer: props => props.column.id,
  },
  {
    accessorKey: 'type',
    header: 'Тип проекта',
    cell: info => ProjectTypeTranslates[info.getValue() as ProjectType],
    footer: props => props.column.id,
  },
  {
    accessorKey: 'collectorType',
    header: 'Тип коллектора',
    cell: info => CollectorTypeTranslates[info.getValue() as CollectorType],
    footer: props => props.column.id,
  },
  {
    header: '№ методов',
    accessorFn: row => row._count.methods,
    cell: info => info.getValue(),
    footer: props => props.column.id,
  },

  {
    header: '№ параметров',
    accessorFn: row => row._count.parameters,
    cell: info => info.getValue(),
    footer: props => props.column.id,
  },
  {
    header: '№ пользователей',
    accessorFn: row => row._count.users,
    cell: info => info.getValue(),
    footer: props => props.column.id,
  },
]
