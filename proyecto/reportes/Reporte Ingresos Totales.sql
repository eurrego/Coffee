create proc reporteIngresosTotales
(@fecha_inicial date, @fecha_fin date )

as

begin 

		select p.NombreProducto,Fecha,sum(v.PrecioCarga*v.CantidadCargas) as Valor from Venta v
		join Producto p on p.idProducto=v.idProducto
		where Fecha between @fecha_inicial and @fecha_fin
		group by  p.NombreProducto ,v.Fecha, v.PrecioCarga, v.CantidadCargas


end


