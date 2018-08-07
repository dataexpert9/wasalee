using BLL.Interface;
using Component.Models.Common;
using Component.Utility;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Implementation
{
    public class BOCommon : IBOCommon
    {
        public DataContext _dbContext { get; set; }

        public BOCommon(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UploadImagesViewModel UploadImages(UploadImagesBindingModel model)
        {
            
            UploadImagesViewModel returnModel = new UploadImagesViewModel();
            if (model.Image != null)
            {

                returnModel = new UploadImagesViewModel
                {
                    ImageUrl = model.Image.SaveImage(model.Type.ToString()),
                };
            }
            return returnModel;
        }


    }
}
