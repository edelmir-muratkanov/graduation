import { useMemo, useState } from 'react'
import {
  getCoreRowModel,
  getFilteredRowModel,
  useReactTable,
} from '@tanstack/react-table'

import { useGetProjectsQuery } from '@/lib/api'

import { COLUMNS } from './columns'

type PaginationState = {
  pageIndex: number
  pageSize: number
}

export const useProjectsTable = () => {
  const columns = useMemo(() => COLUMNS, [])

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
