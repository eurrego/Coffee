USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[gestionTipoArboles]    Script Date: 2/03/2016 1:00:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------GESTION TIPO ARBOLES---------------------
ALTER proc [dbo].[gestionTipoArboles]
(@NombreArbol varchar (45),@Descripcion varchar(45),@idTipoArbol int, @tiempoProduccion int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreTipoArbol) from TipoArbol where NombreTipoArbol = @NombreArbol) = 0)
				begin
					insert into TipoArbol(NombreTipoArbol,Descripcion, TiempoProduccion)
					values (@NombreArbol,@Descripcion, @tiempoProduccion)
					set @mensaje = 'Registro exitoso!'
				end

			 else
				begin
					set @mensaje = 'Existe un tipo de arbol con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			update TipoArbol
			set
			NombreTipoArbol = @NombreArbol,
			Descripcion = @Descripcion,
			TiempoProduccion = @tiempoProduccion
			where idTipoArbol = @idTipoArbol
			set @mensaje = 'Actualización exitosa!'
		end

	else if (@opc = 3)
		
		begin
			update TipoArbol
			set
			EstadoTipoArbol = 'i'
			where idTipoArbol= @idTipoArbol

			set @mensaje = 'Inhabilitación exitosa!'
		end
   else if (@opc=4)   
		
		begin
			update TipoArbol
			set
			EstadoTipoArbol = 'A'
			where idTipoArbol= @idTipoArbol

			set @mensaje = 'Habilitación exitosa!'
		end

	select @mensaje  as Mensaje

end

