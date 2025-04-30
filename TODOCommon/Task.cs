using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOCommon
{
    public class Task
    {
        private string _status;
        public int TaskId { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status
        {
            get { return _status; }
            set
            {
                if (value.Equals("Not Started") || value.Equals("In Progress") || value.Equals("Done"))
                {
                    _status = value;
                }
            }
        }
    }
}