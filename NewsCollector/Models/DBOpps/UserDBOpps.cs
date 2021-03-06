﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NewsCollector.Models.DBOpps
{
    public class UserDBOpps
    {
        private ApplicationDbContext Clients; /*!< Connection with database */

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); /*!< Logger from log4net. */
                                                                                                                 //!< Mainly for logging error's durning work with DB.

        //! Returns user that has an attribute equal to criteria.
        public IList<ApplicationUser> GetClients(string colName /**< Name of attribute */, string criteria /**< Value of attribute */)
        {
            List<ApplicationUser> result = new List<ApplicationUser>();

            using (Clients = ApplicationDbContext.Create())
            {
                foreach (ApplicationUser c in this.Clients.Users)
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
        public IList<ApplicationUser> GetAllClients()
        {
            using (Clients = ApplicationDbContext.Create())
            {
                try
                {
                    return Clients.Users.ToList();
                }
                catch (Exception e)
                {
                    logger.Error("GetAllClients() - cannot retrieve all clients from Database, exception:" + e.ToString());
                    return null;
                }
            }
        }


        //! Add's a Client to DB.
        public async Task AddClient(ApplicationUser user)
        {

            using (Clients = ApplicationDbContext.Create())
            {
                try
                {
                    Clients.Users.Add(user);
                    await Clients.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    logger.Error("AddClient() - cannot add client to Database, exception:" + e.ToString());
                }
            }
        }

        //! Modifies a user that has the same id as arugment.
        public async Task ModifiyClient(ApplicationUser user)
        {
            using (Clients = ApplicationDbContext.Create())
            {
                var original = Clients.Users.Find(user.Id);
                if (original != null)
                {
                    Clients.Entry(original).CurrentValues.SetValues(user);
                    await Clients.SaveChangesAsync();
                }
                else
                {
                    logger.Error("ModifiyClient() - cannot modify non-existing client in Database");
                }
            }
        }

        //! Remove'a user with set id.
        public async Task RemoveClient(string id)
        {
            using (Clients = ApplicationDbContext.Create())
            {
                var result = Clients.Users.Find(id);
                if (result != null)
                {
                    Clients.Users.Remove(result);
                    await Clients.SaveChangesAsync();
                }
                else
                {
                    logger.Error("RemoveClient() - cannot remove non-existing in Database");
                }
            }
        }
    }
}
