USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[gestionConcepto]    Script Date: 13/02/2016 9:26:43 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[gestionConcepto]
(@NombreConcepto varchar (45),@Descripcion varchar(45),@idConcepto int, @opc int)
as
begin

	set nocount on
	declare @mensaje varchar(50)
	if	(@opc =1)
		begin

			if ((select count(NombreConcepto) from Concepto where NombreConcepto = @NombreConcepto) = 0)
				begin
					insert into Concepto (NombreConcepto,Descripcion)
					values (@NombreConcepto,@Descripcion)
					set @mensaje = 'Registro exitoso!'
				end

			 else
				begin
					set @mensaje = 'Existe un concepto con este nombre'
				end
		end

	 else if (@opc =2)

		begin
			update Concepto
			set
			NombreConcepto = @NombreConcepto,
			Descripcion = @Descripcion
			where idConcepto = @idConcepto
			set @mensaje = 'Actualización exitosa!'
		end

	else if (@opc=3)   
		
		begin
			update Concepto
			set
			EstadoConcepto = 'I'
			where idConcepto= @idConcepto

			set @mensaje = 'Inhabilitación exitosa!'
		end
	else if (@opc=4)   
		
		begin
			update Concepto
			set
			EstadoConcepto = 'A'
			where idConcepto= @idConcepto

			set @mensaje = 'Habilitación exitosa!'
		end
	select @mensaje  as Mensaje

end
