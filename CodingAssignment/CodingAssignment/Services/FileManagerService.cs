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
        private readonly string _pathFileName;

        public FileManagerService(string pathFileName)
        {
            this._pathFileName = pathFileName;
        }

        public DataFileModel GetData()
        {
            return JsonConvert.DeserializeObject<DataFileModel>(File.ReadAllText(this._pathFileName));
        }

        public DataModel GetDataModel(int id)
        {
            var dataFileModel = GetData();
            return ReturnDataModelIfExists(dataFileModel, id);
        }

        public bool Insert(DataModel model)
        {
            var dataFileModel = GetData();
            if (null == ReturnDataModelIfExists(dataFileModel, model.Id))
            {
                dataFileModel.Data.Add(model);
                return SaveChangesToFile(dataFileModel);
            }

            return false;
        }

        public bool Update(DataModel model, int id)
        {
            var dataFileModel = GetData();
            var dataModel = ReturnDataModelIfExists(dataFileModel, id);
            if (null != dataModel)
            {
                dataFileModel.Data[dataFileModel.Data.IndexOf(dataModel)] = model;
                return SaveChangesToFile(dataFileModel);
            }

            return false;
        }

        public bool Delete(int id)
        {
            var dataFileModel = GetData();
            var dataModel = ReturnDataModelIfExists(dataFileModel, id);
            if (null != dataModel)
            {
                dataFileModel.Data.Remove(dataModel);
                return SaveChangesToFile(dataFileModel);
            }

            return false;
        }

        private bool SaveChangesToFile(DataFileModel dataFileModel)
        {
            try
            {
                File.WriteAllText(this._pathFileName, JsonConvert.SerializeObject(dataFileModel));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private DataModel ReturnDataModelIfExists(DataFileModel dataFileModel, int id)
        {
            if (dataFileModel != null && dataFileModel.Data.Count > 0)
            {
                foreach (DataModel m in dataFileModel.Data)
                {
                    if (m.Id == id)
                    {
                        return m;
                    }
                }
            }

            return null;
        }
    }
}
