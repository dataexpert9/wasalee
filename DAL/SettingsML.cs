using Component.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class SettingsML
    {
        public int Id { get; set; }

        public string AboutUs { get; set; }

        public string PrivacyPolicy { get; set; }

        public string TermsOfUse { get; set; }

        public string Currency { get; set; }

        public CultureType Culture { get; set; }

        public int Setting_Id { get; set; }

        public virtual Settings Settings { get; set; }

    }
}
