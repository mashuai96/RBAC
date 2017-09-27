using RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RBAC.Controllers
{
    public class ModuleManageController : Controller
    {
        // GET: ModuleManage
        MyRBAC mr = new MyRBAC();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult showData()
        {
            //只获取当前实体
            mr.Configuration.ProxyCreationEnabled = false;
            List<Module> mlist = mr.Module.ToList();
            var result = new { code = 0, msg = "", count = 500, data = mlist };//构建一个符合模版接口要求的数据
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}