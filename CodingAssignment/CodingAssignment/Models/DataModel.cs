
using System.Collections.Generic;

namespace CodingAssignment.Models
{
    public class DataModel
    {
        public int Id { get; set; }
        public List<string> Values { get; set; }

        public DataModel()
        {
            Values = new List<string>();
        }
    }
}