USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[gestionTipoInsumo]    Script Date: 14/02/2016 12:10:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------TIPO INSUMO---------------------------------

ALTER proc [dbo].[gestionTipoInsumo]
(@NombreTipoInsumo varchar (45),@Descripcion varchar(45),@idTipoInsumo int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreTipoInsumo) from TipoInsumo where NombreTipoInsumo = @NombreTipoInsumo) = 0)
				begin
					insert into TipoInsumo (NombreTipoInsumo,Descripcion)
					values (@NombreTipoInsumo,@Descripcion)
					set @mensaje = 'Registro exitoso!'
				end

			 else
				begin
					set @mensaje = 'Existe un Insumo con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			update TipoInsumo
			set
			NombreTipoInsumo = @NombreTipoInsumo,
			Descripcion = @Descripcion
			where idTipoInsumo = @idTipoInsumo
			set @mensaje = 'Actualización exitosa!'
		end

	else if (@opc = 3)
		
		begin
			update TipoInsumo
			set
			EstadoTipoInsumo = 'I'
			where idTipoInsumo= @idTipoInsumo

			set @mensaje = 'Inhabilitación exitosa!'
		end
    else if (@opc=4)   
		
		begin
			update TipoInsumo
			set
			EstadoTipoInsumo = 'A'
			where idTipoInsumo= @idTipoInsumo

			set @mensaje = 'Habilitación exitosa!'
		end
	select @mensaje  as Mensaje

end
