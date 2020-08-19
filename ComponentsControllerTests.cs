using ChecklistAPI.Controllers;
using EquipmentChecklistDataAccess.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChecklistAPI_Tests
{
    public class ComponentsControllerTests
    {
        [Fact]
        public async Task ShouldSave_IfComponentID_HasNoDescriptionOrShortname()
        {
            var component = new Component
            {
                ID = "ABCDE"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ComponentsController(context);

            await controller.PostComponent(component);

            Assert.Equal(1, context.Components.Count(x => x.ID == "ABCDE"));

        }

        [Fact]
        public async Task ShouldNotSave_IfComponentID_HasMoreThan5Characters()
        {
            var component = new Component
            {
                ID = "ABCDEF"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ComponentsController(context);

            await controller.PostComponent(component);

            Assert.Equal(0, context.Components.Count(x => x.ID == "ABCDEF"));

        }

    }
}
