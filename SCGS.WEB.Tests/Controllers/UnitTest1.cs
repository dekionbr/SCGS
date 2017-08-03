using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCGS.WEB.Controllers;
using System.Web.Mvc;

namespace SCGS.WEB.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            RelatoriosController controller = new RelatoriosController();

            // Act
            JsonResult result = controller.Funcionarios(4) as JsonResult;

            Assert.IsTrue(result.Data != null, "Foi");
        }
    }
}
