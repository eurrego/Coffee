USE DBFinca;
GO

create proc exportarDB
(@rutaEscritorio varchar (1000))
as
begin

DECLARE @backupPath varchar(1000);
SET @backupPath = @rutaEscritorio +'\\BDFinca.Bak'

BACKUP DATABASE DBFinca
TO DISK = @backupPath
   WITH FORMAT,
      MEDIANAME = 'C_SQLServerBackups',
      NAME = 'Backup of DBFINCA';
end

GO


USE master;
GO

create proc exportarDB
(@ruta varchar (1000))
as
begin



DECLARE @backupPath nvarchar(400);
DECLARE @sourceDb nvarchar(50);
DECLARE @sourceDb_log nvarchar(50);
DECLARE @destDb nvarchar(50);
DECLARE @destMdf nvarchar(100);
DECLARE @destLdf nvarchar(100);
DECLARE @sqlServerDbFolder nvarchar(100);

SET @sourceDb = 'DBFinca'
SET @sourceDb_log = @sourceDb + '_log'
SET @backupPath = 'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\BDFinca.Bak'
SET @sqlServerDbFolder = 'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\'

SET @destDb = 'DBFinca'
SET @destMdf = @sqlServerDbFolder + @destDb + '.mdf'
SET @destLdf = @sqlServerDbFolder + @destDb + '_log' + '.ldf'

BACKUP DATABASE @sourceDb TO DISK = @backupPath

RESTORE DATABASE @destDb FROM DISK = @backupPath
WITH REPLACE,
   MOVE @sourceDb     TO @destMdf,
   MOVE @sourceDb_log TO @destLdf