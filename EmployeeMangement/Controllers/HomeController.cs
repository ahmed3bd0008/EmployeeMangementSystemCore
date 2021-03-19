using EmployeeMangement.Models;
using EmployeeMangement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Controllers
{
    [Authorize]
    //2
    //[Route("[Controller]/[Action]")]
    public class HomeController : Controller
    {

        private readonly IEmployeeREpository _employeeREpository;
        private readonly IWebHostEnvironment _hostingEnvirnment;

        //constructor injection  IEmployeeREpository in homecontroller
        public HomeController(IEmployeeREpository employeeREpository, IWebHostEnvironment hostingEnvirnment)
        {
            _employeeREpository = employeeREpository;
            _hostingEnvirnment = hostingEnvirnment;
        }
        //1
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]

        /// <2>
        ///  //[Route("~/Home")]
        //[Route("~/")]
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_employeeREpository.GetAllEmployee());
        }
        //1
        //[Route("Home/Details/{id?}")]
        //[Route("{id?}")]
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //throw new Exception("asd");
            var model = _employeeREpository.GtEmployee(id ?? 1);
            if (model==null)
            {
                return View("NotFoundEmployee",id);
            }
            ViewBag.Employee = model;
            ViewData["Employee"] = model;
            ViewBag.PageTittle = "details frpm viewbaage";
            ViewData["PageTittle"] = "details from viewdata";

            /////
            HomeDetailsViewModel ViewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                PageTitle = "model view page"
            };
            return View(ViewModel);
        }
        [Authorize]
        public IActionResult Delete(int? id)
        {
            _employeeREpository.Delete(id ?? 1);
            return RedirectToAction("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeViewModel model)
        {
            if (ModelState.IsValid)
            {
                String Uniquename = ProcessorUploadFile(model);
                ////uploadd muilti img 
                //if (model.Photes != null && model.Photes.Count > 0)
                //{
                //    foreach (var item in model.Photes)
                //    {
                //        String UploadFolder = Path.Combine(_hostingEnvirnment.WebRootPath, "Image");
                //        Uniquename = Guid.NewGuid().ToString() + "_" + item.FileName;
                //        String filename = Path.Combine(UploadFolder, Uniquename);
                //        item.CopyTo(new FileStream(filename, FileMode.Create));
                //    }
                //}
                Employee employee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = Uniquename
                };
                _employeeREpository.Add(employee);
                return RedirectToAction("Details", new { id = employee.Id });

                //   return RedirectToAction("Details", new { id = employee1.Id });
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int? id)
        {
            Employee employee = _employeeREpository.GtEmployee(id ?? 1);
            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExtistinPhoto = employee.PhotoPath
            };
            return View(editEmployeeViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeREpository.GtEmployee(model.Id);
                employee.Name = model.Name;
                employee.Department = model.Department;
                employee.Email = model.Email;
                string Uniquename = ProcessorUploadFile(model);
                //if thier is modify photo in editemployeeview model
                if (model.Photes!=null)
                {
                    employee.PhotoPath = Uniquename;
                }

                if (model.ExtistinPhoto != null)
                {
                    //كده انا مسكت المسار بتاع الصوره
                    string filename = Path.Combine(_hostingEnvirnment.WebRootPath, "Image", model.ExtistinPhoto);
                    System.IO.File.Delete(filename);
                }

                //update database
                _employeeREpository.Upate(employee);

            }
            return RedirectToAction("Index");
        }

        //use CreateEmployeViewModel instrad of EditEmployeViewModel becouse CreateEmployeViewModelis the parent
        private string ProcessorUploadFile(CreateEmployeViewModel model)
        {
            string Uniquename = null;
            if (model.Photes != null && model.Photes.Count > 0)
            {
                foreach (var item in model.Photes)
                {
                    String UploadFolder = Path.Combine(_hostingEnvirnment.WebRootPath, "Image");
                    Uniquename = Guid.NewGuid().ToString() + "_" + item.FileName;
                    String filename = Path.Combine(UploadFolder, Uniquename);
                    //useing is used to when this blockexecuted immedaitly dispose
                    using (var filestream = new FileStream(filename, FileMode.Create))
                    {
                        item.CopyTo(filestream);
                    }
                }
            }

            return Uniquename;
        }
    }
}
