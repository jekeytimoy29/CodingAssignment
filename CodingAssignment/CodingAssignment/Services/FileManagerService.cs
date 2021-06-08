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
            var jsonData = File.ReadAllText("./AppData/DataFile.json");
            var dataFileModel = JsonConvert.DeserializeObject<List<DataModel>>(jsonData) ?? new List<DataModel>();

            foreach(DataModel m in dataFileModel)
            {
                if(m.Id == model.Id)
                {
                    return false;
                }
            }

            dataFileModel.Add(model);

            jsonData = JsonConvert.SerializeObject(dataFileModel);
            File.WriteAllText("./AppData/DataFile.json", jsonData);

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
