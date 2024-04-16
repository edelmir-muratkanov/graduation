/*
  Warnings:

  - You are about to drop the column `collectorType` on the `projects` table. All the data in the column will be lost.

*/
-- AlterTable
ALTER TABLE "projects" DROP COLUMN "collectorType",
ADD COLUMN     "collectorTypes" "CollectorType";
