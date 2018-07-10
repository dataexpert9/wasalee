using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
