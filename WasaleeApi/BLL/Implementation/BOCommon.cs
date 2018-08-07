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

        public List<UploadImagesViewModel> UploadImages(UploadImagesBindingModel model)
        {
            List<string> ImageUrls = new List<string>();
            List<UploadImagesViewModel> returnModel = new List<UploadImagesViewModel>();
            if (model.Images != null)
            {
                foreach (var image in model.Images)
                {
                    returnModel.Add(new UploadImagesViewModel{
                        ImageUrl= image.SaveImage(model.Type.ToString()),
                    });
                }
            }
            return returnModel;
        }


    }
}
