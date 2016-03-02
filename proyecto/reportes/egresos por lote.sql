create proc SP_ReporteEgresosPorLote(@idLote int, @fecha_ini date, @fecha_fin date)
AS
BEGIN




	SELECT lot.NombreLote, lab.NombreLabor ,lbl.Fecha,(sum(spt1.Cantidad * spt1.Valor)+isnull(t.[Valor Insumo],0)) as "Total Egreso" from SalarioPersonaTemporal spt1 
			join Labor_Lote lbl on lbl.idLabor_Lote= spt1.idLabor_Lote
			join Labor lab on lab.idLabor=lbl.idLabor
			join Lote lot on lot.idLote=lbl.idLote
			left join (select sum(Cantidad*Precio) as "Valor Insumo", idLabor_Lote from LaborLote_Insumo group by idLabor_Lote) t on t.idLabor_Lote=lbl.idLabor_Lote
			where  spt1.idLabor_Lote=lbl.idLabor_Lote and lbl.idLote=@idLote and lbl.Fecha between @fecha_ini and @fecha_fin
			group by spt1.idLabor_Lote, lot.NombreLote,lab.NombreLabor,lbl.Fecha,t.[Valor Insumo]
			
			

											
end																			

	
--execute SP_ReporteEgresosPorLote 1,'2015-01-01','2016-02-28'

	


--END

EXECUTE SP_ReporteEgresosPorLote 1,'2015-01-01','2016-03-01'



