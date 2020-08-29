using ChecklistAPI.Controllers;
using ChecklistAPI.Helpers;
using ChecklistAPI.Services;
using EquipmentChecklistDataAccess.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChecklistAPI_Tests
{
    public class AdminsControllerTests
    {
        [Fact]
        public async Task ShouldExistAdminAfterSaving()
        {
            User sampleUser = new User
            {
                ID = "12345",
                FirstName = "test",
                MiddleName = "test",
                LastName = "test",
                Password = "test",
                isActive = true,

            };

            Admin sampleAdmin = new Admin
            {
                User = sampleUser
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new AdminsController(context);


            await controller.PostAdmin(sampleAdmin);

            Assert.Equal(1, context.Admins.Count(x => x.UserID == "12345"));
        }

        [Fact]
        public async Task ShouldExistUserAfterSaving()
        {
            User sampleUser = new User
            {
                ID = "12345",
                FirstName = "test",
                MiddleName = "test",
                LastName = "test",
                Password = "test",
                isActive = true,

            };

            Admin sampleAdmin = new Admin
            {
                User = sampleUser
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new AdminsController(context);


            await controller.PostAdmin(sampleAdmin);

            Assert.Equal(1, context.Users.Count(x => x.ID == "12345"));
        }
    }
}
