﻿using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using NewsCollector.Models;
using NewsCollector.Models.DBOpps;
using System.Threading.Tasks;
using Rotativa;

namespace NewsCollector.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleDBOpps _articleDBOpps = new ArticleDBOpps();

        public ActionResult Article(Guid id)
        {
            ArticleModel article =_articleDBOpps.GetArticles("Id", id.ToString()).First();
            
            if (article == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ArticleViewModel model = new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                LeadParagraph = article.LeadingParagraph,
                Content = article.Body,
                AdditionDate = article.AdditionDate,
                Image = article.Image
            };
            
            if (!User.Identity.IsAuthenticated || 
                ((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).FirstOrDefault() == "Regular")
            {
                model.Content = article.Body.Substring(0, 400) + "...";
                ViewBag.Message = "( ͡° ͜ʖ ͡° )つ──☆*:・ﾟAby zobaczyć pełną wersję artykułu wykup subskrybcję.";
            }
            else
            {
                model.Content = article.Body;
                ViewBag.Message = "";
            }
            
            return View(model);
        }

        public ActionResult ExportPdf(Guid id)
        {
            ArticleModel article = _articleDBOpps.GetArticles("Id", id.ToString()).First();

            if (article == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            ArticleViewModel model = new ArticleViewModel
            {
                Title = article.Title,
                LeadParagraph = article.LeadingParagraph
            };

            if (!User.Identity.IsAuthenticated ||
                ((ClaimsIdentity)User.Identity).Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).FirstOrDefault() == "Regular")
            {
                model.Content = article.Body.Substring(0, 400) + "...";
                ViewBag.Message = "( ͡° ͜ʖ ͡° )つ──☆*:・ﾟAby zobaczyć pełną wersję artykułu wykup subskrybcję.";
            }
            else
            {
                model.Content = article.Body;
                ViewBag.Message = "";
            }

            return new ViewAsPdf("Article",model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle(CreateArticleViewModel article)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            
            var userIdClaim = claimsIdentity.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userIdValue = userIdClaim.Value;

            ArticleModel articleModel = new ArticleModel
            {
                Id = Guid.NewGuid(),
                AuthorId = userIdValue,
                Title = article.Title,
                LeadingParagraph = article.LeadParagraph,
                Body = article.Content,
                AdditionDate = DateTime.Now,
                Image = article.Image
            };

            await _articleDBOpps.AddArticle(articleModel);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            await _articleDBOpps.RemoveArticle(new Guid(id));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Update(ModifyArticleViewModel article)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            var userIdClaim = claimsIdentity.Claims
                 .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userIdValue = userIdClaim.Value;

            ArticleModel modified = new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Body = article.Content,
                LeadingParagraph = article.LeadParagraph,
                AuthorId = userIdValue,
                AdditionDate = DateTime.Now,
                Image = article.Image
            };

            await _articleDBOpps.ModifiyArticle(modified);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}