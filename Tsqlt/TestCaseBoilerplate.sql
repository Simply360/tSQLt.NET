CREATE PROCEDURE {{testClass}}.test {{testName}}
AS
BEGIN
	PRINT 'Executing tSQLt test: {{testClass}}.test {{testName}}';

	{{testCaseBody}}
END;