import { useEffect, useMemo, useState } from 'react'
import { getCoreRowModel, useReactTable } from '@tanstack/react-table'

import { useGetPropertiesQuery } from '@/lib/api'

import { COLUMNS } from './columns'

type PaginationState = {
  pageIndex: number
  pageSize: number
}

export const usePropertiesTable = () => {
  const columns = useMemo(() => COLUMNS, [])

  const [pagination, setPagination] = useState<PaginationState>({
    pageIndex: 1,
    pageSize: 10,
  })

  const [globalFilter, setGlobalFilter] = useState('')

  const getPropertiesQuery = useGetPropertiesQuery({
    config: {
      params: {
        pageSize: pagination.pageSize,
        pageNumber: pagination.pageIndex + 1,
        searchTerm: globalFilter,
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
    state: { table, globalFilter, columns },
    functions: { setGlobalFilter },
  }
}
