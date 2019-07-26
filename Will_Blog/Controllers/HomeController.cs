using Will_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Will_Blog.Views.View_Models;
using System.Net.Mail;
using System.Configuration;
using System.Net.Http;

namespace Will_Blog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var posts = db.Posts.Where(b => b.Published).OrderByDescending(b => b.Created).Take(6).ToList();
            var myLandingPageVM = new LandingPageVM
            {
                TopLeft = posts.FirstOrDefault(),
                TopMiddle = posts.Skip(1).FirstOrDefault(),
                TopRight = posts.Skip(2).FirstOrDefault(),
                BottomLeft = posts.Skip(3).FirstOrDefault(),
                BottomMiddle = posts.Skip(4).FirstOrDefault(),
                BottomRight = posts.LastOrDefault()
            };
            return View(myLandingPageVM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var from = model.FromName + ":  " + $"{model.FromEmail}<{ConfigurationManager.AppSettings["emailto"]}>";
                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = model.Subject,
                        Body = model.Body,
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }
    }        
}

