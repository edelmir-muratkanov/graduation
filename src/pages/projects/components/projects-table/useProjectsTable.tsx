import { useMemo, useState } from 'react'
import type { ColumnDef } from '@tanstack/react-table'
import {
  getCoreRowModel,
  getFilteredRowModel,
  useReactTable,
} from '@tanstack/react-table'

import { useGetProjectsQuery } from '@/lib/api'

type PaginationState = {
  pageIndex: number
  pageSize: number
}

export const useProjectsTable = () => {
  const columns = useMemo<ColumnDef<Project & ProjectStatistic>[]>(
    () => [
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
    ],
    [],
  )

  const [pagination, setPagination] = useState<PaginationState>({
    pageIndex: 0,
    pageSize: 10,
  })

  const [globalFilter, setGlobalFilter] = useState('')

  const getProjectsQuery = useGetProjectsQuery({
    config: {
      params: {
        limit: pagination.pageSize,
        offset: pagination.pageIndex * pagination.pageSize,
      },
    },
  })

  const defaultData = useMemo(() => [], [])

  const table = useReactTable({
    data: getProjectsQuery.data?.data.items ?? defaultData,
    columns,
    rowCount: getProjectsQuery.data?.data.count,
    state: { pagination, globalFilter },
    onPaginationChange: setPagination,
    getCoreRowModel: getCoreRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    manualPagination: true,
    debugTable: true,
  })

  return { table, globalFilter, setGlobalFilter, columns }
}
