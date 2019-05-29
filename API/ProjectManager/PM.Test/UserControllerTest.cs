using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.Controllers;
using System.Collections.Generic;
using System.Web;
using ProjectManager.Models;
using System.Data.Entity;
using ProjectManager;

namespace PM.Test
{
    class MockProjectManagerEntities : ProjectManager.DAC.ProjectManagerEntities
    {
        private DbSet<ProjectManager.DAC.User> _users = null;
        private DbSet<ProjectManager.DAC.Project> _projects = null;
        private DbSet<ProjectManager.DAC.Task> _tasks = null;
        public override DbSet<ProjectManager.DAC.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public override DbSet<ProjectManager.DAC.Project> Projects
        {
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }

        public override DbSet<ProjectManager.DAC.Task> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
            }
        }
    }

    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void TestGetUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.GetUser() as JSonResponse;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Data, typeof(List<User>));
            Assert.AreEqual((result.Data as List<User>).Count, 2);
        }

        [TestMethod]
        public void TestInsertUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var user = new ProjectManager.Models.User();
            user.FirstName = "ankita";
            user.LastName = "ghosh";
            user.EmployeeId = "123456";
            user.UserId = 123;
            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data, 1);
        }

        [TestMethod]
        public void TestUpdateUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.FirstName = "Khush";
            user.LastName = "sarkar";
            user.EmployeeId = "123";
            user.UserId = 493210;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual((context.Users.Local[0]).First_Name.ToUpper(), "KHUSH");
        }

        [TestMethod]
        public void TestDeleteUser_Success()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.FirstName = "Prasun";
            user.LastName = "sarkar";
            user.EmployeeId = "493210";
            user.UserId = 493210;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSonResponse;

            Assert.IsNotNull(result);
            Assert.AreEqual(context.Users.Local.Count,1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDeleteUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user = null;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDeleteUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestDeleteUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.UserId = -1;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.DeleteUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user = null;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestUpdateUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestUpdateUser_NegativeUserIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.UserId = -1;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.UpdateUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInsertUser_UserNullException()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user = null;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestInsertUser_InvalidEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.EmployeeId = "TEST";

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_NegativeEmployeeId()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.EmployeeId = "-233";

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSonResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void TestInsertUser_InvalidProjectIdFormat()
        {
            var context = new MockProjectManagerEntities();
            var users = new TestDbSet<ProjectManager.DAC.User>();
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "592561",
                First_Name = "narayan",
                Last_Name = "dubey",
                Project_ID = 123,
                Task_ID = 123,
                User_ID = 592561
            });
            users.Add(new ProjectManager.DAC.User()
            {
                Employee_ID = "493210",
                First_Name = "Prasun",
                Last_Name = "sarkar",
                Project_ID = 1234,
                Task_ID = 1234,
                User_ID = 493210
            });
            context.Users = users;

            var user = new ProjectManager.Models.User();
            user.ProjectId = -1;

            var controller = new UserController(new ProjectManager.BC.UserBC(context));
            var result = controller.InsertUserDetails(user) as JSonResponse;
        }
        
    }
}

