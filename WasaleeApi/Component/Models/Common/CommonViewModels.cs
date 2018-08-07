using System;
using System.Collections.Generic;
using System.Text;

namespace Component.Models.Common
{
    class CommonViewModels
    {
    }
    public class UploadImagesViewModel
    {
        public string ImageUrl { get; set; }
        public int Type { get; set; }
        public string TypeText { get; set; }
    }
    public class UploadImagesContainer
    {
        public UploadImagesContainer()
        {
            Images = new List<UploadImagesViewModel>();
        }
        public List<UploadImagesViewModel> Images { get; set; }
    }
}
