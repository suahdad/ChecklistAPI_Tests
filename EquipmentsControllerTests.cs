using ChecklistAPI.Controllers;
using EquipmentChecklistDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChecklistAPI_Tests
{
    public class EquipmentsControllerTests
    {
        [Fact]
        public async Task ShouldSave_EvenIfEquipmentID_HasNoShortname()
        {
            var Equipment = new Equipment
            {
                ID = "ABCDE",
                Equipment_TypeID="PM"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new EquipmentsController(context);

            await controller.PostEquipment(Equipment);

            Assert.Equal(1, context.Equipments.Count(x => x.ID == "ABCDE"));

        }

        [Fact]
        public async Task ShouldThrowException_IfEquipmentTypeID_IsNullOrEmpty()
        {
            var Equipment = new Equipment
            {
                ID = "ABCDE"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new EquipmentsController(context);

            var result = await controller.PostEquipment(Equipment);
            Assert.Contains("null or empty", ((Exception)result.Value).Message);

        }

        [Fact]
        public async Task ShouldThrowException_IfEquipmentTypeID_DoesNotExist()
        {
            var Equipment = new Equipment
            {
                ID = "ABCDE",
                Equipment_TypeID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new EquipmentsController(context);

            var result = await controller.PostEquipment(Equipment);
            Assert.Contains("FOREIGN KEY", ((Exception)result.Value).InnerException.Message);

        }



        [Fact]
        public async Task ShouldThrowException_IfEquipmentID_HasMoreThan5Characters()
        {
            var Equipment = new Equipment
            {
                ID = "ABCDEF",
                Equipment_TypeID="PM"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new EquipmentsController(context);


            var result = await controller.PostEquipment(Equipment);
            Assert.Contains("has more than 5 characters", ((Exception)result.Value).Message);

        }

        [Fact]
        public async Task ShouldDelete_IfEquipmentIDIsUnused()
        {
            var Equipment = new Equipment
            {
                ID = "ABCDE",
                Equipment_TypeID = "PM"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new EquipmentsController(context);

            await controller.PostEquipment(Equipment);

            //var result = await controller.PutEquipment("PM01",Equipment);
            //Assert.Contains("has more than 5 characters", ((Exception)result.Value).Message);

            await controller.DeleteEquipment("ABCDE");
            Assert.Equal(0, context.Equipments.Count(x => x.ID == "ABCDE"));
        }

        [Fact]
        public async Task ShouldNotDelete_IfEquipmentIDIsUsed()
        {
            var Equipment = new Equipment
            {
                ID = "ABCDF",
                Equipment_TypeID = "PM"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new EquipmentsController(context);


            //var result = await controller.PutEquipment("PM01",Equipment);
            //Assert.Contains("has more than 5 characters", ((Exception)result.Value).Message);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => controller.DeleteEquipment("ABCDE"));
        }
    }
}
