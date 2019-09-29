using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MitraisCodingTest.Models;

namespace MitraisCodingTest.Controllers
{
    public class RegistrationController : ApiController
    {
        public IEnumerable<Registration> GetRegistration()
        {
            using (MitraisCodingTestEntities db = new MitraisCodingTestEntities())
            {
                return db.Registrations.ToList();
            }
        }
        public HttpResponseMessage GetRegistrationById(int id)
        {
            using (MitraisCodingTestEntities db = new MitraisCodingTestEntities())
            {
                var entity = db.Registrations.FirstOrDefault(e => e.RegistrationId == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Registration with id" + id + "Not Found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Registration registration)
        {
            try
            {
                using (MitraisCodingTestEntities db = new MitraisCodingTestEntities())
                {
                    db.Registrations.Add(registration);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, registration);
                    message.Headers.Location = new Uri(Request.RequestUri + registration.RegistrationId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using (MitraisCodingTestEntities db = new MitraisCodingTestEntities())
            {
                var entity = db.Registrations.FirstOrDefault(e => e.RegistrationId == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Registration with id=" + id.ToString() + "not found to delete");
                }
                else
                {
                    db.Registrations.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Registration registration)
        {
            using (MitraisCodingTestEntities db = new MitraisCodingTestEntities())
            {
                try
                {
                    var entity = db.Registrations.FirstOrDefault(e => e.RegistrationId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Registration with id=" + id.ToString() + "not found to update");
                    }
                    else
                    {
                        entity.MobileNumber = registration.MobileNumber;
                        entity.FirstName = registration.FirstName;
                        entity.LastName = registration.LastName;
                        entity.DateOfBirth = registration.DateOfBirth;
                        entity.Gender = registration.Gender;
                        entity.Email = registration.Email;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
