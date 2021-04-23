using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcHtmlHelper = System.Web.Mvc.HtmlHelper;

namespace CinemaMVC.Helpers
{
    public static class HMTLHelperExtensions
    {
        public static string PageClass(this MvcHtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static MvcHtmlString ChosenInit(this System.Web.Mvc.HtmlHelper html)
            => MvcHtmlString.Create(
                   new StringBuilder()
                        .Append("<script type=\"text/javascript\">")
                            .Append("$(function () {")
                                .Append("var config = {")
                                    .Append("no_results_text: 'Nenhum registro encontrado...',")
                                    .Append("width: '100%',")
                                    .Append("search_contains: true")
                                .AppendLine("};")
                                .Append("$('.chosen-select').chosen(config);")
                            .Append("});")
                        .Append("</script>")
                        .ToString());

        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }
    }
}