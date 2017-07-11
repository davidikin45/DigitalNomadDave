using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using Solution.Base.APIs;
using HtmlTags;
using System.Web;
using Solution.Base.Extensions;
using System.IO;
using System.Web.Hosting;
using System.Data.Entity.Spatial;
using System.Globalization;
using Solution.Base.Interfaces.Persistance;
using Solution.Base.ModelMetadata;
using Solution.Base.Interfaces.Repository;
using Solution.Base.Interfaces.Services;

namespace Solution.Base.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static T GetInstance<T>(this HtmlHelper html)
        {
            return (T)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(T));
        }

        public static IFileSystemRepositoryFactory FileSystemRepositoryFactory(this HtmlHelper html)
        {
            var repos = html.GetInstance<IFileSystemRepositoryFactory>();
            return repos;
        }

        public static string ContentHtml(this HtmlHelper html, string id)
        {
            string content = "";
            var service = html.GetInstance<IContentHtmlService>();

            var dto = service.GetById(id);

            if (dto != null)
            {
                content = dto.HTML;
            }

            return content;
        }

        public static string ContentText(this HtmlHelper html, string id)
        {
            string content = "";
            var service = html.GetInstance<IContentTextService>();

            var dto = service.GetById(id);

            if (dto != null)
            {
                content = dto.Text;
            }

            return content;
        }

        public static IBaseDbContext Database(this HtmlHelper html)
        {
            var dbContext = html.GetInstance<IDbContextFactory>().CreateDefault();
            return dbContext;
        }

        public static TIDbContext Database<TIDbContext>(this HtmlHelper html) where TIDbContext : IBaseDbContext
        {
            var dbContext = html.GetInstance<IDbContextFactory>().Create<TIDbContext>();
            return dbContext;
        }

        public static HtmlString ClientIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return new HtmlString(
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)));
        }

        public static IHtmlString Grid(this HtmlHelper<dynamic> html, Boolean details, Boolean edit, Boolean delete, Boolean sorting)
        {
            var table = new TagBuilder("table");
            table.AddCssClass("table");
            table.AddCssClass("table-striped");

            var thead = new TagBuilder("thead");

            var theadTr = new TagBuilder("tr");

            Boolean hasActions = (details || edit || delete);

            foreach (var prop in html.ViewData.ModelMetadata().Properties
            .Where(p => p.ShowForDisplay && (!p.AdditionalValues.ContainsKey("ShowForGrid") || (Boolean)p.AdditionalValues["ShowForGrid"])))
            {
                var th = new TagBuilder("th");

                var orderType = OrderByType.Descending;
                if (html.ViewBag.OrderColumn.ToLower() == prop.PropertyName.ToLower() && html.ViewBag.OrderType != OrderByType.Ascending)
                {
                    orderType = OrderByType.Ascending;
                }

                string linkText = ModelHelperExtensions.DisplayName(html.ViewData.Model, prop.PropertyName).ToString();
                string link;

                Boolean enableSort = sorting;
                if (prop.AdditionalValues.ContainsKey("AllowSortForGrid"))
                {
                    enableSort = (Boolean)prop.AdditionalValues["AllowSortForGrid"];
                }

                if (enableSort)
                {
                    link = html.ActionLink(linkText, "Index", new { page = html.ViewBag.Page, pageSize = html.ViewBag.PageSize, search = html.ViewBag.Search, orderColumn = prop.PropertyName, orderType = orderType }).ToString();
                }
                else
                {
                    link = linkText;
                }

                // th.InnerHtml = ModelHelperExtensions.DisplayName(html.ViewData.Model, prop.PropertyName).ToString();
                th.InnerHtml = link.ToString();

                theadTr.InnerHtml += th.ToString();
            }

            if (hasActions)
            {
                var thActions = new TagBuilder("th");
                theadTr.InnerHtml += thActions.ToString();
            }

            thead.InnerHtml = theadTr.ToString();

            table.InnerHtml += thead.ToString();

            var tbody = new TagBuilder("tbody");

            foreach (var item in html.ViewData.Model)
            {
                var tbodytr = new TagBuilder("tr");

                foreach (var prop in html.ViewData.ModelMetadata().Properties
               .Where(p => p.ShowForDisplay && (!p.AdditionalValues.ContainsKey("ShowForGrid") || (Boolean)p.AdditionalValues["ShowForGrid"])))
                {
                    var td = new TagBuilder("td");

                    if (prop.AdditionalValues.ContainsKey("DropdownModelType"))
                    {
                        string value = ModelHelperExtensions.Display(html, item, prop.PropertyName).ToString();
                        td.InnerHtml += value;
                    }
                    else if (prop.ModelType == typeof(FileInfo))
                    {
                        string value = ModelHelperExtensions.Display(html, item, prop.PropertyName).ToString();
                        td.InnerHtml += value;
                    }
                    else if (prop.ModelType == typeof(DbGeography))
                    {
                        var model = (DbGeography)item.GetType().GetProperty(prop.PropertyName).GetValue(item, null);
                        if (model != null && model.Latitude.HasValue && model.Longitude.HasValue)
                        {
                            string value = model.Latitude.Value.ToString("G", CultureInfo.InvariantCulture) + "," + model.Longitude.Value.ToString("G", CultureInfo.InvariantCulture);
                            td.InnerHtml += value;
                        }
                    }
                    else
                    {
                        string value = ModelHelperExtensions.DisplayTextSimple(item, prop.PropertyName).ToString();
                        td.InnerHtml += value.Truncate(70);
                    }

                    tbodytr.InnerHtml += td.ToString();
                }

                if (hasActions)
                {
                    var tdActions = new TagBuilder("td");

                    if (details)
                    {
                        tdActions.InnerHtml += html.ActionLink("Details", "Details", new { id = item.Id }).ToString();
                        if (edit || delete)
                        {
                            tdActions.InnerHtml += " | ";
                        }
                    }

                    if (edit)
                    {
                        tdActions.InnerHtml += html.ActionLink("Edit", "Edit", new { id = item.Id }).ToString();
                        if (delete)
                        {
                            tdActions.InnerHtml += " | ";
                        }
                    }

                    if (delete)
                    {
                        tdActions.InnerHtml += html.ActionLink("Delete", "Delete", new { id = item.Id }).ToString();
                    }

                    tbodytr.InnerHtml += tdActions.ToString();
                }

                tbody.InnerHtml += tbodytr.ToString();
            }

            table.InnerHtml += tbody.ToString();

            return new HtmlString(table.ToString());
        }

        public static string Controller(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }

        public static IHtmlString BootstrapPager(this HtmlHelper html, int currentPageIndex, Func<int, string> action, int totalItems, int pageSize = 10, int numberOfLinks = 5)
        {
            if (totalItems <= 0)
            {
                return MvcHtmlString.Empty;
            }
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var lastPageNumber = (int)Math.Ceiling((double)currentPageIndex / numberOfLinks) * numberOfLinks;
            var firstPageNumber = lastPageNumber - (numberOfLinks - 1);
            var hasPreviousPage = currentPageIndex > 1;
            var hasNextPage = currentPageIndex < totalPages;
            if (lastPageNumber > totalPages)
            {
                lastPageNumber = totalPages;
            }

            var nav = new TagBuilder("nav");
            nav.AddCssClass("pagination-nav");

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            ul.AddCssClass("justify-content-center");
            ul.InnerHtml += AddLink(1, action, currentPageIndex == 1, "disabled", "<<", "First Page", false);
            ul.InnerHtml += AddLink(currentPageIndex - 1, action, !hasPreviousPage, "disabled", "<", "Previous Page", false);
            for (int i = firstPageNumber; i <= lastPageNumber; i++)
            {
                ul.InnerHtml += AddLink(i, action, i == currentPageIndex, "active", i.ToString(), i.ToString(), false);
            }
            ul.InnerHtml += AddLink(currentPageIndex + 1, action, !hasNextPage, "disabled", ">", "Next Page", true);
            ul.InnerHtml += AddLink(totalPages, action, currentPageIndex == totalPages, "disabled", ">>", "Last Page", false);

            nav.InnerHtml += ul;

            return new HtmlString(nav.ToString());
        }

        //@Html.BootstrapPager(pageIndex, index => Url.Action("Index", "Product", new { pageIndex = index }), Model.TotalCount, numberOfLinks: 10)
        private static TagBuilder AddLink(int index, Func<int, string> action, bool condition, string classToAdd, string linkText, string tooltip, bool nextPage)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("page-item");
            li.MergeAttribute("title", tooltip);
            if (condition)
            {
                li.AddCssClass(classToAdd);
            }
            var a = new TagBuilder("a");
            a.AddCssClass("page-link");
            if (nextPage && !condition)
            {
                a.AddCssClass("pagination__next");
            }
            a.MergeAttribute("href", !condition ? action(index) : "javascript:");
            a.SetInnerText(linkText);
            li.InnerHtml = a.ToString();
            return li;
        }

        public static MvcHtmlString DisqusCommentCount(this HtmlHelper helper, string path)
        {
            HtmlTag a = new HtmlTag("a");
            a.Attr("data-disqus-identifier", path);

            return MvcHtmlString.Create(a.ToString());
        }

        public static MvcHtmlString DisqusCommentCountScript(this HtmlHelper helper, string disqusShortname)
        {
            HtmlTag script = new HtmlTag("script");
            script.Attr("type", "text/javascript");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var disqus_shortname = '" + disqusShortname + "';");

            sb.AppendLine("(function () {");
            sb.AppendLine("var s = document.createElement('script');");
            sb.AppendLine("s.async = true;");
            sb.AppendLine("s.src = 'http://' + disqus_shortname + '.disqus.com/count.js';");
            sb.AppendLine("(document.getElementsByTagName('HEAD')[0] || document.getElementsByTagName('BODY')[0]).appendChild(s);");
            sb.AppendLine("}");
            sb.AppendLine("());");

            script.AppendHtml(sb.ToString());

            return MvcHtmlString.Create(script.ToString());
        }

        public static MvcHtmlString FacebookCommentsThread(this HtmlHelper helper, string siteUrl, string path, string title)
        {
            var url = string.Format("{0}{1}", siteUrl, path);

            HtmlTag div = new HtmlTag("div");
            div.AddClass("fb-comments");
            div.Attr("data-href", url);
            div.Attr("data-numposts", "10");
            div.Attr("width", "100%");
            /*div.Attr("data-order-by", "social"); reverse_time /*/

            return MvcHtmlString.Create(div.ToString());
        }

        public static MvcHtmlString FacebookCommentCount(this HtmlHelper helper, string siteUrl, string path)
        {
            var url = string.Format("{0}{1}", siteUrl, path);

            HtmlTag span = new HtmlTag("span");
            span.AddClass("fb-comments-count");
            span.Attr("data-href", url);

            return MvcHtmlString.Create(span.ToString());
        }

        //ideally right after the opening<body> tag.
        public static MvcHtmlString FacebookCommentsScript(this HtmlHelper helper, string appid)
        {
            HtmlTag div = new HtmlTag("div");
            div.Id("fb-root");

            HtmlTag script = new HtmlTag("script");
            script.Attr("type", "text/javascript");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" (function(d, s, id) { ");

            sb.AppendLine(" var js, fjs = d.getElementsByTagName(s)[0]; ");
            sb.AppendLine(" if (d.getElementById(id)) return; ");
            sb.AppendLine(" js = d.createElement(s); js.id = id; ");
            sb.AppendLine(" js.src = '//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.9&appId="+ appid + "' ");
            sb.AppendLine(" fjs.parentNode.insertBefore(js, fjs); ");

            sb.AppendLine(" } ");
            sb.AppendLine(" (document, 'script', 'facebook-jssdk')); ");

            script.AppendHtml(sb.ToString());

            return MvcHtmlString.Create(div.ToString() + script.ToString());
        }

        public static MvcHtmlString GoogleAnalyticsScript(this HtmlHelper helper, string trackingId)
        {
            HtmlTag script = new HtmlTag("script");
            script.Attr("type", "text/javascript");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){ ");
            sb.AppendLine(" (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o), ");
            sb.AppendLine(" m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m) ");
            sb.AppendLine(" })(window,document,'script','https://www.google-analytics.com/analytics.js','ga'); ");
            sb.AppendLine(" ga('create', '" + trackingId + "', 'auto'); ");
            sb.AppendLine(" ga('send', 'pageview'); ");

            script.AppendHtml(sb.ToString());

            return MvcHtmlString.Create(script.ToString());
        }

        public static MvcHtmlString GoogleAdSenseScript(this HtmlHelper helper, string id)
        {
            HtmlTag script = new HtmlTag("script");
            script.Attr("type", "text/javascript");
            script.Attr("async", "");
            script.Attr("src", "//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js");

            HtmlTag script2 = new HtmlTag("script");
            script2.Attr("type", "text/javascript");

            StringBuilder sb2 = new StringBuilder();
            sb2.AppendLine(" (adsbygoogle = window.adsbygoogle || []).push({ ");
            sb2.AppendLine(" google_ad_client: '"+ id + "', ");
            sb2.AppendLine(" enable_page_level_ads: true ");
            sb2.AppendLine(" }); ");

            script2.AppendHtml(sb2.ToString());

            return MvcHtmlString.Create(script.ToString() + script2.ToString());
        }

        public static MvcHtmlString DisqusThread(this HtmlHelper helper, string disqusShortname, string siteUrl, string path, string title)
        {
            HtmlTag div = new HtmlTag("div");
            div.Id("disqus_thread");

            HtmlTag script = new HtmlTag("script");
            script.Attr("type", "text/javascript");

            var url = string.Format("{0}{1}", siteUrl, path);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var disqus_shortname = '" + disqusShortname + "';");

            sb.AppendLine("var disqus_config = function() {");
            sb.AppendLine("this.page.url = '" + url + "'");
            sb.AppendLine("this.page.identifier = '" + path + "'");
            sb.AppendLine("this.page.title = '" + title + "'");
            sb.AppendLine("};");

            sb.AppendLine("(function() {");
            sb.AppendLine("var dsq = document.createElement('script');");
            sb.AppendLine("dsq.type = 'text/javascript';");
            sb.AppendLine("dsq.async = false;");
            sb.AppendLine("dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';");
            sb.AppendLine("(document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);");
            sb.AppendLine("})();");

            script.AppendHtml(sb.ToString());

            HtmlTag noscript = new HtmlTag("noscript");
            noscript.AppendHtml("Please enable JavaScript to view the <a href=\"http://disqus.com/?ref_noscript\"> comments powered by Disqus.</a>");

            HtmlTag aDisqus = new HtmlTag("a");
            aDisqus.Attr("href", "http://disqus.com");
            aDisqus.AddClass("dsq-brlink");
            aDisqus.AppendHtml("blog comments powered by<span class=\"logo - disqus\">Disqus</span>");

            var finalHTML = div.ToString() + Environment.NewLine + script.ToString() + Environment.NewLine + noscript.ToString() + Environment.NewLine + aDisqus.ToString() + DisqusCommentCountScript(helper, disqusShortname).ToHtmlString();

            return MvcHtmlString.Create(finalHTML);
        }

        public static MvcHtmlString AddThisLinks(this HtmlHelper helper, string siteUrl, string path, string title, string description, string imageUrl)
        {
            var url = string.Format("{0}{1}", siteUrl, path);

            HtmlTag div = new HtmlTag("div");
            div.Attr("class", "addthis_inline_share_toolbox_p3ki");
            div.Attr("data-url", url);
            div.Attr("data-title", title);
            div.Attr("data-description", description);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                div.Attr("data-media", imageUrl);
            }

            return MvcHtmlString.Create(div.ToString());
        }

        public static MvcHtmlString AddThisRelatedPosts(this HtmlHelper helper)
        {

            HtmlTag div = new HtmlTag("div");
            div.Attr("class", "addthis_relatedposts_inline");

            return MvcHtmlString.Create(div.ToString());
        }

        public static MvcHtmlString ReturnToTop(this HtmlHelper helper)
        {
            HtmlTag link = new HtmlTag("a");
            link.Attr("href", "javascript:");
            link.Id("return-to-top");
            link.AppendHtml(@"<i class=""fa fa-chevron-up""></i>");

            return MvcHtmlString.Create(link.ToString());
        }

        public static MvcHtmlString AddThisScript(this HtmlHelper helper, string pubid)
        {
            HtmlTag script = new HtmlTag("script");
            script.Attr("src", "http://s7.addthis.com/js/300/addthis_widget.js#pubid=" + pubid);
            script.Attr("type", "text/javascript");

            return MvcHtmlString.Create(script.ToString());
        }

        public static MvcHtmlString GoogleFontAsync(this HtmlHelper helper, List<string> fonts, bool regular = true, bool bold = false, bool black = false, bool italic = false, bool boldItalic = false, Boolean hideTextWhileLoading = true, int timeout = 5000)
        {
            var html = Google.Font.GetFontScriptAsync(fonts, regular, bold, black, italic, boldItalic, hideTextWhileLoading, timeout);
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString GetFontBodyCSSAsync(this HtmlHelper helper, string font)
        {
            return MvcHtmlString.Create(Google.Font.GetFontBodyCSSAsync(font));
        }

        public static MvcHtmlString GetFontNavBarCSSAsync(this HtmlHelper helper, string font, string styleCSS)
        {
            return MvcHtmlString.Create(Google.Font.GetFontNavBarCSSAsync(font, styleCSS));
        }

        public static MvcHtmlString GoogleFont(this HtmlHelper helper, string font, string styleCSS, bool bodyFont, bool navBarFont, bool regular = true, bool bold = false, bool italic = false, bool boldItalic = false)
        {
            var html = Google.Font.GetFontLink(font, regular, bold, italic, boldItalic);
            if (bodyFont)
            {
                html += Environment.NewLine + Google.Font.GetFontBodyCSS(font);
            }

            if (navBarFont)
            {
                html += Environment.NewLine + Google.Font.GetFontNavBarCSS(font, styleCSS);
            }

            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString GoogleFontEffects(this HtmlHelper helper, string[] effects)
        {
            var html = Google.Font.GetEffectsLink(effects);
            return MvcHtmlString.Create(html);
        }

        public static MvcHtmlString Action<TController>(this HtmlHelper helper, Expression<Action<TController>> expression, Boolean passRouteValues = false) where TController : Controller
        {
            var controllerName = typeof(TController).Name;
            var routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(expression);
            if (passRouteValues)
            {
                return helper.Action(((MethodCallExpression)expression.Body).Method.Name, controllerName.Substring(0, controllerName.Length - "Controller".Length), routeValues);
            }
            else
            {
                return helper.Action(((MethodCallExpression)expression.Body).Method.Name, controllerName.Substring(0, controllerName.Length - "Controller".Length));
            }

        }

        public static HtmlString MenuLink<TController>(this HtmlHelper helper, Expression<Action<TController>> expression, string linkText, string classNames, string iconClass = "", string content = "") where TController : Controller
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"];
            var currentAction = routeData["action"];

            var controllerName = typeof(TController).Name;
            controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            var actionName = ((MethodCallExpression)expression.Body).Method.Name;

            if (String.Equals(actionName, currentAction as string,
                      StringComparison.OrdinalIgnoreCase)
                &&
               String.Equals(controllerName, currentController as string,
                       StringComparison.OrdinalIgnoreCase))

            {
                classNames = classNames + " active";

                HtmlString activeLink = null;

                if (!string.IsNullOrEmpty(iconClass))
                {
                    activeLink = new HtmlString(string.Format(@"<a href=""{0}"" class=""{1}"" title=""{2}""><i class=""fa {3}""></i></a>", helper.Url().Action<TController>(expression), classNames, linkText, iconClass));
                }
                else if (!string.IsNullOrEmpty(content))
                {
                    activeLink = new HtmlString(string.Format(@"<a href=""{0}"" class=""{1}"" title=""{2}"">{3}{2}</a>", helper.Url().Action<TController>(expression), classNames, linkText, content));
                }
                else
                {
                    activeLink = helper.ActionLink<TController>(expression, linkText,
                    new { @class = classNames, title = linkText }
                    );
                }

                return activeLink;

            }

            HtmlString link = null;

            if (!string.IsNullOrEmpty(iconClass))
            {
                link = new HtmlString(string.Format(@"<a href=""{0}"" class=""{1}"" title=""{2}""><i class=""fa {3}""></i></a>", helper.Url().Action<TController>(expression), classNames, linkText, iconClass));
            }
            else if (!string.IsNullOrEmpty(content))
            {
                link = new HtmlString(string.Format(@"<a href=""{0}"" class=""{1}"" title=""{2}"">{3}{2}</a>", helper.Url().Action<TController>(expression), classNames, linkText, content));
            }
            else
            {
                link = helper.ActionLink<TController>(expression, linkText,
                new { @class = classNames, title = linkText }
                );
            }

            return link;
        }

        public static HtmlString MenuLink(this HtmlHelper helper, string controllerName, string actionName, string linkText, string classNames)
        {
            if (controllerName != "")
            {
                var routeData = helper.ViewContext.RouteData.Values;
                var currentController = routeData["controller"];
                var currentAction = routeData["action"];

                if (String.Equals(actionName, currentAction as string,
                          StringComparison.OrdinalIgnoreCase)
                    &&
                   String.Equals(controllerName, currentController as string,
                           StringComparison.OrdinalIgnoreCase))

                {
                    classNames = classNames + " active";
                    var activeLink = helper.ActionLink(linkText, actionName, controllerName, new { },
                        new { @class = classNames, title = linkText, itemprop = "url" }
                        );
                    return activeLink;

                }

                var link = helper.ActionLink(linkText, actionName, controllerName, new { },
                    new { @class = classNames, title = linkText, itemprop = "url" }
                    );
                return link;
            }
            else
            {
                return new HtmlString(string.Format("<a href='{0}' itemprop='url' class='{1}'>{2}</a>", actionName, classNames, linkText));
            }
        }

        public static UrlHelper Url(this HtmlHelper html)
        {
            return new UrlHelper(html.ViewContext.RequestContext);
        }

        //https://daveaglick.com/posts/getting-an-htmlhelper-for-an-alternate-model-type
        public static HtmlHelper<TModel> For<TModel>(this HtmlHelper helper) where TModel : class, new()
        {
            return For<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection);
        }

        public static HtmlHelper<dynamic> For(this HtmlHelper helper, dynamic model)
        {
            return For(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection, model);
        }

        public static HtmlHelper<TModel> For<TModel>(this HtmlHelper helper, TModel model)
        {
            return For<TModel>(helper.ViewContext, helper.ViewDataContainer.ViewData, helper.RouteCollection, model);
        }

        public static HtmlHelper<TModel> For<TModel>(ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection) where TModel : class, new()
        {
            TModel model = new TModel();
            return For<TModel>(viewContext, viewData, routeCollection, model);
        }

        public static HtmlHelper<TModel> For<TModel>(ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection, TModel model)
        {
            var newViewData = new ViewDataDictionary(viewData) { Model = model };
            ViewContext newViewContext = new ViewContext(
                viewContext.Controller.ControllerContext,
                viewContext.View,
                newViewData,
                viewContext.TempData,
                viewContext.Writer);
            var viewDataContainer = new ViewDataContainer(newViewContext.ViewData);
            return new HtmlHelper<TModel>(newViewContext, viewDataContainer, routeCollection);
        }

        public static HtmlHelper<dynamic> For(ViewContext viewContext, ViewDataDictionary viewData, RouteCollection routeCollection, dynamic model)
        {
            var newViewData = new ViewDataDictionary(viewData) { Model = model };
            ViewContext newViewContext = new ViewContext(
                viewContext.Controller.ControllerContext,
                viewContext.View,
                newViewData,
                viewContext.TempData,
                viewContext.Writer);
            var viewDataContainer = new ViewDataContainer(newViewContext.ViewData);
            return new HtmlHelper<dynamic>(newViewContext, viewDataContainer, routeCollection);
        }

        private class ViewDataContainer : System.Web.Mvc.IViewDataContainer
        {
            public System.Web.Mvc.ViewDataDictionary ViewData { get; set; }

            public ViewDataContainer(System.Web.Mvc.ViewDataDictionary viewData)
            {
                ViewData = viewData;
            }
        }

        public static HtmlString ActionLink<TController>(this HtmlHelper html, Expression<Action<TController>> expression, string linkText, object htmlAttributes, Boolean passRouteValues = true) where TController : Controller
        {
            RouteValueDictionary routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(expression);
            IDictionary<string, object> htmlAttributesDict = (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes);

            if (passRouteValues)
            {
                return html.ActionLink(linkText, routeValues["Action"].ToString(), routeValues["Controller"].ToString(), routeValues, htmlAttributesDict);
            }
            else
            {
                return html.ActionLink(linkText, routeValues["Action"].ToString(), routeValues["Controller"].ToString(), new RouteValueDictionary(), htmlAttributesDict);
            }          
        }

    }
}
