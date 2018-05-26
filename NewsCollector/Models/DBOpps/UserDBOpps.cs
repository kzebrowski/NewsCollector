using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsCollector.Models.Contexts;
using log4net;
using System.Reflection;

namespace NewsCollector.Models.DBOpps
{
    public class UserDBOpps
    {
        private NewsContext Clients; /*!< Connection with database */

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); /*!< Logger from log4net. */
                                                                                                                 //!< Mainly for logging error's durning work with DB.

        //! Returns user that has an attribute equal to criteria.
        IList<UserModel> GetClients(string colName /**< Name of attribute */, string criteria /**< Value of attribute */)
        {
            List<UserModel> result = new List<UserModel>();

            using (Clients = new NewsContext())
            {
                foreach (UserModel c in this.Clients.users)
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
                        logger.Error("GetClients() - cannot retrieve client from Database, exception:" + e.ToString());
                    }
                }

                return result;
            }
        }

        //! Returns all clients as a list.
        IList<UserModel> GetAllClients()
        {
            using (Clients = new NewsContext())
            {
                try
                {
                    return Clients.users.ToList();
                }
                catch (Exception e)
                {
                    logger.Error("GetAllClients() - cannot retrieve all clients from Database, exception:" + e.ToString());
                    return null;
                }
            }
        }


        //! Add's a Client to DB.
        void AddClient(UserModel user)
        {

            using (Clients = new NewsContext())
            {
                try
                {
                    Clients.users.Add(user);
                    Clients.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.Error("AddClient() - cannot add client to Database, exception:" + e.ToString());
                }
            }
        }

        //! Modifies a user that has the same id as arugment.
        void ModifiyClient(UserModel user)
        {
            using (Clients = new NewsContext())
            {
                var original = Clients.users.Find(user.Id);
                if (original != null)
                {
                    Clients.Entry(original).CurrentValues.SetValues(user);
                    Clients.SaveChanges();
                }
                else
                {
                    logger.Error("ModifiyClient() - cannot modify non-existing client in Database");
                }
            }
        }

        //! Remove'a user with set id.
        void RemoveClient(int id)
        {
            using (Clients = new NewsContext())
            {
                var result = Clients.users.Find(id);
                if (result != null)
                {
                    Clients.users.Remove(result);
                    Clients.SaveChanges();
                }
                else
                {
                    logger.Error("RemoveClient() - cannot remove non-existing in Database");
                }
            }
        }
    }
}
