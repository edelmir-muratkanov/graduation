/*
  Warnings:

  - Added the required column `type` to the `projects` table without a default value. This is not possible if the table is not empty.

*/
-- CreateEnum
CREATE TYPE "CollectorType" AS ENUM ('Terrigen', 'Carbonate');

-- CreateEnum
CREATE TYPE "Type" AS ENUM ('Ground', 'Shelf');

-- AlterTable
ALTER TABLE "methods" ADD COLUMN     "collectorType" "CollectorType"[] DEFAULT ARRAY[]::"CollectorType"[];

-- AlterTable
ALTER TABLE "projects" ADD COLUMN     "collectorType" "CollectorType",
ADD COLUMN     "type" "Type" NOT NULL;
