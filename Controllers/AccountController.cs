using Lucky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lucky.Controllers
{
    public class AccountController : Controller
    {

        private DatabaseContext db = new DatabaseContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.UserRegisters.Add(register);


                    if (db.SaveChanges() > 0)
                    {
                        Session["UserId"] = register.Id.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Error occur");
                        return View();
                    }
                }
                catch (Exception ee)
                {
                    ModelState.AddModelError("Email", ee.Message);
                }

            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            var credential = db.UserRegisters.Where(x => x.Email.Equals(login.Email) && x.Password.Equals(login.Password)).FirstOrDefault();
            if (credential == null)
            {
                ModelState.AddModelError("Password", "Credential not Match or first Register U r Account");
                return View();
            }
            else
            {
                Session["UserId"] = credential.Id.ToString() ;
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}