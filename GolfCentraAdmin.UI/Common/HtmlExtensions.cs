using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;

namespace GolfCentraAdmin.UI.Common
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Generate Breadcrumb Navigation
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string BuildBreadcrumbNavigation(this System.Web.Mvc.HtmlHelper helper)
        {
            var menulist = (List<GolfCentraAdmin.ViewModel.PageViewModel>)HttpContext.Current.Session["NavigationMenu"];
            var action = helper.ViewContext.RouteData.Values["action"].ToString();
            var controller = helper.ViewContext.RouteData.Values["controller"].ToString();
            var menuitem = menulist.Find(x => x.ActionName.ToLower() == action.ToLower() && x.ControllerName.ToLower() == controller.ToLower());
            // optional condition: I didn't wanted it to show on  account controller
            if (controller.ToLower() == "account" || menuitem == null)
            {
                return string.Empty;
            }
            StringBuilder breadcrumb = new StringBuilder("<ol class='breadcrumb'><li>").Append(helper.ActionLink("Dashboard", "Index", "Dashboard", new { }, new { target = "_self" }).ToHtmlString()).Append("</li>");

            if (menuitem != null && menuitem.ParentId == 0)
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ActionLink(menuitem.PageName.Titleize(),
                                                    action,
                                                    controller, new { }, new { target = "_self" }));
                breadcrumb.Append("</li>");
            }
            else
            {
                var item = menulist.Find(x => x.PageId == menuitem.ParentId);
                breadcrumb.Append("<li>");
                breadcrumb.Append(item.PageName);
                breadcrumb.Append("</li>");
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ActionLink(menuitem.PageName.Titleize(),
                                                                   action,
                                                                   controller, new { }, new { target = "_self" }));
                breadcrumb.Append("</li>");
            }
            return breadcrumb.Append("</ol>").ToString();
        }
    }
}