using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Cuisine
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual List<StoreCuisine> StoreCuisine { get; set; }
    }
}
