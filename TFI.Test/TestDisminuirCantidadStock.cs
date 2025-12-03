using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TFI.Dominio;

namespace TFI.Test
{
    [TestClass]
    public class TestDisminuirCantidadStock
    {

        [TestMethod]
        public void IngresarCantidadMayorMax()
        {
            Stock stock = new Stock();
            stock.Cantidad = 20;


            Assert.ThrowsException<Exception>(() => {
                stock.DisminuirCantidad(70);
            });
        }

        [TestMethod]
        public void IngresarCantidadExitosa()
        {
            Stock stock = new Stock();
            stock.Cantidad = 20;

            stock.DisminuirCantidad(5);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IngresarCantidadNegativa()
        {
            Stock stock = new Stock();
            stock.Cantidad = 20;

            Assert.ThrowsException<Exception>(() =>
            {
                stock.DisminuirCantidad(-5);
            });
        }

        [TestMethod]
        public void IngresarCantidadCero()
        {
            Stock stock = new Stock();
            stock.Cantidad = 20;

            Assert.ThrowsException<Exception>(() =>
            {
                stock.DisminuirCantidad(0);
            });
        }

    }
}
