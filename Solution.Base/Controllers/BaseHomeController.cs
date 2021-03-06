﻿using Solution.Base.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Solution.Base.Implementation.Extensions;
using Solution.Base.Interfaces.Validation;
using Solution.Base.Implementation.Validation;
using AutoMapper;
using System.Net;
using Solution.Base.Interfaces.Logging;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using System.Threading;
using Solution.Base.Alerts;
using System.Xml.Linq;
using System.Globalization;
using Solution.Base.Extensions;
using Solution.Base.Helpers;
using System.ServiceModel.Syndication;
using Solution.Base.Implementation.Models;
using Solution.Base.Email;

namespace Solution.Base.Controllers
{
    public abstract class BaseHomeController : BaseController
    {
        public BaseHomeController()
        {

        }

        public BaseHomeController(IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
            : base(mapper, logFactory, emailService)
        {

        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("contact")]
        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [Route("contact")]
        public ViewResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var message = new EmailMessage();
                    message.Body = contact.Body;
                    message.IsHtml = true;
                    message.Subject = contact.Subject;
                    message.ReplyDisplayName = contact.Name;
                    message.ReplyEmail = contact.Email;

                    EmailService.SendEmailMessageToAdmin(message);

                    return View("Thanks");
                }
                catch (Exception ex)
                {
                    HandleUpdateException(ex);
                }
            }

            if (contact.Body ==null)
            {
                ViewData.ModelState.Clear();
            }

            return View(contact);
        }

        //<add name = "SitemapXml" path="sitemap.xml" verb="GET" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
        [Route("sitemap.xml")]
        public async Task<ActionResult> SitemapXml()
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            var sitemapNodes = await GetSitemapNodes(cts.Token);
            string xml = GetSitemapDocument(sitemapNodes);
            return this.Content(xml, "text/xml", Encoding.UTF8);
        }

        //0.8-1.0: Homepage, subdomains, product info, major features
        //0.4-0.7: Articles and blog entries, category pages, FAQs
        protected async virtual Task<IList<SitemapNode>> GetSitemapNodes(CancellationToken cancellationToken)
        {
            var siteUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];

            List<SitemapNode> nodes = new List<SitemapNode>();

            nodes.Add(
                new SitemapNode()
                {
                    Url = siteUrl,
                    Priority = 1
                });

            foreach (dynamic menuItem in Url.NavigationMenu().Menu)
            {
                nodes.Add(
                  new SitemapNode()
                  {
                      Url = Url.AbsoluteUrl((string)menuItem.Action, (string)menuItem.Controller, null),
                      Priority = 0.9
                  });
            }

            return nodes;
        }

        [Route("feed")]
        public async Task<ActionResult> Feed()
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            // Create a collection of SyndicationItemobjects from the latest posts
            var rssItems = await RSSItems(cts.Token);

            // Create an instance of SyndicationFeed class passing the SyndicationItem collection
            var feed = new SyndicationFeed(SiteTitle, SiteDescription, new Uri(SiteUrl), rssItems)
            {
                Copyright = new TextSyndicationContent(String.Format("Copyright © {0}", SiteTitle)),
                Language = Thread.CurrentThread.CurrentUICulture.Name
            };

            // Format feed in RSS format through Rss20FeedFormatter formatter
            var feedFormatter = new Rss20FeedFormatter(feed);

            // Call the custom action that write the feed to the response
            return new RSSActionResult(feedFormatter);
        }

        protected abstract Task<IEnumerable<SyndicationItem>> RSSItems(CancellationToken cancellationToken);
        protected abstract string SiteTitle { get; }
        protected abstract string SiteDescription { get; }
        protected abstract string SiteUrl { get; }

        protected string GetSitemapDocument(IEnumerable<SitemapNode> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (SitemapNode sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode.Url)),
                    sitemapNode.LastModified == null ? null : new XElement(
                        xmlns + "lastmod",
                        sitemapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    sitemapNode.Frequency == null ? null : new XElement(
                        xmlns + "changefreq",
                        sitemapNode.Frequency.Value.ToString().ToLowerInvariant()),
                    sitemapNode.Priority == null ? null : new XElement(
                        xmlns + "priority",
                        sitemapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }

        protected class SitemapNode
        {
            public SitemapFrequency? Frequency { get; set; }
            public DateTime? LastModified { get; set; }
            public double? Priority { get; set; }
            public string Url { get; set; }
        }

        protected enum SitemapFrequency
        {
            Never,
            Yearly,
            Monthly,
            Weekly,
            Daily,
            Hourly,
            Always
        }
    }
}
