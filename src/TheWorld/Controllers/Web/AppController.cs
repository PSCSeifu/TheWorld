using System;
using Microsoft.AspNet.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using TheWorld.Models;
using System.Linq;
using Microsoft.AspNet.Authorization;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailservice;
        private IWorldRepository _repository;
        public AppController(IMailService service, IWorldRepository repository)
        {
            _mailservice = service;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            var trips = _repository.GetAllTrips();

            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration Problem");
                }

               if( _mailservice.SendMail(email,
                    email,
                    $"Contact Page From {model.Name} ({model.Email})",
                model.Message))
                {
                    ModelState.Clear();
                    ViewBag.Message = "Mail Sent. Thanks!";
                }
            }

            return View();
        }
    }
}
