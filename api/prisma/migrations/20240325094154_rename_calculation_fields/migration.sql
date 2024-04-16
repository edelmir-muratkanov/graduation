/*
  Warnings:

  - The primary key for the `calculation_items` table will be changed. If it partially fails, the table could be left without primary key constraint.
  - You are about to drop the column `calculationMethodId` on the `calculation_items` table. All the data in the column will be lost.
  - You are about to drop the column `calculationProjectId` on the `calculation_items` table. All the data in the column will be lost.
  - You are about to drop the column `propertiesId` on the `calculation_items` table. All the data in the column will be lost.
  - Added the required column `methodId` to the `calculation_items` table without a default value. This is not possible if the table is not empty.
  - Added the required column `projectId` to the `calculation_items` table without a default value. This is not possible if the table is not empty.

*/
-- DropForeignKey
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_calculationMethodId_calculationProjectId_fkey";

-- DropForeignKey
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_propertiesId_fkey";

-- AlterTable
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_pkey",
DROP COLUMN "calculationMethodId",
DROP COLUMN "calculationProjectId",
DROP COLUMN "propertiesId",
ADD COLUMN     "methodId" TEXT NOT NULL,
ADD COLUMN     "projectId" TEXT NOT NULL,
ADD COLUMN     "propertyId" TEXT,
ADD CONSTRAINT "calculation_items_pkey" PRIMARY KEY ("methodId", "projectId");

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_methodId_projectId_fkey" FOREIGN KEY ("methodId", "projectId") REFERENCES "calculations"("methodId", "projectId") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_propertyId_fkey" FOREIGN KEY ("propertyId") REFERENCES "properties"("id") ON DELETE SET NULL ON UPDATE CASCADE;
