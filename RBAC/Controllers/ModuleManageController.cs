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
        //显示
        public ActionResult showData()
        {
            //只获取当前实体
            mr.Configuration.ProxyCreationEnabled = false;
            List<Module> mlist = mr.Module.ToList();
            var result = new { code = 0, msg = "", count = 500, data = mlist };//构建一个符合模版接口要求的数据
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //查看
        public ActionResult details()
        {
            int id = int.Parse(Request["id"]);
            Module module = mr.Module.FirstOrDefault(x => x.id==id);
            return View(module);
        }
        //编辑
        public ActionResult edit()
        {
            int id = int.Parse(Request["id"]);
            Module module = mr.Module.FirstOrDefault(x => x.id == id);
            return View(module);
        }
        public ActionResult editData(int id,string Name,string Controller)
        {
            Module module = mr.Module.FirstOrDefault(x => x.id == id);
            module.Name = Name;
            module.Controller = Controller;
            mr.SaveChanges();
            return RedirectToAction("Index", "ModuleManage");

        }
        //删除
        public ActionResult delete()
        {
            int id = int.Parse(Request["id"]);
            Module module = mr.Module.FirstOrDefault(x => x.id == id);
            if(module.Role.Count()>0)
            {
                return Json(false);
            }
            else
            {
                mr.Module.Remove(module);
                mr.SaveChanges();
                return Json(true);
            }
        }
        //添加
        public ActionResult add()
        {
            return View();
        }
        public ActionResult addData(string Name,string Controller)
        {
            Module module = new Module();
            module.Name = Name;
            module.Controller = Controller;
            mr.Module.Add(module);
            mr.SaveChanges();
            return RedirectToAction("Index", "ModuleManage");
        }
    }
}