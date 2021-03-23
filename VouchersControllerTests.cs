using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using EquipmentChecklistDataAccess.Models;
using ChecklistAPI.Controllers;

namespace ChecklistAPI_Tests
{
    public class VouchersControllerTests
    {
        [Fact]
        public async Task ShouldGetUserById()
        {
            var _voucher = new Voucher()
            {
                EquipmentID = "PM01",
                UserID = "TEST",
                Validity = DateTime.Now.AddHours(12)
            };

            var _context = new SampleDBContext(useSQLLite: true).Context;
            var _controller = new VouchersController(_context);

            _context.Vouchers.Add(_voucher);
            _context.SaveChanges();

            var _result = _controller.GetVoucher("TEST").Result.Value.ToArray();

            Assert.Equal("TEST", _result[0].UserID); 
        }
        [Fact]
        public async Task ShouldPostVoucher()
        {
            var _voucher = new Voucher()
            {
                EquipmentID = "PM01",
                UserID = "TEST"
            };

            var _context = new SampleDBContext(useSQLLite: true).Context;
            var _controller = new VouchersController(_context);

            _context.Vouchers.Add(_voucher);
            _context.SaveChanges();

            await _controller.PostVoucher(new Voucher() {
                EquipmentID = "PM01",
                UserID = "TEST"
            });
            var _result = _controller.GetVouchers().Result.Value.ToArray();

            Assert.NotEqual(_voucher.Validity, ((Voucher)_result[0]).Validity);
        }

        [Fact]
        public async Task ShouldDeleteRelatedVouchersAtPost()
        {
            var _vouchers = new Voucher[] {
                new Voucher
                {
                    EquipmentID = "PM01",
                    UserID = "TEST"
                },
                new Voucher
                {
                    EquipmentID = "SL01",
                    UserID = "TEST"
                }

            };

            var _context = new SampleDBContext(useSQLLite: true).Context;
            var _controller = new VouchersController(_context);

            _context.Vouchers.AddRange(_vouchers);
            _context.SaveChanges();

            await _controller.PostVoucher(new Voucher()
            {
                EquipmentID = "PM01",
                UserID = "TEST"
            });
            var _result = _controller.GetVouchers().Result.Value.ToArray();

            Assert.Single(_result);
        }
    }
}