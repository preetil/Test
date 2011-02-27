using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoPoco;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcDemoApp.Controllers;
using MvcDemoApp.DataService;

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
    }
}
