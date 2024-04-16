-- CreateTable
CREATE TABLE "calculations" (
    "projectId" TEXT NOT NULL,
    "methodId" TEXT NOT NULL,
    "ratio" DOUBLE PRECISION NOT NULL,

    CONSTRAINT "calculations_pkey" PRIMARY KEY ("methodId","projectId")
);

-- CreateTable
CREATE TABLE "calculation_items" (
    "calculationMethodId" TEXT NOT NULL,
    "calculationProjectId" TEXT NOT NULL,
    "propertiesId" TEXT NOT NULL,
    "ratio" DOUBLE PRECISION NOT NULL,

    CONSTRAINT "calculation_items_pkey" PRIMARY KEY ("calculationMethodId","calculationProjectId","propertiesId")
);

-- AddForeignKey
ALTER TABLE "calculations" ADD CONSTRAINT "calculations_projectId_fkey" FOREIGN KEY ("projectId") REFERENCES "projects"("id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "calculations" ADD CONSTRAINT "calculations_methodId_fkey" FOREIGN KEY ("methodId") REFERENCES "methods"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_calculationMethodId_calculationProjectId_fkey" FOREIGN KEY ("calculationMethodId", "calculationProjectId") REFERENCES "calculations"("methodId", "projectId") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "calculation_items" ADD CONSTRAINT "calculation_items_propertiesId_fkey" FOREIGN KEY ("propertiesId") REFERENCES "properties"("id") ON DELETE RESTRICT ON UPDATE CASCADE;
