using StudentManagement.Common;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class LoginController : Controller
    {
        StudentEntities1 db = new StudentEntities1();


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// this is Index Method & decryptPassword logic
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user obj)
        {
           /*if(ModelState.IsValid)
            {*/
                Password decryptPassword = new Password();
                using (StudentEntities1 db = new StudentEntities1())
                {
                    //var check = db.users.Where(a => a.username.Equals(obj.username) && a.password.Equals(obj.password)).FirstOrDefault();
                    var check = db.users.Where(a => a.username.Equals(obj.username)).FirstOrDefault();

                    if (check != null)
                    {
                        if (decryptPassword.DecryptPassword(check.password).Equals(obj.password))
                        {
                            Session["UserId"] = obj.id.ToString();
                            Session["UserName"] = obj.username.ToString();

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = " Password is incoreect.";
                        }
    
                    }
                    else
                    {
                        ModelState.AddModelError("", "the Ussename Or Password is InCorrect");
                    }
                }
           //}
            
            return View(obj);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}