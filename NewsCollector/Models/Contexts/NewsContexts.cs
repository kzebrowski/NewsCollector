using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewsCollector.Models.Contexts
{
    /*! Class representing connection with data base. Dbset properties are DB tables. */
    public class NewsContexts : DbContext
    {
        public DbSet<UserModel> users { get; set; }
        public DbSet<ArticleModel> articles { get; set; }
    }
}