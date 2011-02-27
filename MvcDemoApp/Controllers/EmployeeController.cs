using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcDemoApp.DataService;
using MvcDemoApp.Models;

namespace MvcDemoApp.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _repository;

        public EmployeeController():this(new EmployeeRepository())
        {
           
       }

        public EmployeeController(IEmployeeRepository employeeRepository )
        {
            _repository = employeeRepository;

            //Automapper configuration to map domain entity to viewmodel entity
            AutoMapper.Mapper.CreateMap<Employee, EditEmployeeViewModel>();
            //Automaper configuration to map viewmodel entity to domain entity
            AutoMapper.Mapper.CreateMap<EditEmployeeViewModel, Employee>();
            //Type of 
            AutoMapper.Mapper.CreateMap<string, char>().ConvertUsing(x => Convert.ToChar(x));
            AutoMapper.Mapper.CreateMap<char, string>().ConvertUsing(x => Convert.ToString(x));
 
        }

        public ViewResult Index()
        {
            return View("ListEmployee", null);
        }

        //
        // GET: /Employee/

        public JsonResult GetEmployeeList()
        {
            var employeeList = _repository.GetAllEmployees();

            return Json(employeeList);
        }

        
        [HttpGet]
        public ActionResult EditEmployee(string essn)
        {
            //Get employee entity  with given ssn from the repository
            var employee = _repository.GetEmployee( essn);
            //Map domain entity with EmployeeViewModel
            var editEmployeeViewModel = AutoMapper.Mapper.Map<Employee,EditEmployeeViewModel>(employee);

            return View("EditEmployee", editEmployeeViewModel);

        }



        [HttpPost]
        public ActionResult EditEmployee(EditEmployeeViewModel editEmployeeViewModel)
        {
            if (editEmployeeViewModel == null)
                return View("Error");

            if (!ModelState.IsValid)
            {
                return View(editEmployeeViewModel);
            }
            else
            {
                var employee = _repository.GetEmployee(editEmployeeViewModel.Ssn);

                //Map ViewModel object to domain object
                employee = AutoMapper.Mapper.Map<EditEmployeeViewModel, Employee>(editEmployeeViewModel);

               
                _repository.Save();
            }


            return (View());
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult DeleteEmployee(string essn)
        {
            var employee = new Employee {Ssn = essn};
            _repository.Delete(employee);
            return RedirectToAction("Index");
        }


    }
}