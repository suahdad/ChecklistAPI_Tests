using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChecklistAPI.Controllers;
using EquipmentChecklistDataAccess.Models;
using Xunit;

namespace ChecklistAPI_Tests
{
    public class ChecklistsControllerTests
    {
        [Fact]
        public async Task ShouldGetChecklistWithIssues()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TYRES",
                        ConditionID="OTHER"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            HashSet<Checklist_Item> checklistItems2 = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TYRES",
                        ConditionID="OK"
                    }
                };

            Checklist checklist2 = new Checklist()
            {
                Checklist_Items = checklistItems2,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);
            await controller.PostChecklist(checklist);
            await controller.PostChecklist(checklist2);
            var test = await controller.GetChecklistIssues();

            Assert.Equal(1, test.Value.Count(x => x.UserID == "TEST"));
        }

        [Fact]
        public async Task ShouldGetChecklist()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TYRES",
                        ConditionID="OTHER"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            HashSet<Checklist_Item> checklistItems2 = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TYRES",
                        ConditionID="OK"
                    }
                };

            Checklist checklist2 = new Checklist()
            {
                Checklist_Items = checklistItems2,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);
            await controller.PostChecklist(checklist);
            await controller.PostChecklist(checklist2);
            var test = await controller.GetChecklists();

            Assert.Equal(2, test.Value.Count(x => x.UserID == "TEST"));
        }


        [Fact]
        public async Task ShouldSave_ifValid_EquipmentEquipmentTypeConditionComponent()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TYRES",
                        ConditionID="OK"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);

            await controller.PostChecklist(checklist);

            Assert.Equal(1, context.Checklists.Count(x => x.UserID == "TEST"));
        }

        [Fact]
        public async Task ShouldNotSave_ifDifferentEquipments()
        {
            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
            {
                new Checklist_Item()
                {
                    Equipment_TypeID="PM",
                    ComponentID="TYRES",
                    ConditionID="OK"
                }
            };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "SL01",
                UserID = "TEST"
            };


            await controller.PostChecklist(checklist);

            Assert.Equal(0, context.Checklists.Count(x => x.UserID == "TEST"));
        }

        [Fact]
        public async Task ShouldNotSave_ifInvalidEquipment()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TYRES",
                        ConditionID="OK"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "TEST",
                UserID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);

            await controller.PostChecklist(checklist);

            Assert.Equal(0, context.Checklists.Count(x => x.UserID == "TEST"));
        }

        [Fact]
        public async Task ShouldNotSave_ifInvalidEquipmentType()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="TEST",
                        ComponentID="TYRES",
                        ConditionID="OK"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);

            await controller.PostChecklist(checklist);

            Assert.Equal(0, context.Checklists.Count(x => x.UserID == "TEST"));
        }

        [Fact]
        public async Task ShouldNotSave_ifInvalidCondition()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TYRES",
                        ConditionID="TEST"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);

            await controller.PostChecklist(checklist);

            Assert.Equal(0, context.Checklists.Count(x => x.UserID == "TEST"));
        }

        [Fact]
        public async Task ShouldNotSave_ifInvalidComponent()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TEST",
                        ConditionID="OK"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);

            await controller.PostChecklist(checklist);

            Assert.Equal(0, context.Checklists.Count(x => x.UserID == "TEST"));
        }

        [Fact]
        public async Task ShouldNotSaveifInvalidUser()
        {

            HashSet<Checklist_Item> checklistItems = new HashSet<Checklist_Item>
                {
                    new Checklist_Item()
                    {
                        Equipment_TypeID="PM",
                        ComponentID="TEST",
                        ConditionID="OK"
                    }
                };

            Checklist checklist = new Checklist()
            {
                Checklist_Items = checklistItems,
                Date_Created = new DateTime(),
                EquipmentID = "PM01",
                UserID = "TEST1"
            };

            var context = new SampleDBContext(useSQLLite: true).Context;
            var controller = new ChecklistsController(context);

            await controller.PostChecklist(checklist);

            Assert.Equal(0, context.Checklists.Count(x => x.UserID == "TEST"));
        }

    }
}
