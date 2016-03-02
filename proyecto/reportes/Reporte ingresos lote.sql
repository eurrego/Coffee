create proc ReporteIngresosLote
 (@idLote int, @fecha_ini date, @fecha_fin date)

 as

 begin

		select v.idVenta,((sum(MP.Cantidad) /125)*v.PrecioCarga) as valor,l.NombreLote, v.Fecha  valor from Venta v
		inner join MovimientoProduccion MP
		on v.idVenta = MP.idVenta
		inner join Produccion P
		on P.idProduccion = MP.idProduccion
		inner join Lote L
		on l.idLote = P.idLote
		where v.Fecha  between @fecha_ini and @fecha_fin and  L.idLote= @idLote   
		group by v.idVenta,v.PrecioCarga,l.NombreLote,v.Fecha

end