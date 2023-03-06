﻿using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Students.API.Models;
using Students.API.Models.Repository;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Students.API.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IDataRepository<Student> _dataRepository;

        public StudentController(IDataRepository<Student> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        //GET: api/student
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Student> students = _dataRepository.GetAll();
            return Ok(students);
        }

        //GET: api/student/2
        [HttpGet("{id}", Name ="Get")]
        public IActionResult Get(int id)
        {
            Student student = _dataRepository.Get(id);
            if(student== null)
            {
                return NotFound("This student record could not be found");
            }

            return Ok(student);
        }

        //POST: api/student
        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            if(student==null)
            {
                return BadRequest("Student is null.");
            }

            _dataRepository.Add(student);

            return CreatedAtRoute(
                "Get",
                new { Id = student.StudentID },
                student);
        }

        //PUT api/student/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            if(student==null)
            {
                return BadRequest("Student is null.");
            }

            Student studentToUpdate=_dataRepository.Get(id);
            if(studentToUpdate == null)
            {
                return NotFound("the student record could not be found.");
            }

            _dataRepository.Update(studentToUpdate,student);
            return NoContent();
        }

        //DELETE: api/employee/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student=_dataRepository.Get(id);
            if(student==null)
            {
                return NotFound("The student record couldn't be found.");
            }
            _dataRepository.Delete(student);

            return NoContent();
        }

        //PATCH: api/employee/2
        [HttpPatch("{id}")]
        public IActionResult Patch(int id,[FromBody] JsonPatchDocument<Student> patchDoc)
        {
            if(patchDoc==null)
            {
                return BadRequest("Patch doc is null");
            }
            var student=_dataRepository.Get(id);
            if(student==null)
            {
                return NotFound("The student record couldn't be found.");
            }

            var studentToPatch = new Student()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                City = student.City
            };

            patchDoc.ApplyTo(studentToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);

            }

            student.FirstName= studentToPatch.FirstName;
            student.LastName= studentToPatch.LastName;
            student.City= studentToPatch.City;

            _dataRepository.Save();
            return NoContent();
        }


    }
}