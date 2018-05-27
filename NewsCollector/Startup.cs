using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using NewsCollector.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsCollector.Startup))]
namespace NewsCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
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
    }
}
