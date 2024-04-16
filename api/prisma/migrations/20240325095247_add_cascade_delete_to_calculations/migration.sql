-- DropForeignKey
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_methodId_projectId_fkey";

-- DropForeignKey
ALTER TABLE "calculation_items" DROP CONSTRAINT "calculation_items_propertyId_fkey";

-- DropForeignKey
ALTER TABLE "calculations" DROP CONSTRAINT "calculations_methodId_fkey";

-- AddForeignKey
ALTER TABLE "calculations" ADD CONSTRAINT "calculations_methodId_fkey" FOREIGN KEY ("methodId") REFERENCES "methods"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_methodId_projectId_fkey" FOREIGN KEY ("methodId", "projectId") REFERENCES "calculations"("methodId", "projectId") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_propertyId_fkey" FOREIGN KEY ("propertyId") REFERENCES "properties"("id") ON DELETE CASCADE ON UPDATE CASCADE;
