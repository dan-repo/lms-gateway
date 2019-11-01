using LmsGateway.Core.Configuration;
using LmsGateway.Core.Data;
using LmsGateway.Data;
using LmsGateway.Domain.Configuration;
using LmsGateway.Services.Configuration;
using LmsGateway.Services.Tests.MockModel;
using LmsGateway.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LmsGateway.Services.Tests
{
    public class SettingServiceTest
    {
        private MockSetting _mockSetting;
        private ISettingService _settingService;
        private IRepository<Setting> _repository;

        public SettingServiceTest()
        {
            var dbContext = Db.GetDataContext();
            _repository = new EFRepository<Setting>(dbContext);
            _settingService = new SettingService(_repository);
        }

        [Fact] //1
        public async Task SaveSetting_Should_SaveSettingSuccessfully_WhenSettingObjectIsSet()
        {
            // arrange
            CreateDiscountSetting();
            _repository.Delete(x => x.Name.StartsWith("MockSetting"));

            // act
            await _settingService.SaveSetting<MockSetting>(_mockSetting);
            var settings = await _repository.FindByAsync(x => x.Name.StartsWith("MockSetting"));

            // assert
            AssertHelper(_mockSetting, settings);
        }

        [Fact] //2
        public async Task SaveSetting_ShouldThrowArgumentNullException_WhenSpecifiedSettingIsNull()
        {
            // act
            Func<Task> result = async () => await _settingService.SaveSetting<MockSetting>(null);

            // assert
            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Fact] //3
        public async Task SaveSetting_ShouldSuccessfullySaveSetting_WhenSpecifiedSettingIsEmpty()
        {
            var setting = new MockSetting();
            _repository.Delete(x => x.Name.StartsWith("MockSetting"));

            // act
            await _settingService.SaveSetting<MockSetting>(setting);
            var settings = await _repository.FindByAsync(x => x.Name.StartsWith("MockSetting"));

            // assert
            AssertHelper(setting, settings);
        }

        [Fact] //4
        public async Task UpdateSetting_ShouldSuccessfullyUpdateSetting_WhenSpecifiedSettingIsModified()
        {
            await SaveDiscountSetting();
            var setting = new MockSetting() { MyProperty2 = "Testing ...", MyProperty5 = 990.89M, MyProperty6 = 21200000 };

            // act
            await _settingService.UpdateSetting<MockSetting>(setting);
            var settings = await _repository.FindByAsync(x => x.Name.StartsWith("MockSetting"));

            AssertHelper(setting, settings);
        }

        [Fact] //5
        public async Task UpdateSetting_ShouldThrowException_WhenSpecifiedSettingIsNotModified()
        {
            _repository.Delete(x => x.Name.StartsWith("MockSetting"));
            await SaveDiscountSetting();

            // act
            Func<Task> result = async() => await _settingService.UpdateSetting<MockSetting>(_mockSetting);

            // assert
            await Assert.ThrowsAsync<Exception>(result);
        }

        [Fact] //6
        public async Task UpdateSetting_ShouldThrowException_WhenSpecifiedSettingIsNull()
        {
            await SaveDiscountSetting();

            Func<Task> result = async () => await _settingService.UpdateSetting<MockSetting>(null);

            await Assert.ThrowsAsync<ArgumentNullException>(result);
        }

        [Fact] //7
        public async Task DeleteSetting_Should_DeleteSpecificSettingSuccessfully_WhenSettingObjectIsSet()
        {
            // arrange
            await SaveDiscountSetting();

            // act
            _repository.Delete(x => x.Name.StartsWith("MockSetting"));
            var settings = await _repository.FindByAsync(x => x.Name.StartsWith("MockSetting"));

            // assert
            Assert.True(settings == null || settings.Count <= 0);
        }

        [Fact] //8
        public async Task GetSetting_Should_GetSpecificSettingSuccessfully_WhenSettingTypeIsSet()
        {
            // arrange
            await SaveDiscountSetting();

            // act
            var setting = await _settingService.GetSetting<MockSetting>();

            // assert
            Assert.True(setting != null);
            Assert.Equal(_mockSetting.MyProperty3, setting.MyProperty3);
        }

        [Fact] //9
        public async Task GetSetting_Should_ReturnNull_WhenSpecifiedSettingDoesNotExist()
        {
            //_settingService.DeleteSetting<MockSetting>();
            _repository.Delete(x => x.Name.StartsWith("MockSetting"));

            var setting = await _settingService.GetSetting<MockSetting>();

            Assert.True(setting == null);
        }
        
        private async Task SaveDiscountSetting()
        {
            bool alreadyExist = await SettingAlreadyExist();
            if (alreadyExist)
            {
                return;
            }

            CreateDiscountSetting();

            await _settingService.SaveSetting<MockSetting>(_mockSetting);
        }

        private async Task<bool> SettingAlreadyExist()
        {
            var settings = await _repository.FindByAsync(x => x.Name.StartsWith("MockSetting"));

            return settings != null && settings.Any() ? true : false;
        }

        // assert
        private void AssertHelper(MockSetting setting, List<Setting> settings)
        {
            Assert.True(settings != null);
            Assert.True(settings.Count > 0);
            Assert.True(settings[2].Id > 0);
            //Assert.Equal(3, settings[2].Id);
            Assert.Equal(setting.MyProperty3, settings[2].Value);
        }

        private void CreateDiscountSetting()
        {
            _mockSetting = new MockSetting()
            {
                MyProperty1 = 90,
                MyProperty2 = "",
                MyProperty3 = "twenty Percentage of price",
                MyProperty4 = true,
                MyProperty5 = 9000.04M,
                MyProperty6 = 9009890,
            };
        }







        //[Fact]
        //public void Test()
        //{
        //    string userid = "username";
        //    string password = "password";
        //    string credential = string.Format("{0}:{1}", userid, password);
        //    //string credential = userid + ":" + password;


        //    // encode
        //    byte[] credentialBytes = Encoding.UTF8.GetBytes(credential);
        //    string credentialHash = Convert.ToBase64String(credentialBytes);

        //    // decode
        //    byte[] normalByte = Convert.FromBase64String(credentialHash);
        //    string normalString = Encoding.UTF8.GetString(normalByte);

        //    Assert.True(normalString != null);
        //}

        //[Fact]
        //public void Test2()
        //{
        //    int a = 29;
        //    a--;
        //    //int t = a;
        //    //int y = ++a;
        //    a -= ++a;
        //    //a = a - ++a;

        //    Assert.Equal(-1, a);

        //    Tuple<int, string> t = new Tuple<int, string>(1, "");

        //    //Complex c1 = new Complex(5, 8); /* It represents (5, 8) */


        //    //System.Numeric.BigInteger;
        //    //System.IO.MemoryMappedFiles.

        //    //try
        //    //{
        //    //    int? x = null;
        //    //    int y = x ?? 0;
        //    //    System.ValueType
        //    //    Assert.Equal(y, x);
        //    //}
        //}







    }
}
