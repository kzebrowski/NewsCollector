using NewsCollector.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsCollector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext newsContext = new ApplicationDbContext();
            List<ArticleModel> articles = newsContext.articles.OrderByDescending(x => x.Id).ToList();
            return View(articles);
        }

        public ActionResult ArticleRead(Guid id)
        {
            ApplicationDbContext newsContext = new ApplicationDbContext();
            ArticleModel article = newsContext.articles.Single(ar => ar.Id == id);

            return View(article);
        }
        public ActionResult ExportPdf(Guid id)
        {
            var q = new ActionAsPdf("ArticleRead", new { id = id });
            return q;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Article()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
    }
}