using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Emp(string gender="All")
        {
            using (AdventureWorksEntities ae = new AdventureWorksEntities())
            {

                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, ae.Employees.ToList());
                    case "m":
                        return Request.CreateResponse(HttpStatusCode.OK, ae.Employees.Where(a => a.Gender.ToLower() == gender.ToLower()).ToList());
                    case "f":
                        return Request.CreateResponse(HttpStatusCode.OK, ae.Employees.Where(a => a.Gender.ToLower() == gender.ToLower()).ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,gender+ " is invalid arguent .Pass Valid request among ALL ,M and F");
                }
                //var Emp =ae.Employees.ToList();

                //return Emp;
            }
        }

        public string Post([FromBody]Employee employee)
        {
            AdventureWorksEntities aw = new AdventureWorksEntities();
            aw.Employees.Add(employee);
            aw.SaveChanges();
            return "Added Successfully";
        }


    }
}
