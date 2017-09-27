using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;

namespace RBAC.Filters
{
    //枚举
    public enum userIdentity { none,identity,authorize};
    public class CostomAuthorizeAttribute:AuthorizeAttribute
    {
        public userIdentity ui { get; set; }
        public string s { get; set; }
        //子类中需要实现父类的虚方法override
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (ui == userIdentity.none)
                return;
            //身份验证
           if(filterContext.HttpContext.Session["user"]==null)
            {
                UrlHelper url = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(url.Action("Index", "Login"));
            }

            if (ui == userIdentity.identity)
                return;
            //权限验证
            //1获取请求的控制器名称
            string controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //2获取角色
            Role role = filterContext.HttpContext.Session["role"] as Role;
            //获取角色对应的模块
            Module module = role.Module.FirstOrDefault(x => x.Controller == controllername);
            if(module==null)
            {
                UrlHelper url = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(url.Action("Index", "Login"));
            }
        }
    }
}