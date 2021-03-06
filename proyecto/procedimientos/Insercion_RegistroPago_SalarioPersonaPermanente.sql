USE [DBFinca]
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertMultiplesSalarioPersonaPermanente]    Script Date: 07/03/2016 13:51:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE Insercion_RegistroPago_SalarioPersonaPermanente
	@DTPagosPersonaPermanente EstructuraSalarioPersonaPermanente READONLY
AS
	INSERT INTO RegistroPago (Fecha)
        values (CONVERT (date, GETDATE())) ;

     DECLARE @UltimoIdRegistroPago int
	 set @UltimoIdRegistroPago=  (SELECT top 1 idRegistroPago FROM  RegistroPago order by idRegistroPago desc);

	INSERT INTO SalarioPersonaPermanente (DocumentoPersona,idRegistroPago, Valor)
        SELECT DocumentoPersona,@UltimoIdRegistroPago, Valor_a_Pagar FROM  @DTPagosPersonaPermanente;



