using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucky.Models;
namespace Lucky.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
           
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(AddWinningNumber winningNumber,string Random)
        {
            if (Random.Equals("1"))
            {
                   var randomNumber = GetRandom();
                    winningNumber.LuckyNo = randomNumber;
                try
                {
                    db.AddWinningNumbers.Add(winningNumber);
                   if(db.SaveChanges()>0){

                    ViewBag.message =  "The last number you  enter is :" + winningNumber.LuckyNo;
                        
                    }
                    else
                    {
                        ViewBag.message = "Something Went wrong!";
                    }
                    return View();
                      }
                catch(Exception e)
                {
                    ModelState.AddModelError("Random", "Duplicate number or some Wrong ");
                    return View();
                }
            }
            else if (string.IsNullOrEmpty(winningNumber.Byhand))
            {
                ModelState.AddModelError("Byhand", "Enter 4 digit number");
                return View();
            }
            else
            {
                try
                {
                    winningNumber.LuckyNo = Convert.ToInt32(winningNumber.Byhand);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Byhand", "Enter 4 Digit not character");
                }

                db.AddWinningNumbers.Add(winningNumber);
                try
                {
                    if (db.SaveChanges() > 0)
                    {
                        ViewBag.message = "The last number you  enter is :" + winningNumber.LuckyNo;

                    }
                    else
                    {
                        ViewBag.message = "Something Went Wrong";
                    }
                }
                catch(Exception ee)
                {
                    ViewBag.error ="Duplicate number or validation fail try 4 Digit Number";
                    return View();
                }
                  
                
              
                
                return View();

            }


         //   return View();
        }

        private int GetRandom()
        {
            Random random = new Random();
           return  random.Next(1000, 9999);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Result()
        {
            List<WinnerList> winnerssss = new List<WinnerList>();
            var data = db.PrizeDetails.ToList();
            if (data == null)
            {
                return Content("No Result");
            }
            else
            {
                foreach (var winner in data)
                {
                    WinnerList winnerList = new WinnerList();
                    var user = db.UserRegisters.Where(x => x.Id == winner.Id).FirstOrDefault();
                    winnerList.UserName = user.Username.ToString();
                    winnerList.Email = user.Email;
                    winnerList.WinningNumber = Convert.ToInt32(winner.WinningNumber);
                    winnerList.PrizeType = winner.PrizeType;
                    winnerssss.Add(winnerList);

                }


            }
            return View(winnerssss);
        }
        }
}