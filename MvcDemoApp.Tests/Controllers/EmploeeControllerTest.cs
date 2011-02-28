using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcDemoApp.Controllers;
using MvcDemoApp.DataService;
using MvcDemoApp.Models;

namespace MvcDemoApp.Tests.Controllers
{
    [TestClass]
    public class EmploeeControllerTest
    {
        private IGenerationSessionFactory _generationSessionFactory;



        [TestInitialize]
        public void Setup()
        {

        }




        [TestMethod]
        public void IndexAction_Should_Return_View_For_ListEmployee()
        {
            var testRepository = new TestRepository();
            var employeeController = new EmployeeController(testRepository);
            var result = employeeController.Index();
            Assert.IsNotNull(result);
            Assert.AreEqual("ListEmployee", result.ViewName);
        }


        [TestMethod]
        public void GetEmployeeList_Should_Return_List_Of_Employee()
        {
            var testRepository = new TestRepository();
            var employeeController = new EmployeeController(testRepository);
            var result = employeeController.GetEmployeeList();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOfType(result.Data, typeof(IQueryable<Employee>));
            Assert.AreEqual(((IQueryable<Employee>)(result.Data)).Count( ), 2);
            Assert.AreEqual(((IQueryable<Employee>)(result.Data)).First().Fname, "John");
        }



        [TestMethod]
        public void EditEmployee_Should_Return_EditEmployee_View_With_Populated_View_Models()
        {
            var testRepository = new TestRepository();
            var employeeController = new EmployeeController(testRepository);
            var result = employeeController.EditEmployee("123-321-1234");
            Assert.IsNotNull(result);   
            Assert.IsInstanceOfType(result.ViewData.Model,typeof(EditEmployeeViewModel) );
            Assert.AreEqual(((EditEmployeeViewModel)result.ViewData.Model).Fname, "John");
            Assert.AreEqual(((EditEmployeeViewModel)result.ViewData.Model).Lname, "Doe");

        }

        [TestMethod]
        public void EditEmployee_Post_Should_Return_EditEmployee_View_With_Required_Data_Missing()
        {

            var employee = new EditEmployeeViewModel {Fname = "Ray", Lname = "Romano"/*, Ssn = "123-09-3214"*/};
            var validationContext = new ValidationContext(employee, null, null);
            var testRepository = new TestRepository();
 
            var employeeController = new EmployeeController(testRepository);

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(employee, validationContext, validationResults);
            foreach (var validationResult in validationResults)
            {
                employeeController.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            var result = employeeController.EditEmployee(employee);

            Assert.IsNotNull(result);

            Assert.AreEqual( "EditEmployee", ((ViewResult)result).ViewName  );
        }


        [TestMethod]
        public void EditEmployee_Post_Should_Return_EditEmployee_View_When_Invalid_Ssn()
        {
            var employee = new EditEmployeeViewModel { Fname = "Ray", Lname = "Romano", Ssn = "ABC-09"};
            var validationContext = new ValidationContext(employee, null, null);
            var testRepository = new TestRepository();

            var employeeController = new EmployeeController(testRepository);

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(employee, validationContext, validationResults);
            foreach (var validationResult in validationResults)
            {
                employeeController.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            var result = employeeController.EditEmployee(employee);

            Assert.IsNotNull(result);

            Assert.AreEqual("EditEmployee", ((ViewResult)result).ViewName);

        }
    }
}
