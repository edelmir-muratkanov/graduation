import type { ColumnDef } from '@tanstack/react-table'

import { CollectorTypeTranslates, ProjectTypeTranslates } from '@/lib/constants'
import type { CollectorType, Project, ProjectType } from '@/types'

export const COLUMNS: ColumnDef<
  Omit<Project, 'parameters' | 'members' | 'ownerId' | 'methods'>
>[] = [
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
]
