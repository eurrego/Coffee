create proc reporteProduccion
(@idLote int, @fecha_ini date, @fecha_fin date) 

as

begin
    
	select l.NombreLote, p.Fecha, p.Cantidad from Produccion p
    join Lote l on l.idLote = p.idLote
	where p.Fecha between @fecha_ini  and @fecha_fin and l.idLote=@idLote
	order by p.Fecha
end


--execute reporteProduccion 1,'2015-01-01','2016-02-27'