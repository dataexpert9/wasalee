using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Component.Models.Common
{
    class CommonBindingModels
    {
    }
    public class UploadImagesBindingModel
    {
        [Required]
        public List<IFormFile> Images { get; set; }

        [Required]
        public ImageType Type { get; set; }
    }

    public enum ImageType
    {
        Request=0,
        User=1,
        Driver=2
    }
    
}
