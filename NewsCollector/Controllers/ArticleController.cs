using System;
using System.Web.Mvc;
using NewsCollector.Models;

namespace NewsCollector.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult ArticleEditor()
        {
            return View();
        }

        public ActionResult Article()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateArticle(ArticleViewModel article)
        {
            ArticleModel acticleModel = new ArticleModel
            {
                Id = Guid.NewGuid(),
                AuthorId = 1,
                Title = article.Title,
                LeadingParagraph = article.LeadParagraph,
                Body = article.Content
            };

            return Json("success");
        }
    }
}