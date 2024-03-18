import type { MouseEvent } from 'react'
import { useMemo, useState } from 'react'
import { useNavigate } from '@tanstack/react-router'
import type { PaginationState, Row } from '@tanstack/react-table'
import { getCoreRowModel, useReactTable } from '@tanstack/react-table'

import { useGetMethodsQuery } from '@/lib/api'
import { useDebounce } from '@/lib/useDebounce'

import { COLUMNS } from './columns'

export const useMethodsTable = () => {
  const columns = useMemo(() => COLUMNS, [])
  const navigate = useNavigate()
  const [pagination, setPagination] = useState<PaginationState>({
    pageIndex: 0,
    pageSize: 10,
  })

  const [globalFilter, setGlobalFilter] = useState<string | undefined>(
    undefined,
  )

  const debouncedGlobalFilter = useDebounce(globalFilter, 1_000)

  const getMethodsQuery = useGetMethodsQuery({
    config: {
      params: {
        limit: pagination.pageSize,
        offset: pagination.pageIndex * pagination.pageSize,
        search: debouncedGlobalFilter,
      },
    },
  })

  const defaultData = useMemo(() => {}, [])

  const table = useReactTable({
    data: getMethodsQuery.data.data.items ?? defaultData,
    columns,
    rowCount: getMethodsQuery.data.data.count,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    onPaginationChange: setPagination,
    manualFiltering: true,
    onGlobalFilterChange: setGlobalFilter,
    state: { pagination, globalFilter },
    debugTable: !import.meta.env.PROD,
  })

  const handleRowClick = (
    event: MouseEvent,
    row: Row<Omit<Method & MethodStatistic, 'parameters'>>,
  ) => {
    if (event.metaKey || event.ctrlKey) {
      const win = window.open(`/parameters/${row.original.id}`, '_blank')
      win?.focus()
    } else {
      // navigate()
    }
  }

  return {
    state: { table, columns, globalFilter },
    functions: { handleRowClick, setGlobalFilter, navigate },
  }
}
