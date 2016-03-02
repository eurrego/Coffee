create proc ReporteDeudaEmpleados
(@fecha_ini date, @fecha_fin date)

as

begin 

select t.DocumentoPersona,p.NombrePersona,sum(t.valor) as "Valor Deuda" from
		(select dp.DocumentoPersona,sum(dp.Valor)-isnull(a.total_abono,0) as valor from DeudaPersona dp
			join Persona p on p.DocumentoPersona= dp.DocumentoPersona
			left join (select ab.idDeudaPersona, SUM(ab.Valor) as total_abono from AbonoDeuda ab group by  ab.idDeudaPersona) a on a.idDeudaPersona=dp.idDeudaPersona
			where dp.Fecha between @fecha_ini and @fecha_fin and dp.EstadoCuenta='D'
			group by dp.DocumentoPersona,a.total_abono)  t
join Persona p on p.DocumentoPersona=t.DocumentoPersona
group by t.DocumentoPersona,p.NombrePersona


end
 
 --execute ReporteDeudaEmpleados '2015-01-01','2016-02-28'