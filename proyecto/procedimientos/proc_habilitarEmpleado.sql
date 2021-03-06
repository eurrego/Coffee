USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[gestionPersona]    Script Date: 19/02/2016 7:40:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[gestionPersona]
(
@nombrePersona varchar(50),@genero char(1),@telefono varchar(10),@fechaNacimiento date, @documentoPerosna int, @opc int,
@TipoDocumento varchar(2) , @TipoContrato Varchar(10)
)

as
begin

set nocount on
declare @mensaje varchar(50)

if(@opc=1)
	 begin
		if ((select count(DocumentoPersona) from Persona where DocumentoPersona = @documentoPerosna) = 0)

			begin
				insert into Persona (TipoDocumento, DocumentoPersona,TipoContratoPersona,NombrePersona, Genero,Telefono,FechaNacimineto)
				values (@TipoDocumento,@documentoPerosna,@TipoContrato,@nombrePersona,@genero,@telefono,@fechaNacimiento)
				set @mensaje = 'Registro exitoso'
			end
		else
			begin
				set @mensaje = 'Existe un Empleado con este documento'
			end
	 end

else if(@opc=2)

		begin
		update Persona
			set
			NombrePersona=@nombrePersona,
			Genero=@genero,
			Telefono=@telefono,
			FechaNacimineto=@fechaNacimiento,
			TipoContratoPersona=@TipoContrato
			where DocumentoPersona = @documentoPerosna
			set @mensaje = 'Actualización exitosa!'

		end
else if (@opc = 3)
		
		begin
			update Persona
			set
			EstadoPersona = 'I'
			where DocumentoPersona = @documentoPerosna

			set @mensaje = 'Inhabilitación exitosa!'
		end

else if (@opc=4)   
		
		begin
			update Persona
			set
			EstadoPersona = 'A'
			where DocumentoPersona= @documentoPerosna

			set @mensaje = 'Habilitación exitosa!'
		end
		
	select @mensaje  as Mensaje


end
