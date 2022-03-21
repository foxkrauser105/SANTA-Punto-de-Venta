

  UPDATE registro_ventas
  SET    precio = productosagregados.precios
  FROM   registro_ventas r, (SELECT   p.id_producto, p.precio precios
                             FROM     productos p,   registro_ventas r
							 WHERE    p.id_producto  = r.id_producto
							 GROUP BY p.id_producto, p.precio) productosagregados
  WHERE  r.id_producto = productosagregados.id_producto;

  SELECT  id_producto, precio
  FROM    productos;

  SELECT  id_producto, precio
  FROM    registro_ventas;

  

  