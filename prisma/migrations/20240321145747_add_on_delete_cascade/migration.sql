-- DropForeignKey
ALTER TABLE "methods_properties" DROP CONSTRAINT "methods_properties_methodId_fkey";

-- DropForeignKey
ALTER TABLE "methods_properties" DROP CONSTRAINT "methods_properties_propertyId_fkey";

-- DropForeignKey
ALTER TABLE "projects_methods" DROP CONSTRAINT "projects_methods_methodId_fkey";

-- DropForeignKey
ALTER TABLE "projects_methods" DROP CONSTRAINT "projects_methods_projectId_fkey";

-- DropForeignKey
ALTER TABLE "projects_properties" DROP CONSTRAINT "projects_properties_projectId_fkey";

-- DropForeignKey
ALTER TABLE "projects_properties" DROP CONSTRAINT "projects_properties_propertyId_fkey";

-- DropForeignKey
ALTER TABLE "projects_users" DROP CONSTRAINT "projects_users_projectId_fkey";

-- DropForeignKey
ALTER TABLE "projects_users" DROP CONSTRAINT "projects_users_userId_fkey";

-- AddForeignKey
ALTER TABLE "methods_properties" ADD CONSTRAINT "methods_properties_methodId_fkey" FOREIGN KEY ("methodId") REFERENCES "methods"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "methods_properties" ADD CONSTRAINT "methods_properties_propertyId_fkey" FOREIGN KEY ("propertyId") REFERENCES "properties"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_properties" ADD CONSTRAINT "projects_properties_projectId_fkey" FOREIGN KEY ("projectId") REFERENCES "projects"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_properties" ADD CONSTRAINT "projects_properties_propertyId_fkey" FOREIGN KEY ("propertyId") REFERENCES "properties"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_methods" ADD CONSTRAINT "projects_methods_projectId_fkey" FOREIGN KEY ("projectId") REFERENCES "projects"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_methods" ADD CONSTRAINT "projects_methods_methodId_fkey" FOREIGN KEY ("methodId") REFERENCES "methods"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_users" ADD CONSTRAINT "projects_users_projectId_fkey" FOREIGN KEY ("projectId") REFERENCES "projects"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_users" ADD CONSTRAINT "projects_users_userId_fkey" FOREIGN KEY ("userId") REFERENCES "users"("id") ON DELETE CASCADE ON UPDATE CASCADE;
