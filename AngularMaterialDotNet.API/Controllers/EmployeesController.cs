using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngularMaterialDotNet.API.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Web;

namespace AngularMaterialDotNet.API.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Employees
        public List<EmployeeResponse> GetEmployees()
        {
            var userId = User.Identity.GetUserId();
            Mapper.CreateMap<Employee, EmployeeResponse>();
            return Mapper.Map<List<EmployeeResponse>>(db.Employees.Where(x => x.ApplicationUserId == userId));
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            if (User.Identity.GetUserId() != employee.ApplicationUserId)
            {
                return BadRequest("This is not your employee");
            }

            Mapper.CreateMap<Employee, EmployeeResponse>();
            return Ok(Mapper.Map<EmployeeResponse>(employee));
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, EmployeeRequest employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = db.Employees.Find(id);

            if (User.Identity.GetUserId() != employee.ApplicationUserId)
            {
                return BadRequest("This is not your employee");
            }
            Mapper.CreateMap<EmployeeRequest, Employee>();
            employee = Mapper.Map<EmployeeRequest, Employee>(employeeRequest, employee);

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(EmployeeRequest employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.CreateMap<EmployeeRequest, Employee>();
            var employee = Mapper.Map<Employee>(employeeRequest);
            // Set creation date
            employee.CreationDate = DateTime.Now;
            employee.ApplicationUserId = User.Identity.GetUserId();
            db.Employees.Add(employee);
            db.SaveChanges();

            Mapper.CreateMap<Employee, EmployeeResponse>();
            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeId }, Mapper.Map<EmployeeResponse>(employee));
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            if (User.Identity.GetUserId() != employee.ApplicationUserId)
            {
                return BadRequest("This is not your employee");
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            Mapper.CreateMap<Employee, EmployeeResponse>();
            return Ok(Mapper.Map<EmployeeResponse>(employee));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}