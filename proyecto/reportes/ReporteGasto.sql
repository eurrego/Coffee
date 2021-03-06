USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[SP_CONSULTA_EGRESO]    Script Date: 2/03/2016 11:21:20 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[SP_CONSULTA_EGRESO]
(@FECHA_INI datetime,@FECHA_FIN datetime)
AS
BEGIN
	SELECT C.NombreConcepto, E.Descripcion, E.Fecha, E.Valor FROM Egreso E
	JOIN Concepto C on C.idConcepto= E.idConcepto
	WHERE E.Fecha BETWEEN cast(@FECHA_INI as date) AND cast(@FECHA_FIN as date )
END

--EXECUTE SP_CONSULTA_EGRESO '2015-01-01','2015-03-01'
