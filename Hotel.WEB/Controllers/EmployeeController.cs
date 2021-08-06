using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.WEB.Helpers;
using Hotel.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel.WEB.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService service;
        private IMapper mapperEmployee;
        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
            mapperEmployee = new MapperConfiguration(cfg =>
              cfg.CreateMap<EmployeeDTO, EmployeeModel>()).CreateMapper();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeModel employee = mapperEmployee
                    .Map<EmployeeDTO,EmployeeModel>(service.GetAll().FirstOrDefault(e => e.Login == model.Login && e.Password == Crypto.Hash(model.Password)));
                if (employee != null && Crypto.Hash(model.Password) == employee.Password)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index","Customer");
                }
                else
                {
                    ModelState.AddModelError("", "User not found");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
               if(model.Password == model.RepeatPassword)
               {
                    EmployeeModel employee = mapperEmployee
                    .Map<EmployeeDTO, EmployeeModel>(service.GetAll().FirstOrDefault(e => e.Login == model.Login));

                    if(employee == null)
                    {
                        service.Create(new EmployeeDTO()
                        {
                            Login = model.Login,
                            Password = model.HashedPassword
                        });

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User with such login already exist");
                    }
               }
                
            return View(model);
        }
    }
}