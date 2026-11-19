USE SchoolDb;
GO

IF OBJECT_ID('dbo.Students', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students (
        StudentId NVARCHAR(20) NOT NULL PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Course NVARCHAR(50) NOT NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'332')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'332', N'Arjane', N'BSIT');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'334')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'334', N'Jereyme', N'BSIT');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'3412')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'3412', N'Arjane', N'BSIT');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'4240')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'4240', N'Khiemi', N'BSIT');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'544')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'544', N'Jerome', N'BSIT');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'67')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'67', N'Lad', N'BSIT');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'690')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'690', N'Joey', N'BSIS');
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE StudentId = N'765')
    INSERT INTO dbo.Students (StudentId, Name, Course) VALUES (N'765', N'Sophia', N'BSP');
GO