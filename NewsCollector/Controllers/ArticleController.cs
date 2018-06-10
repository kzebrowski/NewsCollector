using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using NewsCollector.Models;
using NewsCollector.Models.DBOpps;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace NewsCollector.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleDBOpps _articleDBOpps = new ArticleDBOpps();

        public ActionResult Article(Guid id)
        {
            ArticleModel article =_articleDBOpps.GetArticles("Id", id.ToString()).First();
            CreateArticleViewModel model = new CreateArticleViewModel
            {
                Title = article.Title,
                LeadParagraph = article.LeadingParagraph,
                Content = article.Body,
                //Image = byteArrayToImage(article.Image)
            };
            
            return View(model);
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
                //Image = imageToByteArray(Image.FromFile(article.ImagePath))
            };

            await _articleDBOpps.AddArticle(articleModel);

            return Json("success");
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            await _articleDBOpps.RemoveArticle(new Guid(id));

            return Json("done");
        }

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
                //Image = imageToByteArray(Image.FromFile(article.ImagePath))
            };

            await _articleDBOpps.ModifiyArticle(modified);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        //! Use the following methods to save and retrieve images.
        //! To save an image use the first method, to retrieve use the second one.

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}