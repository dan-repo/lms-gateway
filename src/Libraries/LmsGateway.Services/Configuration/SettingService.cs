using System;
using System.Linq;
using LmsGateway.Core.Configuration;
using LmsGateway.Core.Data;
using LmsGateway.Core.Infrastructure;
using LmsGateway.Domain.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LmsGateway.Services.Configuration
{
    public class SettingService : ISettingService
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(IRepository<Setting> settingRepository)
        {
            Guard.NotNull(settingRepository, nameof(settingRepository));

            _settingRepository = settingRepository;
        }

        public List<Setting> GetAllSettings()
        {
            return _settingRepository.GetAll();
        }

        public async Task<T> GetSetting<T>() where T : ISetting, new()
        {
            string typeName = GetClassName(typeof(T));

            IList<Setting> settings = await _settingRepository.FindByAsync(x => x.Name.StartsWith(typeName));
            if (settings == null || settings.Count <= 0)
            {
                return default(T);
            }

            T setting = Activator.CreateInstance<T>();
            foreach (Setting dbSetting in settings)
            {
                string fullName = dbSetting.Name;
                string propertyName = fullName.Split('.')[1];
                string value = dbSetting.Value;

                Type type = setting.GetType();
                PropertyInfo propName = type.GetProperty(propertyName);
                PropertyInfo propertyInfo = setting.GetType().GetProperty(propertyName);

                try
                {
                    setting.GetType().GetProperty(propertyName).SetValue(setting, Convert.ChangeType(value, propertyInfo.PropertyType));
                }
                catch(Exception ex)
                {
                    string s = ex.ToString();
                }
            }

            return setting;
        }

        public async Task SaveSetting<T>(T setting) where T : ISetting, new()
        {
            Guard.NotNull(setting, nameof(setting));

            bool settingExist = await SettingExist<T>();
            if (settingExist)
            {
                throw new Exception("Setting already exist! Please update setting instead.");
            }

            List<Setting> settings = new List<Setting>();
            PropertyInfo[] propertyInfo = setting.GetType().GetProperties();

            foreach (var property in propertyInfo)
            {
                string name = GetName(property);
                object rawValue = property.GetValue(setting, null);
                string value = rawValue == null ? null : rawValue.ToString();

                Setting newSetting = new Setting() { Name = name, Value = value };
                settings.Add(newSetting);
            }

            await _settingRepository.AddRangeAsync(settings);
            //_settingRepository.Save();
        }

        public async Task UpdateSetting<T>(T setting) where T : ISetting, new()
        {
            Guard.NotNull(setting, nameof(setting));
            
            T existingSetting = await GetSetting<T>();
            if (existingSetting == null)
            {
                throw new Exception("Current setting does not exist!");
            }

            // setting exist, then compare both settings
            PropertyInfo[] propertyInfo = setting.GetType().GetProperties();
            PropertyInfo[] existingPropertyInfo = existingSetting.GetType().GetProperties();
            if (existingPropertyInfo.Length != propertyInfo.Length)
            {
                using (var transaction = _settingRepository.DbContext.Database.BeginTransaction())
                {
                    DeleteSetting<T>();
                    await SaveSetting<T>(setting);

                    transaction.Commit();
                }

                return;
            }

            int noOfChanges = 0;
            foreach (var property in propertyInfo)
            {
                object rawValue = property.GetValue(setting, null);
                string value = rawValue == null ? null : rawValue.ToString();

                object existingRawValue = property.GetValue(existingSetting, null);
                string existingValue = existingRawValue == null ? null : existingRawValue.ToString();

                if (value != existingValue)
                {
                    noOfChanges++;
                }
            }

            if (noOfChanges == 0)
            {
                throw new Exception("No changes detected! The current and existing setting are the same. Kindly modify before updating.");
            }

            await UpdateSettingHelper(setting, existingPropertyInfo);
            //_settingRepository.Save();
        }

        public void DeleteSetting<T>() where T : ISetting, new()
        {
            string typeName = GetClassName(typeof(T));
            _settingRepository.Delete(x => x.Name.StartsWith(typeName));
            //_settingRepository.Save();
        }

        private async Task UpdateSettingHelper<T>(T setting, PropertyInfo[] existingPropertyInfo) where T : ISetting, new()
        {
            string typeName = GetClassName(typeof(T));
            IList<Setting> settings = await _settingRepository.FindByAsync(x => x.Name.StartsWith(typeName));
            if (settings != null && settings.Count > 0)
            {
                foreach (var property in existingPropertyInfo)
                {
                    string name = GetName(property);
                    object rawValue = property.GetValue(setting, null);
                    string value = rawValue == null ? null : rawValue.ToString();

                    if (settings.Where(x => x.Name == name).SingleOrDefault() != null)
                    {
                        settings.Where(x => x.Name == name).SingleOrDefault().Value = value;
                    }
                }
            }
        }

        private async Task<bool> SettingExist<T>() where T : ISetting, new()
        {
            string typeName = GetClassName(typeof(T));
            IList<Setting> existingSettings = await _settingRepository.FindByAsync(x => x.Name.StartsWith(typeName));
            return existingSettings != null && existingSettings.Count > 0 ? true : false;
        }

        private string GetClassName(Type type)
        {
            string[] settings = type.FullName.Split('.');
            string className = settings[settings.Length - 1];
            return className;
        }

        private string GetName(PropertyInfo propertyInfo)
        {
            Guard.NotNull(propertyInfo, nameof(propertyInfo));

            string[] names = propertyInfo.DeclaringType.FullName.Split('.');
            string className = names[names.Length - 1];
            string propertyName = propertyInfo.Name;
            string settingName = string.Join(".", className, propertyName);

            return settingName;
        }







    }
}
