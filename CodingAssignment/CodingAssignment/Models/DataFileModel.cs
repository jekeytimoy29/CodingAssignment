using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CodingAssignment.Models
{
    public class DataFileModel
    {
        public List<DataModel> Data { get; set; }

        public DataFileModel()
        {
            Data = new List<DataModel>();
            
        }
    }
}
