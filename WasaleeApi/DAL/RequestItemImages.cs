using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class RequestItemImages
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public int RequestItem_Id { get; set; }

        public RequestItem RequestItem { get; set; }
    }
}
