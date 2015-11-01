CREATE PROCEDURE {{testClass}}.test_{{testName}}
AS
BEGIN
	PRINT 'Executing tSQLt test: {{testClass}}.test_{{testName}}';

	{{testCaseBody}}
END;