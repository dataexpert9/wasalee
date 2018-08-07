using Component.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBOCommon
    {
        List<UploadImagesViewModel> UploadImages(UploadImagesBindingModel model);
    }
}
