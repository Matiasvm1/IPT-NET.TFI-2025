using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TFI.Dominio;

namespace TFI.Test
{
    [TestClass]
    public class TestCalcularVuelto
    {
        [TestMethod]
        public void CalcularVueltoConImporteNegativo()
        {
            
            Indumentaria indumentariaDePrueba = new Indumentaria()
            {
                Precio = 300
            };
            Venta ventaPrueba = new Venta()
            {
                LineaDeVentas = new List<LineaDeVenta>()
                {
                    new LineaDeVenta(indumentariaDePrueba,5)
                }
            };

            Assert.ThrowsException<Exception>(() =>
            {
                ventaPrueba.CalcularVuelto(-80);
            });
        }

        [TestMethod]
        public void CalcularVueltoImporteSuficiente()
        {
            Indumentaria indumentariaDePrueba = new Indumentaria()
            {
                Precio = 300
            };
            Venta ventaPrueba = new Venta()
            {
                LineaDeVentas = new List<LineaDeVenta>()
                {
                    new LineaDeVenta(indumentariaDePrueba,5)
                }
            };

            var vuelto = ventaPrueba.CalcularVuelto(2000);

            Assert.AreEqual(500,vuelto);
        }

        [TestMethod]
        public void CalcularVueltoImporteInsuficiente()
        {
            Indumentaria indumentariaDePrueba = new Indumentaria()
            {
                Precio = 300
            };
            Venta ventaPrueba = new Venta()
            {
                LineaDeVentas = new List<LineaDeVenta>()
                {
                    new LineaDeVenta(indumentariaDePrueba,5)
                }
            };

            Assert.ThrowsException<Exception>(() =>
            {
                ventaPrueba.CalcularVuelto(500);
            });
        }
    }
}
