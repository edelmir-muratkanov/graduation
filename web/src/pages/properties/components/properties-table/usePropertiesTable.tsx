import { useEffect, useMemo, useState } from 'react'
import { getCoreRowModel, useReactTable } from '@tanstack/react-table'

import { useGetPropertiesQuery } from '@/lib/api'
import { useDebounce } from '@/lib/useDebounce'

import { useColumns } from './columns'

type PaginationState = {
  pageIndex: number
  pageSize: number
}

export const usePropertiesTable = () => {
  const { COLUMNS } = useColumns()
  const columns = useMemo(() => COLUMNS, [COLUMNS])

  const [pagination, setPagination] = useState<PaginationState>({
    pageIndex: 1,
    pageSize: 10,
  })

  const [globalFilter, setGlobalFilter] = useState('')
  const debouncedGlobalFilter = useDebounce(globalFilter, 1_000)

  const getPropertiesQuery = useGetPropertiesQuery({
    config: {
      params: {
        pageSize: pagination.pageSize,
        pageNumber: pagination.pageIndex + 1,
        searchTerm: debouncedGlobalFilter,
      },
    },
  })

  const defaultData = useMemo(() => [], [])

  const table = useReactTable({
    data: getPropertiesQuery.data?.data.items ?? defaultData,
    columns,
    rowCount: getPropertiesQuery.data?.data.totalCount,
    state: { pagination, globalFilter },
    onPaginationChange: setPagination,
    getCoreRowModel: getCoreRowModel(),
    manualFiltering: true,
    manualPagination: true,
    debugTable: process.env.NODE_ENV !== 'production',
  })

  useEffect(() => {
    if (setPagination) {
      setPagination(pagination => ({
        pageIndex: 0,
        pageSize: pagination.pageSize,
      }))
    }
  }, [globalFilter, setPagination])

  return {
    state: { table, globalFilter, columns: COLUMNS },
    functions: { setGlobalFilter },
  }
}
