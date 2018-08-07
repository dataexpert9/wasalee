using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBOStore
    {
        List<Cuisine> HomeCuisines();
        List<Store> HomeRestaurants();

    }
}
