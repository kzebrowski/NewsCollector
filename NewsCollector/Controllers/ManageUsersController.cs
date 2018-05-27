﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsCollector.Models;
using NewsCollector.Models.DBOpps;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace NewsCollector.Controllers
{
    public class ManageUsersController : Controller
    {

        ApplicationDbContext context;
        UserDBOpps userDBOpps;
        
        public ManageUsersController()
        {
            context = new ApplicationDbContext();
            userDBOpps = new UserDBOpps();
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            userDBOpps.RemoveClient(id);
            
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

        // GET: ManageUsers
        public ActionResult Index()
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
    }
}