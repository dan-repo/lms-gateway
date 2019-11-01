
using LmsGateway.Core.Configuration;

namespace LmsGateway.Services.Tests.MockModel
{
    public class MockSetting : ISetting
    {
        public int MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }
        public string MyProperty3 { get; set; }
        public bool MyProperty4 { get; set; }
        public decimal MyProperty5 { get; set; }
        public long MyProperty6 { get; set; }


    }
}
