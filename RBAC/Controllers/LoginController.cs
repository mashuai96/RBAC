using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RBAC.Models;
using RBAC.Filters;

namespace RBAC.Controllers
{
    [CostomAuthorize(s ="none")]
    public class LoginController : Controller
    {

        // GET: Login
        MyRBAC m = new MyRBAC();
        Users u = new Users();   
        public ActionResult Index()
        {
            return View();
        }
        //登录
        public ActionResult Login(string username,string password)
        {
            Users us = m.Users.FirstOrDefault(x => x.Username == username && x.Passord==password);
            if (us != null)
            {
                Session["user"] = us;   //用户对象
                //角色列表
                List<Role> roles = us.Role.ToList();
                Session["roles"] = roles;
                Session["role"] = roles[0];
                //角色模块
                Session["roleModules"] = us.Role.ToDictionary(r => r.id, r => r.Module);
                return Json(new { code = 200 });
               
            }
            else
            {
                return Json(new { code = 404 });
            }
        }
        //注册
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AddData(string username,string password)
        {
            u.Username = username;
            u.Passord = password;
            Role role = m.Role.FirstOrDefault(x => x.id == 3);
            u.Role.Add(role);
            m.Users.Add(u);
            int i = m.SaveChanges();
            return Json(new { code = 200 });
        }
    }
}