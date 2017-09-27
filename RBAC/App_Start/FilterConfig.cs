using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Filters;

namespace RBAC.App_Start
{
    public class FilterConfig
    {
        public static void RegisterFilter(GlobalFilterCollection fileter)
        {
            fileter.Add(new CostomAuthorizeAttribute());//自定义的过滤器加入到全局过滤器当中

        }
    }
}