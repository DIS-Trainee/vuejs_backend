using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vue_project.Models

namespace vue_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipeInfoController : ControllerBase
    {
        public PipeInfoController()
        {
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<TModel>> GetTModels()
        {
            return new List<TModel> { };
        }

        [HttpGet("{id}")]
        public ActionResult<TModel> GetTModelById(int id)
        {
            return null;
        }

        [HttpPost("")]
        public ActionResult<TModel> PostTModel(TModel model)
        {
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutTModel(int id, TModel model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<TModel> DeleteTModelById(int id)
        {
            return null;
        }
    }
}