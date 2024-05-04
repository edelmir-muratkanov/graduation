import type { MouseEvent } from 'react'
import { useMemo, useState } from 'react'
import { useNavigate } from '@tanstack/react-router'
import type { PaginationState, Row } from '@tanstack/react-table'
import { getCoreRowModel, useReactTable } from '@tanstack/react-table'

import { useGetMethodsQuery } from '@/lib/api'
import { useDebounce } from '@/lib/useDebounce'
import type { Method } from '@/types'

import { COLUMNS } from './columns'

export const useMethodsTable = () => {
  const columns = useMemo(() => COLUMNS, [])
  const navigate = useNavigate()
  const [pagination, setPagination] = useState<PaginationState>({
    pageIndex: 0,
    pageSize: 10,
  })

  const [globalFilter, setGlobalFilter] = useState('')

  const debouncedGlobalFilter = useDebounce(globalFilter, 1_000)

  const getMethodsQuery = useGetMethodsQuery({
    config: {
      params: {
        pageSize: pagination.pageSize,
        pageNumber: pagination.pageIndex + 1,
        searchTerm: debouncedGlobalFilter,
        sortColumn: 'name',
      },
    },
  })

  const defaultData = useMemo(() => {}, [])

  const table = useReactTable({
    data: getMethodsQuery.data.data.items ?? defaultData,
    columns,
    pageCount: getMethodsQuery.data.data.totalPages,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    onPaginationChange: setPagination,
    manualFiltering: true,
    onGlobalFilterChange: setGlobalFilter,
    autoResetPageIndex: true,
    state: { pagination, globalFilter },
    debugTable: !import.meta.env.PROD,
  })

  const handleRowClick = (
    event: MouseEvent,
    row: Row<Omit<Method, 'parameters'>>,
  ) => {
    if (event.metaKey || event.ctrlKey) {
      const win = window.open(`/parameters/${row.original.id}`, '_blank')
      win?.focus()
    } else {
      navigate({
        to: '/methods/$methodId',
        params: { methodId: row.original.id },
      })
    }
  }

  return {
    state: { table, columns, globalFilter },
    functions: { handleRowClick, setGlobalFilter, navigate },
  }
}
