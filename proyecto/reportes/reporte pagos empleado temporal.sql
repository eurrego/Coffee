
alter procedure [dbo].[SP_ReportePagos]
(@FECHA_INI date,@FECHA_FIN date)
AS
BEGIN

SELECT SPT.DocumentoPersona, PER.NombrePersona,LABLO.Fecha,LAB.NombreLabor ,sum(SPT.Cantidad * SPT.Valor) as "TotalPagado" FROM SalarioPersonaTemporal SPT
join (SELECT RPS.idSalarioPersonaTemporal FROM RegistroPagoSalario RPS
join (select REP_PAGOS.idRegistroPago from (SELECT RP.idRegistroPago FROM RegistroPago RP
									  WHERE RP.Fecha BETWEEN cast(@FECHA_INI as date) 
									  AND cast(@FECHA_FIN as date )) "REP_PAGOS") RP on RP.idRegistroPago = RPS.idRegistroPago) "IDSALPER"  on IDSALPER.idSalarioPersonaTemporal=SPT.idSalarioPersonaTemporal

join Persona PER on PER.DocumentoPersona=SPT.DocumentoPersona
join Labor_Lote LABLO on LABLO.idLabor_Lote= SPT.idLabor_Lote
join Labor LAB on LAB.idLabor= LABLO.idLabor
group by SPT.DocumentoPersona, PER.NombrePersona,SPT.Cantidad,LABLO.Fecha, LAB.NombreLabor , SPT.Valor
END

EXECUTE [SP_ReportePagos] '2015-01-01','2016-02-29'