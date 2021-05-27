using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        Sample_UserEntities ue = new Sample_UserEntities();

        public IEnumerable<UserTable> Get()
        {
            var users = ue.UserTables.ToList();
            return users;
        }

        public IEnumerable<UserTable> Get( int id)
        {
            var user = ue.UserTables.Where(u => u.UserID == id).ToList();
            return user;
        }

        public HttpResponseMessage Post([FromBody]UserTable userTable)
        {
            try
            {
                ue.UserTables.Add(userTable);
                ue.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, "The Record has been Created suessfully");
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed to create the Record");
            }            
        }

        public HttpResponseMessage Put(int id,[FromBody] UserTable userTable)
        {
            try
            {
                if (id == userTable.UserID)
                {
                    ue.Entry(userTable).State = System.Data.Entity.EntityState.Modified;
                    //ue.UserTables.Add(userTable);
                    ue.SaveChanges();
                    HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
                    return message;
                }
                else
                {
                    HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return message;
                }
            }
            catch (Exception ex)
            {

                HttpResponseMessage  message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return message;
            }               
        }

        public HttpResponseMessage Delete(int id)
        {
            var user = ue.UserTables.Find(id);
            if (user != null)
            {
                ue.UserTables.Remove(user);
                ue.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"Not Deleted");
            }
        }
    }
}
