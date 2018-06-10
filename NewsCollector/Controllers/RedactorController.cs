using NewsCollector.Models;
using NewsCollector.Models.DBOpps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace NewsCollector.Controllers
{
    [Authorize]
    public class RedactorController : Controller
    {
        private readonly ArticleDBOpps _articleDBOpps = new ArticleDBOpps();

        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        public ActionResult ArticleEditor()
        {
            return View();
        }

        public ActionResult MyArticles()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var userIdClaim = claimsIdentity.Claims
                 .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userIdValue = userIdClaim.Value;


            var articles = _articleDBOpps.GetArticlesForRedactor(userIdValue);
            List<ArticleBrowsingViewModel> model = new List<ArticleBrowsingViewModel>();

            foreach(var article in articles)
            {
                model.Add(new ArticleBrowsingViewModel { Id = article.Id, Title = article.Title });
            }

            return View(model);
        }

        public ActionResult EditArticle(string id)
        {
            ArticleModel article = _articleDBOpps.GetArticles("Id", id).First();
            ModifyArticleViewModel model = new ModifyArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                LeadParagraph = article.LeadingParagraph,
                Content = article.Body
            };

            return View(model);
        }
    }
}