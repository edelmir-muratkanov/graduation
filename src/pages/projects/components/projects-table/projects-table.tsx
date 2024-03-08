import { flexRender } from '@tanstack/react-table'

import {
  Button,
  Input,
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui'

import { useProjectsTable } from './useProjectsTable'

export const ProjectsTable = () => {
  const { state, functions } = useProjectsTable()

  return (
    <div className='flex flex-col gap-4 w-full '>
      <div className='flex justify-between'>
        <Input
          value={state.globalFilter}
          onChange={e => functions.setGlobalFilter(e.target.value)}
          placeholder='Search all columns...'
          className='p-2 font-lg shadow border border-block w-[200px]'
        />
        <Select
          onValueChange={e => state.table.setPageSize(Number(e))}
          value={state.table.getState().pagination.pageSize.toString()}
        >
          <SelectTrigger className='w-[160px]'>
            <SelectValue placeholder='Show'>
              Show {state.table.getState().pagination.pageSize}
            </SelectValue>
          </SelectTrigger>
          <SelectContent>
            {[10, 20, 30, 40, 50].map(pageSize => (
              <SelectItem key={pageSize} value={pageSize.toString()}>
                Show {pageSize}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      </div>
      <div className='rounded-md border'>
        <Table>
          <TableHeader>
            {state.table.getHeaderGroups().map(group => (
              <TableRow key={group.id}>
                {group.headers.map(header => (
                  <TableHead key={header.id} colSpan={header.colSpan}>
                    {header.isPlaceholder
                      ? null
                      : flexRender(
                          header.column.columnDef.header,
                          header.getContext(),
                        )}
                  </TableHead>
                ))}
              </TableRow>
            ))}
          </TableHeader>

          <TableBody>
            {state.table.getRowModel().rows?.length ? (
              state.table.getRowModel().rows.map(row => (
                <TableRow
                  id={row.id}
                  className='cursor-pointer'
                  data-state={row.getIsSelected() && 'selected'}
                  onClick={e => functions.handleRowClick(e, row)}
                >
                  {row.getVisibleCells().map(cell => (
                    <TableCell key={cell.id}>
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext(),
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell
                  colSpan={state.columns.length}
                  className='h-24 text-center'
                >
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
      <div className='flex items-center justify-center'>
        <div className='flex gap-1 m-auto'>
          <Button
            variant='outline'
            size='sm'
            onClick={() => state.table.firstPage()}
            disabled={!state.table.getCanPreviousPage()}
          >
            First
          </Button>

          <Button
            size='sm'
            variant='outline'
            onClick={() => state.table.previousPage()}
            disabled={!state.table.getCanPreviousPage()}
          >
            Previous
          </Button>

          <Button
            variant='outline'
            size='sm'
            onClick={() => state.table.nextPage()}
            disabled={!state.table.getCanNextPage()}
          >
            Next
          </Button>

          <Button
            variant='outline'
            size='sm'
            onClick={() => state.table.lastPage()}
            disabled={!state.table.getCanNextPage()}
          >
            Last
          </Button>
        </div>

        <span className='self-end border px-3 h-8 rounded-md'>
          {state.table.getState().pagination.pageIndex + 1}/
          {state.table.getPageCount()}
        </span>
      </div>
    </div>
  )
}
