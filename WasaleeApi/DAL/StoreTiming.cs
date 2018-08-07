using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public class StoreTiming
    {
        [Key, ForeignKey("Store")]
        public int? Id { get; set; }
        public TimeSpan Monday_From { get; set; }
        public TimeSpan Monday_To { get; set; }
        public TimeSpan Tuesday_From { get; set; }
        public TimeSpan Tuesday_To { get; set; }
        public TimeSpan Wednesday_From { get; set; }
        public TimeSpan Wednesday_To { get; set; }
        public TimeSpan Thursday_From { get; set; }
        public TimeSpan Thursday_To { get; set; }
        public TimeSpan Friday_From { get; set; }
        public TimeSpan Friday_To { get; set; }
        public TimeSpan Saturday_From { get; set; }
        public TimeSpan Saturday_To { get; set; }
        public TimeSpan Sunday_From { get; set; }
        public TimeSpan Sunday_To { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }


    }
}
