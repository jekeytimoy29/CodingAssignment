using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingAssignment.Models;
using CodingAssignment.Services;
using CodingAssignment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodingAssignment.Controllers
{
    [ApiController]
    [Route("/File")]
    public class FileController : ControllerBase
    {
        private readonly IFileManagerService _fileManger;

        public FileController(IFileManagerService _fileManger)
        {
            this._fileManger = _fileManger;
        }

        [HttpGet]
        public DataFileModel Get()
        {
            return this._fileManger.GetData();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataModel(int id)
        {
            var res = this._fileManger.GetDataModel(id);
            if (null == res)
                return NotFound();

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model != null 
                && model.Id >= 0)
            {
                if(this._fileManger.Insert(model))
                    return Ok(Get());
            }

            return UnprocessableEntity();
        }

        [HttpPut]
        public async Task<IActionResult> Put(DataModel model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model != null && model.Id >= 0 && model.Id == id
                && this._fileManger.Update(model, id))
                    return Ok(Get());

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id >= 0 && this._fileManger.Delete(id))
                return Ok(Get());

            return NotFound();
        }
    }
}
