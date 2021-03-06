USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[gestionInsumo]    Script Date: 14/02/2016 5:52:49 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--------------------------------------------------------------INSUMOS------------

 ALTER proc [dbo].[gestionInsumo] 
 (@idTipoInsumo tinyint, @nombreInsumo varchar(45), 
 @descripcion varchar(150), @marca varchar(45),
 @unidadMedida varchar(45),@idInsumo int,@opc int)

 as
 begin
	 set nocount on
	 declare @mensaje varchar(50)
	 if(@opc = 1)
		begin
			if ((select count(nombreInsumo) from Insumo where NombreInsumo = @nombreInsumo) = 0)
				 begin
					 insert into Insumo (idTipoInsumo, NombreInsumo,Descripcion,Marca,UnidadMedida)
					 values (@idTipoInsumo,@nombreInsumo,@descripcion,@marca,@unidadMedida)
					 set @mensaje = 'Registro exitoso!'
				 end

			else
				begin
					set @mensaje = 'Existe un Insumo con este nombre'
				end
		end

	  else if(@opc = 2)
			 begin
				 update Insumo
				 set
				 idTipoInsumo = @idTipoInsumo,
				 NombreInsumo = @nombreInsumo,
				 Descripcion = @descripcion,
				 Marca = @marca,
				 UnidadMedida = UnidadMedida
				 where idInsumo = @idInsumo
				 set @mensaje = 'Actualización exitosa!'
			end

	 else if (@opc = 3)
			begin
				update Insumo
				set
				EstadoInsumo = 'I'
				where idinsumo= @idInsumo

				set @mensaje = 'Inhabilitación exitosa!'
		end
	else if (@opc=4)   
		begin
			update Insumo
			set
			EstadoInsumo = 'A'
			where idinsumo= @idInsumo

			set @mensaje = 'Habilitación exitosa!'
		end

			select @mensaje  as Mensaje
 end
