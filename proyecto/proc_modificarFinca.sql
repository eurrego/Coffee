USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[ModificarFinca]    Script Date: 1/03/2016 10:09:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[ModificarFinca]
(
@nombreFinca varchar(25),
@Propietario varchar(45),
@idMunicipio int,
@Vereda varchar(50),
@telefono varchar(10),
@hectareas varchar(5)
)


as

begin

 declare @Mensaje varchar(50) 

	update Finca
	set
	NombreFinca = @nombreFinca,
	Propietario = @Propietario,
	idMunicipio = @idMunicipio,
	Vereda = @Vereda,
	Telefono = @telefono,
	Cuadras = @hectareas

 set @Mensaje = 'Actualización exitosa'

 select @Mensaje as Mensaje
	
end