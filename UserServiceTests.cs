using ChecklistAPI.Helpers;
using ChecklistAPI.Services;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChecklistAPI_Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task ShouldIndicateExpiry()
        {
            IOptions<AppSettings> appSettings = Options.Create<AppSettings>(new AppSettings
            {
                Secret = "Test"
            });

            var context = new SampleDBContext(useSQLLite: true).Context;
            var service = new UserService(appSettings,context);

            //var sampleToken = 


        }
    }
}
