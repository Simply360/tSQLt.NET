CREATE PROCEDURE [{{testClassSchemaName}}].[{{procedureName}}]
AS
BEGIN;
	PRINT 'Executing tSQLt test: [{{escapedTestClassSchemaName}}].[{{escapedProcedureName}}]';

	{{testCaseBody}}
END;