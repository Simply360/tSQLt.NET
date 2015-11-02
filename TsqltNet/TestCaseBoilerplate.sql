CREATE PROCEDURE [{{testClassSchemaName}}].[{{procedureName}}]
AS
BEGIN;
	PRINT 'Executing tSQLt test: [{{testClassSchemaName}}].[{{procedureName}}]';

	{{testCaseBody}}
END;