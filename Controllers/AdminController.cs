using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucky.Models;

namespace Lucky.Controllers
{
    public class AdminController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private Random r = new Random();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(PrizeDetail prize,string Random,string Number)
        {
            if (prize.PrizeType == null)
            {
                //ModelState.AddModelError("message", "Please select prize type bro");
                return View();
            }
            if (prize.PrizeType.Equals("1"))
            {
                if (GetGrandPirze() != 0)
                {
                    int firstwinner = GetGrandPirze();
                    var user = db.AddWinningNumbers.Single(x => x.LuckyNo ==firstwinner);
                    PrizeDetail winner = new PrizeDetail() { PrizeType = "1", UserId = user.Id.ToString(), WinningNumber = GetGrandPirze().ToString() };
                    var prizeDetail = db.PrizeDetails.Where(x => x.PrizeType.Equals(prize.PrizeType)).FirstOrDefault();
                    if (prizeDetail == null)
                    {
                        if (CleanSpace(winner) == true)
                        {
                            return RedirectToAction("Index");
                        }
                        ModelState.AddModelError("message", "Something Went Wrong");


                    }
                    else
                    {
                        ModelState.AddModelError("PrizeType", "The prize is already draw");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("message", "There is no user to Draw");
                }
            }
            else if(Random.Equals("0"))
            {
                if (string.IsNullOrEmpty(Number))
                {
                    ModelState.AddModelError("Number", "You have to enter 4 digit number");
                    return View();
                }
                else
                {
                    try
                    {
                        var number = Convert.ToInt32(Number);
                        var user = db.AddWinningNumbers.Where(x => x.LuckyNo == number).FirstOrDefault();
                        if (user == null)
                        {
                            ModelState.AddModelError("Number", "The number did't not match!");
                            return View();
                        }
                        else
                        {
                            PrizeDetail prize1 = new PrizeDetail() { PrizeType = prize.PrizeType, UserId = user.UserId, WinningNumber = number.ToString() };
                            var prizeDetail = db.PrizeDetails.Where(x => x.PrizeType.Equals(prize.PrizeType)).FirstOrDefault();
                            if (prizeDetail == null)
                            {
                                if (CleanSpace(prize1) == true)
                                {
                                    return RedirectToAction("Index");
                                }
                                ModelState.AddModelError("message", "Something Went Wrong");


                            }
                            else
                            {
                                ModelState.AddModelError("PrizeType", "The prize is already draw");
                                return View();
                            }

                        }

                    }
                    catch(Exception e)
                    {
                        ModelState.AddModelError("Number", "Be Serious! 4 Digit Number Bro");
                        return View();
                    }
                    
                }

            }
            else
            {
                if (getSecondToLast() == 0)
                {
                    ModelState.AddModelError("PirzeType", "Please select Grand prize and then Others");
                    return View();
                }
                var winnerNumber = getSecondToLast();
                var user = db.AddWinningNumbers.Where(x => x.LuckyNo == winnerNumber).FirstOrDefault();
                PrizeDetail prize1 = new PrizeDetail { PrizeType = prize.PrizeType, WinningNumber = winnerNumber.ToString(), UserId = user.UserId };
                var prizeDetail = db.PrizeDetails.Where(x => x.PrizeType.Equals(prize.PrizeType)).FirstOrDefault();
                if (prizeDetail == null)
                {
                    if (CleanSpace(prize1) == true)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("message", "Something Went Wrong");

                }
                else
                {
                    ModelState.AddModelError("PrizeType", "The prize is already draw");
                    return View();
                }


            }
            return View();
        }

        private bool CleanSpace(PrizeDetail prize)
        {
            db.PrizeDetails.Add(prize);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private int GetGrandPirze()
        {
            //Random r = new Random();
            var lists = new List<int>();
            var grandPrizeWinners = db.AddWinningNumbers.GroupBy(x => x.UserId).OrderByDescending(x=>x.Count()).Take(2);
            if (grandPrizeWinners != null)
            {
                foreach (var group in grandPrizeWinners)
                {
                    foreach (var data in group)
                    {
                        lists.Add(data.LuckyNo);
                    }
                }

                var randomList = lists.OrderBy(x => r.Next(lists.Count));
                var index = r.Next(randomList.ToList().Count);
                return randomList.ToList()[index];
            }
            return 0;
        }

        private int getSecondToLast()
        {
             List<int> getAllWinningNumbers = new List<int>();
            List<int> getWinnerNumbersList = new List<int>();
            List<SelectListItem> remainingNumber = new List<SelectListItem>();
            foreach(var winningNumber in db.AddWinningNumbers.ToList())
            {
                getAllWinningNumbers.Add(winningNumber.LuckyNo);
            }

            var winnerList = db.PrizeDetails.ToList();
            if (winnerList == null)
            {
                return 0;
            }
            else
            {
                foreach(var winner in winnerList)
                {
                    var winnerWinningNumbers = db.AddWinningNumbers.Where(x => x.UserId.Equals(winner.UserId)).ToList();
                    foreach(var winnerWinningNumber in winnerWinningNumbers)
                    {
                        getWinnerNumbersList.Add(winnerWinningNumber.LuckyNo);
                    }
                }
            }

            foreach(var item in getWinnerNumbersList)
            {
                getAllWinningNumbers.Remove(item);
            }

            foreach(var item in getAllWinningNumbers)
            {
                remainingNumber.Add(new SelectListItem() { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.remainingNumber = remainingNumber;

            

            return getPrize(getAllWinningNumbers);
        }

        private int getPrize(List<int> getAllWinningNumbers)
        {
          

          return getAllWinningNumbers.OrderBy(x=>r.Next(getAllWinningNumbers.Count)).ToList()[r.Next(getAllWinningNumbers.ToList().Count)];
        }


    


    }
}