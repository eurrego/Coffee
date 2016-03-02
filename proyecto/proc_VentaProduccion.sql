
alter proc VentaProduccion
(@Cantidad decimal(10,2))
as


	begin 
		declare @cantidadKilos decimal(10,2) = @Cantidad * 125
		declare @idProduccion int
		declare @CantidadRestante int
		
		declare @idVenta int = (select TOP 1 idVenta from Venta order by idVenta desc )
		
	

		while(@cantidadKilos != 0 )

			begin  

				set @idProduccion  = (select TOP 1 idProduccion from Produccion where EstadoProduccion  = 'NV' order by idProduccion ) 

				set @CantidadRestante  = (select  isnull (P.Cantidad - sum(MP.Cantidad),P.Cantidad) as CantidadRestante from Produccion P
				left join MovimientoProduccion MP
				on p.idProduccion = MP.idProduccion
				where P.idProduccion = @idProduccion and P.EstadoProduccion = 'NV'
				group by P.Cantidad)
			

				if (@CantidadRestante < @cantidadKilos)
					begin

					insert into MovimientoProduccion (idProduccion,idVenta,Cantidad)
							values (@idProduccion,@idVenta,@CantidadRestante)
					
					set @cantidadKilos = @cantidadKilos-@CantidadRestante

					update Produccion
						set 
						EstadoProduccion = 'VC'

						where idProduccion = @idProduccion


					
					end

				else

					begin

						insert into MovimientoProduccion (idProduccion,idVenta,Cantidad)
								values (@idProduccion,@idVenta,@cantidadKilos)

						set @cantidadKilos = 0
						

					end

			end
			select 'Registro exitoso' as Mensaje		

	end