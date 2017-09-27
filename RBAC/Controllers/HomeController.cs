using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;
using RBAC.Filters;

namespace RBAC.Controllers
{
    [CostomAuthorize(s = "identity")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //头部分布视图
        public ActionResult Header()
        {
            //显示用户名称
            Users user = Session["user"] as Users;
            //   角色列表
            List<Role> roles = Session["roles"] as List<Role>;
            List<SelectListItem> rlist = new List<SelectListItem>();
            Role role = Session["role"] as Role;
            foreach (Role item in roles)
            {
                rlist.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.id.ToString(),
                    Selected = item.id == role.id
                });
            }
            ViewBag.rlist = rlist;
            return PartialView(user);
        }
        public ActionResult Nav(int id=0)
        {
            int roleid= 0;
            if(id==0)
            {
                Role role = Session["role"] as Role;
                roleid = role.id;
            }
            else
            {
                roleid = int.Parse(Request["roleid"]);
            }
            
            //获取对应模块的
            Dictionary<int, ICollection<Module>> dic = Session["roleModules"] as Dictionary<int, ICollection<Module>>;
            ICollection<Module> modules = dic[roleid];
            return PartialView(modules);
        }
    }
}