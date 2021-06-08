using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodingAssignment.Models;
using CodingAssignment.Services.Interfaces;
using Newtonsoft.Json;

namespace CodingAssignment.Services
{
    public class FileManagerService : IFileManagerService
    {
        public DataFileModel GetData()
        {
            var data = JsonConvert.DeserializeObject<DataFileModel>(File.ReadAllText("./AppData/DataFile.json"));

            return data;
        }

        public bool Insert(DataModel model)
        {
            var dataFileModel = JsonConvert.DeserializeObject<DataFileModel>(File.ReadAllText("./AppData/DataFile.json"));

            foreach(DataModel m in dataFileModel.Data)
            {
                if(m.Id == model.Id)
                {
                    return false;
                }
            }

            dataFileModel.Data.Add(model);
            File.WriteAllText("./AppData/DataFile.json", JsonConvert.SerializeObject(dataFileModel));

            return true;
        }

        public bool Update(DataModel model, int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
