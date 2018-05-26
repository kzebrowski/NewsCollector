﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsCollector.Models.Contexts;
using log4net;
using System.Reflection;

namespace NewsCollector.Models.DBOpps
{
    public class ArticleDBOpps
    {
        private NewsContext Articles; /*!< Connection with database */

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); /*!< Logger from log4net. */
                                                                                                                 //!< Mainly for logging error's durning work with DB.

        //! Returns article that has an attribute equal to criteria.
        IList<ArticleModel> GetArticles(string colName /**< Name of attribute */, string criteria /**< Value of attribute */)
        {
            List<ArticleModel> result = new List<ArticleModel>();

            using (Articles = new NewsContext())
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
        IList<ArticleModel> GetAllArticles()
        {
            using (Articles = new NewsContext())
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

        //! Add's an Article to DB.
        void AddArticle(ArticleModel article)
        {
            using (Articles = new NewsContext())
            {
                try
                {
                    Articles.articles.Add(article);
                    Articles.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("AddArticle() - cannot add client to Database, exception:" + e.ToString());
                }
            }
        }

        //! Modifies a user that has the same id as arugment.
        void ModifiyArticle(ArticleModel article)
        {
            using (Articles = new NewsContext())
            {
                var original = Articles.users.Find(article.Id);
                if (original != null)
                {
                    Articles.Entry(original).CurrentValues.SetValues(article);
                    Articles.SaveChanges();
                }
                else
                {
                    logger.Error("ModifiyArticle() - cannot modify non-existing client in Database");
                }
            }
        }

        //! Remove'a user with set id.
        void RemoveArticle(int id)
        {
            using (Articles = new NewsContext())
            {
                var result = Articles.articles.Find(id);
                if (result != null)
                {
                    Articles.articles.Remove(result);
                    Articles.SaveChanges();
                }
                else
                {
                    logger.Error("RemoveArticle() - cannot remove non-existing in Database");
                }
            }
        }
    }
}