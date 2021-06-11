using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WebApplication2;
using WebApplication2.Controllers;

namespace WebApplication2.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            KhuyenMaisController controller = new KhuyenMaisController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            KhuyenMaisController controller = new KhuyenMaisController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        /*[TestMethod]
        public void Delete()
        {
            // Arrange
            KhuyenMaisController controller = new KhuyenMaisController();

            // Act
            ViewResult result = controller.Delete() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }*/
    }
}
