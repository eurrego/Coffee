create proc ReporteLaboresLote
(@idLote int, @fecha_ini date, @fecha_fin date)

as

begin
	
		select lt.NombreLote, l.NombreLabor,lbl.Fecha from Labor_Lote lbl
		join Labor l on l.idLabor= lbl.idLabor
		join Lote lt on lt.idLote = lbl.idLote
		where lbl.idLote = @idLote and lbl.Fecha between @fecha_ini and @fecha_fin
		order by lbl.Fecha
end

--execute ReporteLaboresLote 1,'2015-01-01','2016-02-27'