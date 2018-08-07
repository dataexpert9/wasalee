using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Settings
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<SettingsML> SettingsML { get; set; }
    }
}
