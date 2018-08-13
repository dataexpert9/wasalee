using BLL.Interface;
using Component.Models.Common;
using Component.Models.Driver;
using Component.Utility;
using Component.Utility.Enums;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ReportProblemMessage> GetReportProblems(int Type, CultureType Culture)
        {
            return _dbContext.ReportProblemMessage.Where(x => x.Type == Type && x.IsDeleted == false && x.Culture == Culture).ToList();
        }

        public bool CancelRequest(CancelRequestBindingModel model)
        {
            var Request = _dbContext.RequestItem.FirstOrDefault(x => x.Id == model.RequestItem_Id);

            if (Request != null)
            {
                if(Request.Status != (int)RequestItemStatus.Cancelled) { 
                Request.Status = (int)RequestItemStatus.Cancelled;

                var cancelRequest = _dbContext.CancelItemReason.Add(new CancelItemReason
                {
                    CancelAt = DateTime.UtcNow,
                    ReportProblemMessage_Id = model.ReportProblemMessage_Id,
                    RequestItem_Id = model.RequestItem_Id
                });

                _dbContext.SaveChanges();

                return true;
                }
            }
            return false;
        }

    }
}
