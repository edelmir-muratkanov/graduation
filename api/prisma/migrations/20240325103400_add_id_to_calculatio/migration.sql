/*
  Warnings:

  - The primary key for the `calculation_items` table will be changed. If it partially fails, the table could be left without primary key constraint.
  - You are about to drop the column `methodId` on the `calculation_items` table. All the data in the column will be lost.
  - You are about to drop the column `projectId` on the `calculation_items` table. All the data in the column will be lost.
  - The primary key for the `calculations` table will be changed. If it partially fails, the table could be left without primary key constraint.
  - A unique constraint covering the columns `[calculationId,propertyId,collectorType]` on the table `calculation_items` will be added. If there are existing duplicate values, this will fail.
  - A unique constraint covering the columns `[methodId,projectId]` on the table `calculations` will be added. If there are existing duplicate values, this will fail.
  - The required column `id` was added to the `calculation_items` table with a prisma-level default value. This is not possible if the table is not empty. Please add this column as optional, then populate it before making it required.
  - The required column `id` was added to the `calculations` table with a prisma-level default value. This is not possible if the table is not empty. Please add this column as optional, then populate it before making it required.

*/
-- DropForeignKey
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_methodId_projectId_fkey";

-- AlterTable
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_pkey",
DROP COLUMN "methodId",
DROP COLUMN "projectId",
ADD COLUMN     "calculationId" TEXT,
ADD COLUMN     "id" TEXT NOT NULL,
ADD CONSTRAINT "calculation_items_pkey" PRIMARY KEY ("id");

-- AlterTable
ALTER TABLE "calculations" DROP CONSTRAINT "calculations_pkey",
ADD COLUMN     "id" TEXT NOT NULL,
ADD CONSTRAINT "calculations_pkey" PRIMARY KEY ("id");

-- CreateIndex
CREATE UNIQUE INDEX "calculation_items_calculationId_propertyId_collectorType_key" ON "calculation_items"("calculationId", "propertyId", "collectorType");

-- CreateIndex
CREATE UNIQUE INDEX "calculations_methodId_projectId_key" ON "calculations"("methodId", "projectId");

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_calculationId_fkey" FOREIGN KEY ("calculationId") REFERENCES "calculations"("id") ON DELETE SET NULL ON UPDATE CASCADE;
