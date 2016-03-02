alter proc ReporteCompraInsumos 
(@fecha_ini date, @fecha_fin date)

as 
begin

		select i.NombreInsumo, i.Marca,c.NumeroFactura,c.Fecha, p.NombreProveedor, sum(ci.Cantidad*ci.Precio) as Valor from Compra c
		join Proveedor p on p.Nit=c.NitProveedor
		join Compra_Insumo ci on ci.idCompra=c.idCompra
		join Insumo i on i.idInsumo= ci.idInsumo
		where c.Fecha between @fecha_ini and @fecha_fin
		group by i.NombreInsumo, i.Marca,c.NumeroFactura,c.Fecha, p.NombreProveedor
		order by c.Fecha

end

exec  ReporteCompraInsumos '2015-01-01','2016-03-01'