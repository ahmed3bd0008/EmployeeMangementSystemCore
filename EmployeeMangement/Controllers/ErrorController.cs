using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscod}")]
        public IActionResult HttpStatusCodeHandling(int statuscod)
        {
            switch (statuscod)
            {
                case (404):
                    {
                        ViewBag.Error = ("sorry ther is not response for this");
                    }
                    break;
                case (500):
                    {
                        ViewBag.Error = ("sorry ther is not response for this");
                    }
                    break;
                default:
                    break;
            }
            return View();
        }
        //[Route("/Error")]
        //[AllowAnonymous]
        //public IActionResult Error()
        //{
        //    return View("Error");
        //}
    }
}
