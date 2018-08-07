using System.Linq;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interface;

namespace BLL
{
    public class BLLSettings : IBOSettings
    {
        public DataContext _dbContext { get; set; }
        public BLLSettings(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Settings LoadSettings()
        {
            try
            {
                var setting = _dbContext.Settings.FirstOrDefault();
                if (setting != null)
                {
                    return setting;
                }
                else
                    return new DAL.Settings();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
