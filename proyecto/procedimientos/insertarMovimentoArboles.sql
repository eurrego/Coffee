USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[insertarMovimentoArboles]    Script Date: 11/03/2016 9:53:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[insertarMovimentoArboles]
	(@dtMovimientoArboles MovimientoArboles READONLY)
AS
begin

	
	update ma
	set ma.Cantidad = ma.Cantidad - dtma.Cantidad
	from MovimientosArboles ma
	inner join @dtMovimientoArboles as dtma
	on ma.idMovimientosArboles = dtma.idMovimiento
	
	delete from MovimientosArboles  
	where Cantidad = 0


end