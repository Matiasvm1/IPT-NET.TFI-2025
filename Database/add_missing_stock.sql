-- Agregar stock para productos faltantes
USE IvcDb;
GO

-- Agregar stock para Campera Deportiva (Id=3)
INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (15, 3, 2, 100, 10); -- S

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (20, 3, 3, 100, 10); -- M

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (18, 3, 4, 100, 10); -- L

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (12, 3, 5, 100, 10); -- XL

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (8, 3, 6, 100, 10); -- XXL

-- Agregar stock para Buzo con Capucha (Id=4)
INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (25, 4, 1, 100, 10); -- XS

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (30, 4, 2, 100, 10); -- S

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (35, 4, 3, 100, 10); -- M

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (28, 4, 4, 100, 10); -- L

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (22, 4, 5, 100, 10); -- XL

INSERT INTO Stock (Cantidad, Indumentaria_Id, Talle_Id, CantidadMaxima, CantidadMinima) 
VALUES (15, 4, 6, 100, 10); -- XXL

PRINT 'Stock agregado exitosamente para Campera Deportiva y Buzo con Capucha';
GO
