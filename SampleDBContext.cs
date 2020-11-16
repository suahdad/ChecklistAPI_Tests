
using EquipmentChecklistDataAccess;
using EquipmentChecklistDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace ChecklistAPI_Tests
{
    public class SampleDBContext
    {
        public SampleDBContext(bool useSQLLite)
        {
            if(useSQLLite)this.UseSqlite();
            this.Context = GetDbContext().Result;
            Seed();
        }
        public EquipmentChecklistDBContext Context { get; private set; }

        private bool _useSqlite;

        public void UseSqlite()
        {
            _useSqlite = true;
        }

        public async Task<EquipmentChecklistDBContext> GetDbContext()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            if (_useSqlite)
            {
                // Use Sqlite DB.
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                // Use In-Memory DB.
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).ConfigureWarnings(w =>
                {
                    w.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                })
                .EnableSensitiveDataLogging();
                
            }

            var dbContext = new EquipmentChecklistDBContext(builder.Options);
            if (_useSqlite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database and MS SQL.
                await dbContext.Database.OpenConnectionAsync();
            }

            await dbContext.Database.EnsureCreatedAsync();

            return dbContext;
        }

        private void Seed()
        {
            User user = new User()
                {
                    ID = "TEST",
                    FirstName = "John",
                    MiddleName = "A.",
                    LastName = "Smith",
                    Password = "password"
                };

            HashSet<Condition> condition = new HashSet<Condition>
            {
                new Condition()
                {
                    ID = "OK"
                },
                new Condition()
                {
                    ID = "OTHER"
                }
            };

            Component component = new Component()
            {
                ID = "TYRES"
            };

            Equipment[] equipment = new Equipment[]
            {
                new Equipment
                {        
                    Equipment_TypeID = "PM",
                    ID = "PM01"
                }, 
                new Equipment
                {
                    Equipment_TypeID = "SL",
                    ID = "SL01"
                }
            };


            Equipment_Type[] equipmentType = new Equipment_Type[]
            {
                new Equipment_Type
                {
                    ID = "PM"
                },
                new Equipment_Type
                {
                    ID="SL"
                }
            };

            Question Question = new Question()
            {
                Equipment_TypeID = "PM",
                ComponentID = "TYRES",
            };

            Context.Users.Add(user);
            Context.Components.AddRange(component);
            Context.Conditions.AddRange(condition);
            Context.Equipments.AddRange(equipment);
            Context.Equipment_Types.AddRange(equipmentType);
            Context.Questions.Add(Question);
            
        }


    }
}
