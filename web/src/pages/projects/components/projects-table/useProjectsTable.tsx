import type { MouseEvent } from 'react'
import { useEffect, useMemo, useState } from 'react'
import { useNavigate } from '@tanstack/react-router'
import type { Row } from '@tanstack/react-table'
import { getCoreRowModel, useReactTable } from '@tanstack/react-table'

import { useGetProjectsQuery } from '@/lib/api'
import type { Project } from '@/types'

import { COLUMNS } from './columns'

type PaginationState = {
  pageIndex: number
  pageSize: number
}

export const useProjectsTable = () => {
  const columns = useMemo(() => COLUMNS, [])
  const navigate = useNavigate()

  const [pagination, setPagination] = useState<PaginationState>({
    pageIndex: 1,
    pageSize: 10,
  })

  const [globalFilter, setGlobalFilter] = useState('')

  const getProjectsQuery = useGetProjectsQuery({
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
    data: getProjectsQuery.data?.data.items ?? defaultData,
    columns,
    rowCount: getProjectsQuery.data?.data.totalCount,
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

  const handleRowClick = (
    event: MouseEvent,
    row: Row<Omit<Project, 'parameters' | 'members' | 'ownerId' | 'methods'>>,
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
