import { TabsContent } from '@/components/ui'

import { AcceptableParametersChart } from './components/acceptable-parameters-chart'
import { MethodsApplecabilityChart } from './components/methods-applecability-chart'
import { ParametersApplecabilityTable } from './components/parameters-applecability-table'

export const ProjectCalculations = () => {
  return (
    <TabsContent value='calculations' className='space-y-10'>
      <div className='w-full flex h-96'>
        <div className='w-[500px] text-center'>
          <AcceptableParametersChart />
        </div>
        <div className='w-full text-center'>
          <MethodsApplecabilityChart />
        </div>
      </div>

      <ParametersApplecabilityTable />
    </TabsContent>
  )
}
