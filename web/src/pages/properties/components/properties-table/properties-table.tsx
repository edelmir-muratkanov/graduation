import { flexRender } from '@tanstack/react-table'
import { X } from 'lucide-react'

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

import { usePropertiesTable } from './usePropertiesTable'

export const PropertiesTable = () => {
  const { state, functions } = usePropertiesTable()

  return (
    <div className='flex flex-col gap-4 w-full '>
      <div className='flex justify-between'>
        <Input
          value={state.globalFilter}
          onChange={e => functions.setGlobalFilter(e.target.value)}
          placeholder='Поиск...'
          className='p-2 font-lg shadow border border-block w-[400px]'
          endIcon={
            <X
              className='size-5 cursor-pointer'
              onClick={() => functions.setGlobalFilter('')}
            />
          }
        />
        <Select
          onValueChange={e => state.table.setPageSize(Number(e))}
          value={state.table.getState().pagination.pageSize.toString()}
        >
          <SelectTrigger className='w-[160px]'>
            <SelectValue>
              Показать {state.table.getState().pagination.pageSize}
            </SelectValue>
          </SelectTrigger>
          <SelectContent>
            {[10, 20, 30, 40, 50].map(pageSize => (
              <SelectItem key={pageSize} value={pageSize.toString()}>
                Показать {pageSize}
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
                  key={row.id}
                  className='cursor-pointer'
                  data-state={row.getIsSelected() && 'selected'}
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
                  Нет результатов
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
      <div className='flex items-center justify-center'>
        <div className='flex gap-1 m-auto'>
          <Button
            size='sm'
            variant='outline'
            onClick={() => state.table.previousPage()}
            disabled={!state.table.getCanPreviousPage()}
          >
            Предыдущая страница
          </Button>

          <Button
            variant='outline'
            size='sm'
            onClick={() => state.table.nextPage()}
            disabled={!state.table.getCanNextPage()}
          >
            Следующая страница
          </Button>
        </div>
      </div>
    </div>
  )
}
