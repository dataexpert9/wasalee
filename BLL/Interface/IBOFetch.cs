using Component.Utility.Enums;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Wasalee.BindingModels.FetchAnything;

namespace BLL.Interface
{
    public interface IBOFetch
    {
        RequestItem RequestItem(RequestItemBindingModel model,CultureType culture);
        //bool SaveRequestImages(RequestItemBindingModel model, int RequestItem_Id);
        List<RequestItem> GetPendingRequests(int User_Id,int Items,int Page,CultureType culture);
        List<RequestItem> GetDeliveredOrCompletedRequests(int User_Id, int Items, int Page, CultureType culture);
        RequestItem GetRequestById(int Request_Id,CultureType culture);
        double GetDistance();
    }
}
