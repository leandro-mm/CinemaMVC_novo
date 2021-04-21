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
    public class SessaoControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //--Verifica se uma View é retornada quando o método SessoesController Index() é acionado        
            SessoesController controller = new SessoesController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            //--Verifica se uma sessão é retornada quando o método SessoesController Details() é acionado            
            SessoesController controller = new SessoesController();
            ViewResult result = controller.Details(6) as ViewResult;
            var sessao = (Sessao)result.ViewData.Model;
            Assert.AreEqual(DateTime.Now.Date, sessao.Data);
        }

        [TestMethod]
        public void TestDetailsRedirect()
        {
            //--verifica se é feito redirecionamento para Index quando uma ID com o valor - 1 é passada para o método Details().
            var controller = new SessoesController();
            var result = (RedirectToRouteResult)controller.Details(-1);
            Assert.AreEqual("Index", result.RouteName);
        }
    }
}
