/*
  Warnings:

  - Made the column `calculationId` on table `calculation_items` required. This step will fail if there are existing NULL values in that column.

*/
-- DropForeignKey
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_calculationId_fkey";

-- AlterTable
ALTER TABLE "calculation_items" ALTER COLUMN "calculationId" SET NOT NULL;

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_calculationId_fkey" FOREIGN KEY ("calculationId") REFERENCES "calculations"("id") ON DELETE CASCADE ON UPDATE CASCADE;
