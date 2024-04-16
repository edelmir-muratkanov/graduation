/*
  Warnings:

  - You are about to drop the `Method` table. If the table is not empty, all the data it contains will be lost.
  - You are about to drop the `MethodOnParameter` table. If the table is not empty, all the data it contains will be lost.
  - You are about to drop the `Project` table. If the table is not empty, all the data it contains will be lost.
  - You are about to drop the `ProjectOnMethod` table. If the table is not empty, all the data it contains will be lost.
  - You are about to drop the `ProjectOnParameter` table. If the table is not empty, all the data it contains will be lost.
  - You are about to drop the `ProjectOnUser` table. If the table is not empty, all the data it contains will be lost.
  - You are about to drop the `Property` table. If the table is not empty, all the data it contains will be lost.
  - You are about to drop the `User` table. If the table is not empty, all the data it contains will be lost.

*/
-- DropForeignKey
ALTER TABLE "MethodOnParameter" DROP CONSTRAINT "MethodOnParameter_methodId_fkey";

-- DropForeignKey
ALTER TABLE "MethodOnParameter" DROP CONSTRAINT "MethodOnParameter_propertyId_fkey";

-- DropForeignKey
ALTER TABLE "ProjectOnMethod" DROP CONSTRAINT "ProjectOnMethod_methodId_fkey";

-- DropForeignKey
ALTER TABLE "ProjectOnMethod" DROP CONSTRAINT "ProjectOnMethod_projectId_fkey";

-- DropForeignKey
ALTER TABLE "ProjectOnParameter" DROP CONSTRAINT "ProjectOnParameter_projectId_fkey";

-- DropForeignKey
ALTER TABLE "ProjectOnParameter" DROP CONSTRAINT "ProjectOnParameter_propertyId_fkey";

-- DropForeignKey
ALTER TABLE "ProjectOnUser" DROP CONSTRAINT "ProjectOnUser_projectId_fkey";

-- DropForeignKey
ALTER TABLE "ProjectOnUser" DROP CONSTRAINT "ProjectOnUser_userId_fkey";

-- DropTable
DROP TABLE "Method";

-- DropTable
DROP TABLE "MethodOnParameter";

-- DropTable
DROP TABLE "Project";

-- DropTable
DROP TABLE "ProjectOnMethod";

-- DropTable
DROP TABLE "ProjectOnParameter";

-- DropTable
DROP TABLE "ProjectOnUser";

-- DropTable
DROP TABLE "Property";

-- DropTable
DROP TABLE "User";

-- CreateTable
CREATE TABLE "users" (
    "id" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "passwordHash" TEXT NOT NULL,
    "role" "Role" NOT NULL DEFAULT 'User',

    CONSTRAINT "users_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "properties" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,

    CONSTRAINT "properties_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "methods_properties" (
    "methodId" TEXT NOT NULL,
    "propertyId" TEXT NOT NULL,
    "parameters" JSONB NOT NULL,

    CONSTRAINT "methods_properties_pkey" PRIMARY KEY ("methodId","propertyId")
);

-- CreateTable
CREATE TABLE "methods" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,

    CONSTRAINT "methods_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "projects" (
    "id" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "country" TEXT NOT NULL,
    "operator" TEXT NOT NULL,

    CONSTRAINT "projects_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "projects_properties" (
    "projectId" TEXT NOT NULL,
    "propertyId" TEXT NOT NULL,
    "value" DOUBLE PRECISION NOT NULL,

    CONSTRAINT "projects_properties_pkey" PRIMARY KEY ("projectId","propertyId")
);

-- CreateTable
CREATE TABLE "projects_methods" (
    "projectId" TEXT NOT NULL,
    "methodId" TEXT NOT NULL,

    CONSTRAINT "projects_methods_pkey" PRIMARY KEY ("methodId","projectId")
);

-- CreateTable
CREATE TABLE "projects_users" (
    "projectId" TEXT NOT NULL,
    "userId" TEXT NOT NULL,

    CONSTRAINT "projects_users_pkey" PRIMARY KEY ("userId","projectId")
);

-- CreateIndex
CREATE UNIQUE INDEX "users_email_key" ON "users"("email");

-- CreateIndex
CREATE UNIQUE INDEX "properties_name_key" ON "properties"("name");

-- CreateIndex
CREATE UNIQUE INDEX "methods_name_key" ON "methods"("name");

-- AddForeignKey
ALTER TABLE "methods_properties" ADD CONSTRAINT "methods_properties_methodId_fkey" FOREIGN KEY ("methodId") REFERENCES "methods"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "methods_properties" ADD CONSTRAINT "methods_properties_propertyId_fkey" FOREIGN KEY ("propertyId") REFERENCES "properties"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_properties" ADD CONSTRAINT "projects_properties_projectId_fkey" FOREIGN KEY ("projectId") REFERENCES "projects"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_properties" ADD CONSTRAINT "projects_properties_propertyId_fkey" FOREIGN KEY ("propertyId") REFERENCES "properties"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_methods" ADD CONSTRAINT "projects_methods_projectId_fkey" FOREIGN KEY ("projectId") REFERENCES "projects"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_methods" ADD CONSTRAINT "projects_methods_methodId_fkey" FOREIGN KEY ("methodId") REFERENCES "methods"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_users" ADD CONSTRAINT "projects_users_projectId_fkey" FOREIGN KEY ("projectId") REFERENCES "projects"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "projects_users" ADD CONSTRAINT "projects_users_userId_fkey" FOREIGN KEY ("userId") REFERENCES "users"("id") ON DELETE RESTRICT ON UPDATE CASCADE;
