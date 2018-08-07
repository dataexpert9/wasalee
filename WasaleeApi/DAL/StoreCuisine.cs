using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class StoreCuisine
    {

        public int Id { get; set; }

        public int Store_Id { get; set; }

        public int Cuisine_Id { get; set; }

        public virtual Store Store { get; set; }

        public virtual Cuisine Cuisine { get; set; }

    }
}
