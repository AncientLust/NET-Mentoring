/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DELETE FROM dbo.Company;
DELETE FROM dbo.Employee;
DELETE FROM dbo.Person;
DELETE FROM dbo.Address;

INSERT INTO dbo.Address ([Street], [City], [State], [ZipCode]) 
VALUES 
    ('Street1', 'City1', 'State1', 'ZipCode1'),
    ('Street2', 'City2', 'State2', 'ZipCode2'),
    ('Street3', 'City3', 'State3', 'ZipCode3');


INSERT INTO dbo.Person ([FirstName], [LastName]) 
VALUES 
    ('FirstName1', 'LastName1'),
    ('FirstName2', 'LastName2'),
    ('FirstName3', 'LastName3');



INSERT INTO dbo.Company ([Name], [AddressId]) 
VALUES 
    ('CompanyName1', (SELECT [id] FROM dbo.Address WHERE [ZipCode] = 'ZipCode1')),
    ('CompanyName2', (SELECT [id] FROM dbo.Address WHERE [ZipCode] = 'ZipCode2')),
    ('CompanyName3', (SELECT [id] FROM dbo.Address WHERE [ZipCode] = 'ZipCode3'));


INSERT INTO dbo.Employee ([AddressId], [PersonId], [CompanyName], [Position], [EmployeeName]) 
VALUES 
    (
        (SELECT [id] FROM dbo.Address WHERE [ZipCode] = 'ZipCode1'), 
        (SELECT [id] FROM dbo.Person WHERE [FirstName] = 'FirstName1'),
        'CompanyName1',
        'Position1',
        'EmployeeName1'
    ),
    (
        (SELECT [id] FROM dbo.Address WHERE [ZipCode] = 'ZipCode2'), 
        (SELECT [id] FROM dbo.Person WHERE [FirstName] = 'FirstName2'),
        'CompanyName2',
        'Position2',
        null
    ),
    ( 
        (SELECT [id] FROM dbo.Address WHERE [ZipCode] = 'ZipCode3'), 
        (SELECT [id] FROM dbo.Person WHERE [FirstName] = 'FirstName3'),
        'CompanyName3',
        'Position3',
        'EmployeeName3'
    );