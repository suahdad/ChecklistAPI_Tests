using EquipmentChecklistDataAccess;
using EquipmentChecklistDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ChecklistAPI_Tests
{
    public class EFCoreTest
    {
        [Fact]
        public void inMemoryDBTesting()
        {
            var builder = new DbContextOptionsBuilder<EquipmentChecklistDBContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            using (var context = new EquipmentChecklistDBContext(builder.Options))
            {
                context.Equipment_Types.Add(new Equipment_Type()
                {
                    ID = "PM"
                });

                context.SaveChanges();

                Assert.Equal(1, context.Equipment_Types.Count(x => x.ID == "PM"));

            }
        }

        [Fact]
        public void sqlLiteDBTesting()
        {
            var builder = new DbContextOptionsBuilder<EquipmentChecklistDBContext>()
                .EnableSensitiveDataLogging()
                .UseSqlite("DataSource=:memory:");

            using (var context = new EquipmentChecklistDBContext(builder.Options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Equipment_Types.Add(new Equipment_Type()
                {
                    ID = "SL"
                });

                context.SaveChanges();

                Assert.Equal(1, context.Equipment_Types.Count(x => x.ID == "SL"));

            }
        }
    }
}
