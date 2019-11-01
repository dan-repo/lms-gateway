using System.Collections.Generic;
using LmsGateway.Domain.Configuration;
using LmsGateway.Core.Configuration;
using System.Threading.Tasks;

namespace LmsGateway.Core.Configuration
{
    public interface ISettingService
    {
        List<Setting> GetAllSettings();
        Task<T> GetSetting<T>() where T : ISetting, new();
        Task SaveSetting<T>(T setting) where T : ISetting, new();
        Task UpdateSetting<T>(T setting) where T : ISetting, new();
        void DeleteSetting<T>() where T : ISetting, new();




        //void DeleteSetting<T>() where T : ISetting, new();
        //void SaveSetting<T>(T setting) where T : ISetting, new();
        //void UpdateSetting<T>(T setting) where T : ISetting, new();
        //T GetSetting<T>() where T : ISetting, new();
        //IList<Setting> GetAllSettings();

    }



}
