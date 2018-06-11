using NewsCollector.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NewsCollector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext newsContext = new ApplicationDbContext();
            List<ArticleModel> articles = newsContext.articles.OrderByDescending(x => x.AdditionDate).ToList();
            List<ArticleViewModel> model;

           
            model = articles.Select(a => new ArticleViewModel { Id = a.Id, Title = a.Title, Content = a.Body.Length>=400? a.Body.Substring(0,400)+"...": a.Body, LeadParagraph = a.LeadingParagraph }).ToList();

            return View(model);
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

        public ActionResult SignIn()
        {
            return View();
        }
    }
}