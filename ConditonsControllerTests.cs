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
    public class ConditonsControllerTests
    {
        [Fact]
        public async Task ShouldSave_IfConditionID_HasNoDescriptionOrShortname()
        {
            var condition = new Condition
            {
               ID = "ABCDE"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ConditionsController(context);

            await controller.PostCondition(condition);

            Assert.Equal(1, context.Conditions.Count(x => x.ID == "ABCDE"));

        }

        [Fact]
        public async Task ShouldNotSave_IfConditionID_HasMoreThan5Characters()
        {
            var condition = new Condition
            {
                ID = "ABCDEF"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ConditionsController(context);

            await controller.PostCondition(condition);

            Assert.Equal(0, context.Conditions.Count(x => x.ID == "ABCDEF"));

        }
    }
}
