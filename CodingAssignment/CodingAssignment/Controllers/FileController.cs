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

        [HttpPost]
        public DataFileModel Post(DataModel model)
        {
            if(this._fileManger.Insert(model))
                return Get();
            
            return null;
        }

        [HttpPut]
        public DataFileModel Put(DataModel model, int id)
        {
            if(this._fileManger.Update(model, id))
                return Get();
            
            return null;
        }

        [HttpDelete]
        public DataFileModel Delete(int id)
        {
            if(this._fileManger.Delete(id))
                return Get();
            
            return null;
        }
    }
}
