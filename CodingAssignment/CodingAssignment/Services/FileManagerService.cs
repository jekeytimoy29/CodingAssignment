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
        private const string PATH = "./AppData/DataFile.json";

        public DataFileModel GetData()
        {
            var data = JsonConvert.DeserializeObject<DataFileModel>(File.ReadAllText(PATH));
            return data;
        }
        
        public DataModel GetDataModel(int id)
        {
            var dataFileModel = GetData();

            if (dataFileModel != null)
            {
                return GetDataModel(dataFileModel, id);
            }

            return null;
        }

        public bool Insert(DataModel model)
        {
            var dataFileModel = GetData();

            if (dataFileModel != null)
            {
                if(null == GetDataModel(dataFileModel, model.Id))
                {
                    dataFileModel.Data.Add(model);
                    
                    return SaveChangesToFile(dataFileModel);
                }
            }

            return false;
        }

        public bool Update(DataModel model, int id)
        {
            var dataFileModel = GetData();

            if (dataFileModel != null && dataFileModel.Data.Count > 0)
            {
                var dataModel = GetDataModel(dataFileModel, id);
                if (null != dataModel)
                {
                    dataFileModel.Data[dataFileModel.Data.IndexOf(dataModel)] = model;
                    return SaveChangesToFile(dataFileModel);
                }
            }

            return false;
        }

        public bool Delete(int id)
        {
            var dataFileModel = GetData();

            if (dataFileModel != null && dataFileModel.Data.Count > 0)
            {
                var dataModel = GetDataModel(dataFileModel, id);
                if (null != dataModel)
                {
                    dataFileModel.Data.Remove(dataModel);
                    return SaveChangesToFile(dataFileModel);
                }
            }

            return false;
        }

        private bool SaveChangesToFile(DataFileModel dataFileModel)
        {
            try
            {
                File.WriteAllText(PATH, JsonConvert.SerializeObject(dataFileModel));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private DataModel GetDataModel(DataFileModel dataFileModel, int id)
        {
            foreach (DataModel m in dataFileModel.Data)
            {
                if (m.Id == id)
                {
                    return m;
                }
            }

            return null;
        }
    }
}
