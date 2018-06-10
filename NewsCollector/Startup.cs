using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using NewsCollector.Models;
using Owin;
using System;
using System.Reflection;

[assembly: OwinStartupAttribute(typeof(NewsCollector.Startup))]
namespace NewsCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            log4net.Config.XmlConfigurator.Configure();

            ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            logger.Info("App is running");

            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";

                string userPWD = "12QWas_+";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }

                AddNewArticle(user);
            }

            // creating Creating Redactor role    
            if (!roleManager.RoleExists("Redactor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Redactor";
                roleManager.Create(role);

                //Here we create a Redactor user
                var user = new ApplicationUser();
                user.UserName = "redactor@gmail.com";
                user.Email = "redactor@gmail.com";

                string userPWD = "12QWas_+";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Redactor
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Redactor");
                }

                AddNewArticle(user);
                AddNewArticle(user);
                AddNewArticle(user);
                AddNewArticle(user);
            }

            // creating Creating Regular role    
            if (!roleManager.RoleExists("Regular"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Regular";
                roleManager.Create(role);

                //Here we create a Regular user
                var user = new ApplicationUser();
                user.UserName = "regular@gmail.com";
                user.Email = "regular@gmail.com";

                string userPWD = "12QWas_+";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Regular   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Regular");

                }
            }

            // creating Creating Subscriber role    
            if (!roleManager.RoleExists("Subscriber"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Subscriber";
                roleManager.Create(role);

                //Here we create a Subscriber user
                var user = new ApplicationUser();
                user.UserName = "sub@gmail.com";
                user.Email = "sub@gmail.com";

                string userPWD = "12QWas_+";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Subscriber   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Subscriber");

                }
            }
        }
        public void AddNewArticle(ApplicationUser user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var art = new ArticleModel();
                art.Id = Guid.NewGuid();
                art.AuthorId = user.Id;
                art.Author = user;
                art.AdditionDate = DateTime.UtcNow;
                art.Title = "Lorem ipsum dolor sit amet, est constituto consequuntur te, wisi sed.";
                art.LeadingParagraph = "Ne quis inermis perpetua duo, vel ex noster habemus intellegat. Ut quo libris ceteros scriptorem, vis ex minim commune offendit, vim ex propriae omnesque percipit. Sea homero ornatus cu. Perpetua maiestatis definitiones eu quo.";
                art.Body = "Id illud aeque quodsi qui, probo definitiones conclusionemque te has, eu has invidunt inimicus delicatissimi. Sea et vero idque verterem, ne vis vitae doming semper. Et porro volumus salutatus usu. Duis sonet adolescens te pri, case complectitur ius an. Eu nam agam tantas mucius. Est eius suscipiantur eu, eos ad cibo elaboraret, te explicari consectetuer comprehensam has." +
                 " Ut per porro doctus eligendi. Cu fuisset suavitate ius, ne cum purto justo nostro. Vix duis fabulas intellegat ad, ex his tale epicuri voluptatibus.Usu in meis dictas. Mel habemus molestie id, quando appetere mea ut. Mei offendit facilisi mandamus te, diam fuisset ne mea, nec at ludus homero recteque.Vis sonet referrentur id, ius aliquip minimum ex, vis ad stet menandri. Brute moderatius consectetuer nec ea, solet cetero option quo ex.Eam ei veniam tamquam. Nihil ornatus qui ex, eu quis movet mollis sit. Eam cu saepe consulatu." +

                 "No duis exerci quaestio eam, hinc iusto omittantur id eam. Ei vix prompta commune, decore habemus expetendis no cum. In alia dolores mea. Eam cu inani virtute. Eos legere nostrum ea, ea qui delenit facilisi." +

               " Elitr lucilius voluptatum vim an, ne his aliquando vulputate.Pro cu explicari gloriatur, et pro inani nostro, ludus soluta dolorum ex pro.Tota solum laboramus cu vel, an ius utamur viderer, omnes placerat qualisque vel no. Ex mei tation veniam pertinax, saepe epicurei oportere his id.Erant soleat noluisse ad pro, nam no quidam accusamus consequat." +

                "Ut velit possit probatus sea. An suscipit delicatissimi his. Ne assum necessitatibus sed, pro no persius expetenda constituto. Eu qui oblique equidem, nibh impetus oblique ne sed. Vix et sint illud falli, eruditi deleniti at vel." +

               "Id pro nulla tamquam labitur. Cu vel quis dicat, ut viderer malorum vix.Has ubique dissentiet definitionem cu.Iusto reformidans an quo, quando concludaturque duo in, percipit omittantur nec no." +

                "Ex duo paulo consul persecuti, labores albucius convenire an his, dicunt verterem eu eam.Rebum utroque posidonium ea sit, has te meis harum numquam.Nostro facilis has ei. Eos et putant explicari. Vim malis oratio ornatus ex, eirmod diceret constituam ei sit." +

                "Te usu utamur latine, his error malorum an. Ius dicta delicata cu, at omnes salutandi cum, te dicit virtute definitiones eos.Ignota erroribus quo ei, pri case sapientem cu, duis ridens per te.Sed saepe postea ceteros an, mea ei modus graece, ei has stet petentium.Affert delenit corrumpit mea ea." +

                "Movet placerat cu usu, nostrud delicatissimi an pri. Ex sonet nonumy per, ne lorem soluta alienum est. Oratio philosophia duo ei, ludus oporteat ius an, vix eu primis fuisset pertinax.Id nostrum antiopam vis, qui dico minim deseruisse ne.";
                db.articles.Add(art);
                db.SaveChanges();
            }
        }
    }
}
