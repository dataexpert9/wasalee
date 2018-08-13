using Component.Models.Common;
using Component.Models.Driver;
using Component.Utility.Enums;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBOCommon
    {
        UploadImagesViewModel UploadImages(UploadImagesBindingModel model);
        List<ReportProblemMessage> GetReportProblems(int Type,CultureType Culture);
        bool CancelRequest(CancelRequestBindingModel model);
    }
}
