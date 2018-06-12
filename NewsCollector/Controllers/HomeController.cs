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
            List<ArticleViewModel> model = articles
                .Select(a => new ArticleViewModel {
                    Id = a.Id ,
                    Title = a.Title,
                    Content = a.Body,
                    LeadParagraph = a.LeadingParagraph,
                    Image = a.Image
                })
                .ToList();      

            return View(model);
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