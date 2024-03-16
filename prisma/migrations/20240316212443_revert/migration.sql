/*
  Warnings:

  - You are about to drop the column `collectorType` on the `methods` table. All the data in the column will be lost.
  - You are about to drop the column `collectorTypes` on the `projects` table. All the data in the column will be lost.

*/
-- AlterTable
ALTER TABLE "methods" DROP COLUMN "collectorType",
ADD COLUMN     "collectorTypes" "CollectorType"[] DEFAULT ARRAY[]::"CollectorType"[];

-- AlterTable
ALTER TABLE "projects" DROP COLUMN "collectorTypes",
ADD COLUMN     "collectorType" "CollectorType";
