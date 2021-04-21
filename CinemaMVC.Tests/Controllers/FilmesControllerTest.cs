using CinemaMVC.Controllers;
using CinemaMVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CinemaMVC.Tests.Controllers
{
    [TestClass]
    public class FilmesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //--Verifica se uma View é retornada quando o método FilmesController Index() é acionado        
            FilmesController controller = new FilmesController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            //--Verifica se um Filme é retornado quando o método FilmesController Details() é acionado            
            FilmesController controller = new FilmesController();
            ViewResult result = controller.Details(6) as ViewResult;
            var filme = (Filme)result.ViewData.Model;
            Assert.AreEqual("O Poderoso Chefão - Parte 2", filme.Titulo);
        }

        [TestMethod]
        public void TestDetailsRedirect()
        {
            //--verifica se é feito redirecionamento para Index quando uma ID com o valor - 1 é passada para o método Details().
            var controller = new FilmesController();
            var result = (RedirectToRouteResult)controller.Details(-1);
            Assert.AreEqual("Index", result.RouteName);
        }

    }//end class
}
