using System;
using System.Collections.Generic;
using System.Text;

namespace Component.Utility
{
   public class MyRegularExpressions
    {
        public const string Price = @"^\$?(\d{1,3}(\,\d{3})*|(\d+))(\.\d{2})?$";
        public const string Name = @"^[A-z]+$";
        public const string Email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    }
}
