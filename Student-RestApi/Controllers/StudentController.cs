using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Student_RestApi.ViewModels;
using Student_RestApi.Models;
using Student_RestApi.Infrastructure;

namespace Student_RestApi.Controllers
{
    [ApiController]
    [Route("Students")]
    public class StudentController : Controller
    {
        private XMLGenerator _XML = new XMLGenerator();
        [HttpGet]
        public IList<Student> Index()
        {
            List<Student> students = _XML.GetChildren();

            while(students.Count < 3)
            {
                students.Add(new Student());
            }

            return students;
        } 

        [HttpPost]
        public Response Index([FromBody]VMStudent studentObj)
        {
            Response _response = new Response();
            _response.isSuccessfull = _XML.AddChild(studentObj);
            return _response;
        }

        [HttpPut]
        public Response Index([FromQuery]int id, [FromBody]VMStudent studentObj)
        {
            Response _response = new Response();
            _response.isSuccessfull = _XML.UpdateChild(id, studentObj);
            return _response;
        }

        [HttpDelete]
        public Response Index([FromQuery]int id)
        {
            Response _response = new Response();
            _response.isSuccessfull = _XML.RemoveChild(id);
            return _response;
        }
    }
}
