/*
  Warnings:

  - The primary key for the `calculation_items` table will be changed. If it partially fails, the table could be left without primary key constraint.

*/
-- DropForeignKey
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_propertiesId_fkey";

-- AlterTable
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_pkey",
ADD COLUMN     "collectorType" "CollectorType",
ALTER COLUMN "propertiesId" DROP NOT NULL,
ADD CONSTRAINT "calculation_items_pkey" PRIMARY KEY ("calculationMethodId", "calculationProjectId");

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_propertiesId_fkey" FOREIGN KEY ("propertiesId") REFERENCES "properties"("id") ON DELETE SET NULL ON UPDATE CASCADE;
