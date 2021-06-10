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

        public async Task<DataFileModel> GetData()
        {
            string jsonString = await File.ReadAllTextAsync(this._pathFileName);
            return JsonConvert.DeserializeObject<DataFileModel>(jsonString);
        }

        public async Task<DataModel> GetDataModel(int id)
        {
            var dataFileModel = await GetData();
            return ReturnDataModelIfExists(dataFileModel, id);
        }

        public bool Insert(DataModel model)
        {
            var dataFileModel = GetData().Result;

            if(null == ReturnDataModelIfExists(dataFileModel, model.Id))
            {
                dataFileModel.Data.Add(model);
                SaveChangesToFileAsync(dataFileModel);
                return true;
            }

            return false;
        }

        public bool Update(DataModel model, int id)
        {
            var dataFileModel = GetData().Result;
            var dataModel = ReturnDataModelIfExists(dataFileModel, id);

            if(null != dataModel)
            {
                dataFileModel.Data[dataFileModel.Data.IndexOf(dataModel)] = model;
                SaveChangesToFileAsync(dataFileModel);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var dataFileModel = GetData().Result;
            var dataModel = ReturnDataModelIfExists(dataFileModel, id);

            if(null != dataModel)
            {
                dataFileModel.Data.Remove(dataModel);
                SaveChangesToFileAsync(dataFileModel);
                return true;
            }

            return false;
        }

        private async void SaveChangesToFileAsync(DataFileModel dataFileModel)
        {
            await File.WriteAllTextAsync(this._pathFileName, JsonConvert.SerializeObject(dataFileModel));
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
