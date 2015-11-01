CREATE PROCEDURE {{testClass}}.{{testName}}
AS
BEGIN
	PRINT 'Executing tSQLt test: {{testClass}}.{{testName}}';

	{{testCaseBody}}
END;