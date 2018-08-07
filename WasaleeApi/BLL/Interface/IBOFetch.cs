using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Wasalee.BindingModels.FetchAnything;

namespace BLL.Interface
{
    public interface IBOFetch
    {
        RequestItem RequestItem(RequestItemBindingModel model);
        //bool SaveRequestImages(RequestItemBindingModel model, int RequestItem_Id);
        double GetDistance();
    }
}
