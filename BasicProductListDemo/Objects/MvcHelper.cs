using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductListMVCDemo.Objects
{
    public class MvcHelper
    {
        /// <summary>
        /// Renders a partial view from a controller to a string.
        /// </summary>
        /// <returns>The view rendered into an HTML string</returns>
        public static string RenderControllerPartialViewToString(Controller currentController, string viewName, object model = null)
        {
            currentController.ViewData.Model = model;

            using (StringWriter viewWriter = new StringWriter())
            {
                // find the partial view
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(currentController.ControllerContext, viewName);

                // create view context
                ViewContext viewContext = new ViewContext(currentController.ControllerContext, viewResult.View, currentController.ViewData, currentController.TempData, viewWriter);

                // render the view to viewWriter
                viewResult.View.Render(viewContext, viewWriter);
                
                return viewWriter.ToString();
            }
        }
    }
}