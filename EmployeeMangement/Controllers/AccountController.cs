using EmployeeMangement.Data;
using EmployeeMangement.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Controllers
{
    public class AccountController : Controller
    {
       readonly private UserManager<ApplicationUser> _userManager;
       readonly private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //remote validation after leve text show if email exist or not
        [AcceptVerbs("Get","Post")]
     public async Task< IActionResult> IsEmailInUsed(string Email)
        {
            var result = await _userManager.FindByNameAsync(Email);
            if (result==null)
            {
                return Json(true);
            }
            else
            {
                return Json("this  erroe becouse this email is exist");
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Register(RegisterUserViewModel model )
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City
                };
              var result= await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if  ( _signInManager.IsSignedIn(User )&&User.IsInRole("admin"))
                    {
                        return RedirectToAction("ListUser","administration");
                    }
                    //to type isperstent:true  perstent stall after close brwser
                    //to type isperstent:fale  sission  end  after close brwser
                    await _signInManager.SignInAsync(user, isPersistent: false);


                    return RedirectToAction("index", "Home");
                }
                //else if result ==false
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> Logout()
        {
            //used to sign out
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task < IActionResult> Login(LoginUserViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RemmberMe, true);
                if (result.Succeeded)
                {
                 
                    if (ReturnUrl!=null)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "invaild");
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
