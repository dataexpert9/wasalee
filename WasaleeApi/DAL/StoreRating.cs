using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class StoreRating
    {
        public int Id { get; set; }

        public double Rating { get; set; }

        public string Feedback { get; set; }
            
        public int Store_Id { get; set; }

        public virtual Store Store { get; set; }

    }
}
