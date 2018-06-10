using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using NewsCollector.Models;

using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using NewsCollector.Models.DBOpps;

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
                Title = article.Title,
                LeadParagraph = article.LeadingParagraph,
                Content = article.Body
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateArticle(CreateArticleViewModel article)
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
                Body = article.Content
            };

            _articleDBOpps.AddArticle(articleModel);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpPost]
        public ActionResult Delete(string id)
        {
            _articleDBOpps.RemoveArticle(new Guid(id));

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Update(ModifyArticleViewModel article)
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
                AuthorId = userIdValue
            };

            _articleDBOpps.ModifiyArticle(modified);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}