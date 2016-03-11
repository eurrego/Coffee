USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[gestionArboles2]    Script Date: 11/03/2016 9:52:01 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[gestionArboles2]
(@idLote smallint ,@idTipoArbol tinyint, @cantidad int, @fecha datetime)

as
begin

	set nocount on
	declare @mensaje varchar(50)

	declare @idArboles int
	


		if ((select count(idLote) from Arboles where idLote =  @idLote ) = 0)
		 begin
		       insert into Arboles(idLote,idTIpoArbol,Cantidad)
				      values (@idLote,@idTipoArbol,'0')
		 end
		else
		 begin
		    if ((select count(idTIpoArbol) from Arboles where idTIpoArbol =  @idTipoArbol and idLote = @idLote) = 0)
				begin
					insert into Arboles(idLote,idTIpoArbol,Cantidad)
					values (@idLote,@idTipoArbol,'0')	
			end
		 end

			set @idArboles = (select idArboles from Arboles where idTIpoArbol = @idTipoArbol and idLote = @idLote)

			insert into MovimientosArboles (idArboles, Fecha, Cantidad)
			values (@idArboles, @fecha,  @cantidad)

			

		 set @mensaje = 'Registro exitoso!'
	


		select @mensaje as Mensaje
end



