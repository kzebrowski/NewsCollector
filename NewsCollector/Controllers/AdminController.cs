using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsCollector.Models;
using NewsCollector.Models.DBOpps;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace NewsCollector.Controllers
{
    public class AdminController : Controller
    {

        ApplicationDbContext context;
        UserDBOpps userDBOpps;
        ArticleDBOpps articleDBOpps;

        public AdminController()
        {
            context = new ApplicationDbContext();
            userDBOpps = new UserDBOpps();
            articleDBOpps = new ArticleDBOpps();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            await userDBOpps.RemoveClient(id);
            
            return Json("DONE");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteArticle(Guid id)
        {
            await articleDBOpps.RemoveArticle(id);

            return Json("DONE");
        }

        [HttpPost]
        public ActionResult ChangeRole(string id, string newRole)
        {
            var users = GetUsersData().ToList();
            
            string currRoleName = users.Find(u => u.UserId.Equals(id)).Role;

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            UserManager.RemoveFromRole(id, currRoleName);
            UserManager.AddToRole(id, newRole);
            
            return Json("DONE");
        }

        private IEnumerable<UserViewModel> GetUsersData()
        {   
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserViewModel()
                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            return usersWithRoles;
        }

        // GET: Admin/ManageUsers
        public ActionResult ManageUsers()
        {
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserViewModel()
                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }

        // GET: Admin/ManageArticles
        public ActionResult ManageArticles()
        {
            var articles = articleDBOpps.GetAllArticles();
            var users = userDBOpps.GetAllClients();

            var articlesWithAuthors = (from article in articles
                                      join user in users on article.AuthorId equals user.Id
                                      select new ArticleModel {
                                          Id = article.Id,
                                          Author = user,
                                          AuthorId = user.Id,
                                          Title = article.Title,
                                      }).ToList();

            return View(articlesWithAuthors);
        }

        // GET: Admin/EditArticle
        public ActionResult EditArticle(string id)
        {
            ArticleModel article = articleDBOpps.GetArticles("Id", id).First();
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