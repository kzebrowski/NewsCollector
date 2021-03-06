﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;
using System.Threading.Tasks;

namespace NewsCollector.Models.DBOpps
{
    public class ArticleDBOpps
    {
        private ApplicationDbContext Articles; /*!< Connection with database */

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); /*!< Logger from log4net. */
                                                                                                                 //!< Mainly for logging error's durning work with DB.

        //! Returns article that has an attribute equal to criteria.
        public IList<ArticleModel> GetArticles(string colName /**< Name of attribute */, string criteria /**< Value of attribute */)
        {
            List<ArticleModel> result = new List<ArticleModel>();

            using (Articles = ApplicationDbContext.Create())
            {
                foreach (ArticleModel c in this.Articles.articles)
                {
                    try
                    {
                        var value = c.GetType().GetProperty(colName).GetValue(c);

                        if (value.ToString().Equals(criteria))
                        {
                            result.Add(c);
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error("GetArticles() - cannot retrieve client from Database, exception:" + e.ToString());
                    }
                }

                return result;
            }
        }

        //! Returns all articles as a list.
        public IList<ArticleModel> GetAllArticles()
        {
            using (Articles = ApplicationDbContext.Create())
            {
                try
                {
                    return Articles.articles.ToList();
                }
                catch (Exception e)
                {
                    logger.Error("GetAllArticles() - cannot retrieve all clients from Database, exception:" + e.ToString());
                    return null;
                }
            }
        }

        //! Add's an article to DB.
        public async Task AddArticle(ArticleModel article)
        {
            using (Articles = ApplicationDbContext.Create())
            {
                try
                {
                    article.AdditionDate = DateTime.Now;
                    Articles.articles.Add(article);
                    await Articles.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    logger.Error("AddArticle() - cannot add client to Database, exception:" + e.ToString());
                }
            }
        }

        //! Modifies an article that has the same id as arugment.
        public async Task ModifiyArticle(ArticleModel article)
        {
            using (Articles = ApplicationDbContext.Create())
            {
                var original = Articles.articles.Find(article.Id);
                if (original != null)
                {
                    Articles.Entry(original).CurrentValues.SetValues(article);
                    await Articles.SaveChangesAsync();
                }
                else
                {
                    logger.Error("ModifiyArticle() - cannot modify non-existing client in Database");
                }
            }
        }

        //! Remove an article with set id.
        public async Task RemoveArticle(Guid id)
        {
            using (Articles = ApplicationDbContext.Create())
            {
                var result = Articles.articles.Find(id);
                if (result != null)
                {
                    Articles.articles.Remove(result);
                    await Articles.SaveChangesAsync();
                }
                else
                {
                    logger.Error("RemoveArticle() - cannot remove non-existing in Database");
                }
            }
        }

        public IList<ArticleModel> GetArticlesForRedactor(string redacorId)
        {
            using (Articles = ApplicationDbContext.Create())
            {
                var result = this.Articles.articles.Where(a => a.AuthorId == redacorId).ToList();

                return result;
            }
        }
    }
}