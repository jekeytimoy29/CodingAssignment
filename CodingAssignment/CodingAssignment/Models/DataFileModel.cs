using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
