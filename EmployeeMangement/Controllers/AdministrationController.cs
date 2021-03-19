using EmployeeMangement.Data;
using EmployeeMangement.ViewModel;
using EmployeeMangement.ViewModel.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace EmployeeMangement.Controllers
{
    // [Authorize(Roles ="admin,user")]
    [Authorize(Roles = "admin")]
    //[Authorize(Roles ="user")]
    public class AdministrationController : Controller
    {
        readonly private RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdministrationController> _loger;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> usermanager, ILogger<AdministrationController> logger)
        {
            _roleManager = roleManager;
            _userManager = usermanager;
            _loger = logger;
        }
        /// <summary>
        /// ///////////////role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        //create role
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var Result = await _roleManager.CreateAsync(Role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");
                }
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }
        //retun all role
        [HttpGet]
        public IActionResult ListRole()
        {
            var Result = _roleManager.Roles;
            return View(Result);
        }
        //  edit role
        //
        [HttpGet]
        public async Task<IActionResult> Editrole(string id)
        {
            var roleresult = await _roleManager.FindByIdAsync(id);
            if (roleresult == null)
            {
                return RedirectToAction("error", "error");
            }
            EditRoleVIewModel model = new EditRoleVIewModel()
            {
                ID = roleresult.Id,
                RoleName = roleresult.Name
            };
            foreach (var item in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(item, model.RoleName))
                {
                    model.Users.Add(item.UserName);
                }
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVIewModel model)
        {
            var Role = await _roleManager.FindByIdAsync(model.ID);
            if (Role == null)
            {
                return RedirectToAction("Error", "Error");
            }
            Role.Name = model.RoleName;
            var Result = await _roleManager.UpdateAsync(Role);
            if (Result.Succeeded)
            {
                return RedirectToAction("ListRole", "Administration");
            }
            foreach (var item in Result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View(model);
        }

        //add user to role
        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string RoleId)
        {
            var Role = await _roleManager.FindByIdAsync(RoleId);
            if (Role == null)
            {
                return RedirectToAction("Error", "Error");
            }
            var Users = _userManager.Users;
            var model = new List<AddUserToRoleViewModel>();
            foreach (var item in Users)
            {
                AddUserToRoleViewModel addUser = new AddUserToRoleViewModel()
                {
                    RoleId = RoleId,
                    UserId = item.Id,
                    UserName = item.UserName
                };
                if (await _userManager.IsInRoleAsync(item, Role.Name))
                {
                    addUser.IsSelected = true;
                }
                else
                    addUser.IsSelected = false;
                model.Add(addUser);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(IList<AddUserToRoleViewModel> models, string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                return RedirectToAction("Error", "Error");
            }


            for (int i = 0; i < models.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(models[i].UserId);
                IdentityResult result = null;
                if (models[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!(models[i].IsSelected) && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (models.Count - 1))
                    {
                        continue;
                    }
                    else
                        return RedirectToAction("EditRole", new { id = RoleId });
                }
            }
            //foreach (var item in models)
            //{
            //    IdentityResult result = null;
            //    var user = await _userManager.FindByIdAsync(item.UserId);
            //    if (item.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
            //    {
            //        result = await _userManager.AddToRoleAsync(user, role.Name);
            //    }
            //    else if (!item.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
            //    {
            //        result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}
            return RedirectToAction("EditRole", new { id = RoleId });
        }
        //////////edit user
        ///
        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("Error", "Error");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            //t
            EditUserViewModel model = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                City = user.City,
                //use linq to save claaims
                Claims = claims.Select(e => e.Value).ToList(),
                Roles = roles.ToList()
            };
            return View(model);
        }

        ///
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return RedirectToAction("Error", "Error");
                }
                user.UserName = model.Name;
                user.Email = model.Email;
                user.City = model.City;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUser", "Administration");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }
        /// 
        /// 
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("ListUser", "Administration");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUser", "Administration");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View();
        }
        /////////////////////////////////////////////////////////// <user>/////////////////////////
        /// 
        /// </summary>
        public IActionResult ListUser()
        {
            return View(_userManager.Users);
        }

        //////add role
        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            var Role = await _roleManager.FindByIdAsync(RoleId);
            if (Role == null)
            {
                RedirectToAction("Error", "Error");
            }
            try
            {
                var Result = await _roleManager.DeleteAsync(Role);
                if (Result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                else
                {
                    foreach (var item in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    return View("ListRole");
                }
            }
            //not uses expetion becouse it generic it use for all exception and i wnat to custom this error
            catch (DbUpdateException ex)
            {
                _loger.LogError($"Error deleted role {ex}");
                ViewBag.ErrorTitle = $"{Role.Name} is used role ";
                ViewBag.ErrorMessage = $"ant delete this {Role.Name}";
                return RedirectToAction("Error", "Error");
            }

        }
        [HttpGet]
        public async Task<IActionResult> AddRoleToUser(string UserId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    return RedirectToAction("Erorr", "Error");
                }
                List<AddUserToRoleViewModel> roles = new List<AddUserToRoleViewModel>();
                foreach (var item in _roleManager.Roles)
                {
                    roles.Add(new AddUserToRoleViewModel()
                    {
                        RoleId = item.Id,
                        UserId = user.Id,
                        UserName = user.UserName,
                        RoleName = item.Name
                    });
                    if (await _userManager.IsInRoleAsync(user, item.Name))
                    {
                        roles.Last().IsSelected = true;
                    }
                }
                return View(roles);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(List<AddUserToRoleViewModel> models)
        {
            var user = await _userManager.FindByIdAsync(models.Select(e => e.UserId).FirstOrDefault());
            foreach (var item in models)
            {
                if (!(item.IsSelected) && await _userManager.IsInRoleAsync(user, item.RoleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
                else if (item.IsSelected && !(await _userManager.IsInRoleAsync(user, item.RoleName)))
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("EditUser", new { Id = user.Id });
        }

        /////////////////////////////////////////
        ///claims
        //mange  claims to user
        [HttpGet]
        public async Task<IActionResult> MangClaimsToUser(String UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return RedirectToAction("Error", "Error");
            }
            //get all claims
            var ExistClaims = ConfigClaims.AllClaims();
            var claimsInUser = await _userManager.GetClaimsAsync(user);
            //define of user calms view model contain user id an claims list
            UserClaimsViewModel model = new UserClaimsViewModel();
            model.UserId = UserId;
            foreach (var item in ExistClaims)
            {
                CustomerClaims customerClaims = new CustomerClaims();
                customerClaims.ClaimsType = item.Type;
                if (claimsInUser.Any(e => e.Value == item.Type))
                {
                    customerClaims.IsSelected = true;
                }
                model.UserClaims.Add(customerClaims);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> MangClaimsToUser(UserClaimsViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return RedirectToAction("Error", "Error");
            }
            //remove all claim (that in user or not)from user
            // var claimsRemovie= await _userManager.RemoveClaimsAsync(user, ConfigClaims.AllClaims());
            //remove allclaims that in user
            var claimsinuser = await _userManager.GetClaimsAsync(user);
            var claimsRemovi = await _userManager.RemoveClaimsAsync(user, claimsinuser);
            IdentityResult claimsAdd;
            foreach (var item in model.UserClaims)
            {
                if (item.IsSelected)
                {
                    claimsAdd = await _userManager.AddClaimAsync(user, new Claim(item.ClaimsType, item.ClaimsType));
                    continue;
                }
            }
            return RedirectToAction("EditUser", new { Id =model.UserId});
        }

    }
}



