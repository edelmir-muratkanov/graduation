import type { MouseEvent } from 'react'
import { useMemo, useState } from 'react'
import { useNavigate } from '@tanstack/react-router'
import type { Row } from '@tanstack/react-table'
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
  const navigate = useNavigate()

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
    debugTable: process.env.NODE_ENV !== 'production',
  })

  const handleRowClick = (
    event: MouseEvent,
    row: Row<Project & ProjectStatistic>,
  ) => {
    if (event.metaKey || event.ctrlKey) {
      const win = window.open(`/projects/${row.original.id}`, '_blank')
      win?.focus()
    } else {
      navigate({
        to: '/projects/$projectId',
        params: { projectId: row.original.id },
      })
    }
  }

  return {
    state: { table, globalFilter, columns },
    functions: { setGlobalFilter, handleRowClick },
  }
}
