using CodingAssignment.Models;
using CodingAssignment.Services;
using CodingAssignment.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;


namespace UnitTestProject
{
    [TestClass]
    public class FileManagerServiceTests
    {

        private IFileManagerService _service;

        [TestInitialize]
        public void Init()
        {
            // Always revert back to original in every test
            const string filePath = "AppData/DataFile.json";

            DataModel dataModel1 = new DataModel()
            {
                Id = 1,
                Values = new List<string>() { "Value1a", "Value1b", "Value1c", "Value1d" }
            };

            DataModel dataModel2 = new DataModel()
            {
                Id = 2,
                Values = new List<string>() { "Value2a", "Value2b", "Value2c", "Value2d" }
            };

            DataFileModel dataFileModel = new DataFileModel();
            dataFileModel.Data.Add(dataModel1);
            dataFileModel.Data.Add(dataModel2);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(dataFileModel));


            _service = new FileManagerService(filePath);
        }

        [TestMethod]
        public void DeserailizeDataTest()
        {
            //Arrange

            //Act
            var res = _service.GetData();

            //Assert
            Assert.IsNotNull(res);

            Assert.IsInstanceOfType(res, typeof(DataFileModel));

        }

        [TestMethod]
        public void TestGetData_IfItContainsDataModel1()
        {
            //Arrange
            DataModel dataModel1 = new DataModel()
            {
                Id = 1,
                Values = new List<string>() { "Value1a", "Value1b", "Value1c", "Value1d" }
            };
            bool isContains = false;

            //Act
            var res = _service.GetData();

            //Assert

            foreach(DataModel m in res.Data)
            {
                if(m.Id == dataModel1.Id)
                {
                    if(m.Values.Count != dataModel1.Values.Count)
                    break;

                    isContains = true;

                    for(int i=0; i < m.Values.Count; i++)
                    {
                        if(!m.Values[i].Equals(dataModel1.Values[i]))
                        {
                            isContains = false;
                            break;
                        }
                    }
                }
            }

            Assert.IsTrue(isContains);

        }

        [TestMethod]
        public void TestGetDataModel_NotNull()
        {
            //Arrange
            DataModel dataModel1 = new DataModel()
            {
                Id = 1,
                Values = new List<string>() { "Value1a", "Value1b", "Value1c", "Value1d" }
            };

            //Act
            var res = _service.GetDataModel(dataModel1.Id);

            //Assert

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(DataModel));
        }

        [TestMethod]
        public void TestGetDataModel_Null()
        {
            //Arrange
            int indexOutOfBounds = 100;

            //Act
            var res = _service.GetDataModel(indexOutOfBounds);

            //Assert
            Assert.IsNull(res);
        }

        [TestMethod]
        public void TestInsertDataModel_True()
        {
            //Arrange
            DataModel dataModel = new DataModel()
            {
                Id = 3,
                Values = new List<string>() { "Value3a", "Value3b", "Value3c", "Value3d" }
            };

            //Act
            bool res = _service.Insert(dataModel);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestInsertDataModel_False()
        {
            //Arrange
            DataModel dataModel = new DataModel()
            {
                Id = 3,
                Values = new List<string>() { "Value3a", "Value3b", "Value3c", "Value3d" }
            };

            //Act
            bool res = _service.Insert(dataModel);
            res = _service.Insert(dataModel);

            //Assert
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestUpdateDataModel_True()
        {
            //Arrange
            DataModel dataModel = new DataModel()
            {
                Id = 1,
                Values = new List<string>() { "Value" }
            };

            //Act
            bool res = _service.Update(dataModel, dataModel.Id);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestUpdateDataModel_False()
        {
            //Arrange
            DataModel dataModel = new DataModel()
            {
                Id = 3,
                Values = new List<string>() { "Value3a", "Value3b", "Value3c", "Value3d" }
            };

            //Act
            bool res = _service.Update(dataModel, dataModel.Id);

            //Assert
            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestDeleteDataModel_True()
        {
            //Arrange
            DataModel dataModel = new DataModel()
            {
                Id = 1,
                Values = new List<string>() { "Value" }
            };

            //Act
            bool res = _service.Delete(dataModel.Id);

            //Assert
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestDeleteDataModel_False()
        {
            //Arrange
            DataModel dataModel = new DataModel()
            {
                Id = 3,
                Values = new List<string>() { "Value3a", "Value3b", "Value3c", "Value3d" }
            };

            //Act
            bool res = _service.Delete(dataModel.Id);

            //Assert
            Assert.IsFalse(res);
        }
    }
}
