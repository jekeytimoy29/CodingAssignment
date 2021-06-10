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
        public async Task<IActionResult> Get()
        {
            var result = await this._fileManger.GetData();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataModel(int id)
        {
            var dataModel = await this._fileManger.GetDataModel(id);
            if (null == dataModel)
                return NotFound();

            return Ok(dataModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if(this._fileManger.Insert(model))
                return Ok(await this._fileManger.GetData());

            return UnprocessableEntity();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DataModel model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if(model.Id == id &&
                this._fileManger.Update(model, id))
                return Ok(await this._fileManger.GetData());

            return NotFound();
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if(this._fileManger.Delete(id))
                return Ok(await this._fileManger.GetData());

            return NotFound();
        }
    }
}
