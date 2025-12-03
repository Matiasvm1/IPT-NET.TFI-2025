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
            Stock stock = new Stock(100, 10);
            stock.Cantidad = 20;


            Assert.ThrowsException<Exception>(() => {
                stock.DisminuirCantidad(150);
            });
        }

        [TestMethod]
        public void IngresarCantidadExitosa()
        {
            Stock stock = new Stock(100, 10);
            stock.Cantidad = 20;

            stock.DisminuirCantidad(5);
            
            Assert.AreEqual(15, stock.Cantidad);
        }

        [TestMethod]
        public void IngresarCantidadNegativa()
        {
            Stock stock = new Stock(100, 10);
            stock.Cantidad = 20;

            Assert.ThrowsException<Exception>(() =>
            {
                stock.DisminuirCantidad(-5);
     });
        }

        [TestMethod]
        public void IngresarCantidadCero()
        {
            Stock stock = new Stock(100, 10);
            stock.Cantidad = 20;

            Assert.ThrowsException<Exception>(() =>
            {
                stock.DisminuirCantidad(0);
         });
 }

 }
}
