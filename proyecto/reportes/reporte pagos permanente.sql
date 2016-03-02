
alter procedure [dbo].[SP_ReportePagosPerma]
(@FECHA_INI date,@FECHA_FIN date)
AS
BEGIN

SELECT SPP.DocumentoPersona, PER.NombrePersona,RP.Fecha ,sum(SPP.Valor) as "TotalPagado" FROM SalarioPersonaPermanente SPP
join (select REP_PAGOS.idRegistroPago, REP_PAGOS.Fecha from (SELECT RP.idRegistroPago, RP.Fecha FROM RegistroPago RP
									  WHERE RP.Fecha BETWEEN cast(@FECHA_INI as date) 
									  AND cast(@FECHA_FIN as date )) "REP_PAGOS") RP on RP.idRegistroPago = SPP.IdRegistroPago

join Persona PER on PER.DocumentoPersona=SPP.DocumentoPersona
group by SPP.DocumentoPersona, PER.NombrePersona,RP.Fecha ,SPP.Valor

END

--EXECUTE [SP_ReportePagosPerma] '2015-01-01','2016-02-29'