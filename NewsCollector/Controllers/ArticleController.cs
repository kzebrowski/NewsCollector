using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsCollector.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using NewsCollector.Models.DBOpps;

namespace NewsCollector.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleDBOpps _articleDBOpps = new ArticleDBOpps();

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
            ArticleModel articleModel = new ArticleModel
            {
                Id = Guid.NewGuid(),
                AuthorId = 1,
                Title = article.Title,
                LeadingParagraph = article.LeadParagraph,
                Body = article.Content
            };

            _articleDBOpps.AddArticle(articleModel);

            return Json("success");
        }
    }
}