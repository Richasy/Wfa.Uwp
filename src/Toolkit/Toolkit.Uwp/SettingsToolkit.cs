// Copyright (c) Richasy. All rights reserved.

using System;
using Wfa.Models.Data.Constants;
using Wfa.Models.Enums;
using Wfa.Toolkit.Interfaces;
using Windows.Storage;

namespace Wfa.Toolkit
{
    /// <summary>
    /// Settings toolkit.
    /// </summary>
    public class SettingsToolkit : ISettingsToolkit
    {
        /// <inheritdoc/>
        public T ReadLocalSetting<T>(SettingNames settingName, T defaultValue)
        {
            var settingContainer = GetSettingContainer();

            if (IsSettingKeyExist(settingName))
            {
                if (defaultValue is Enum)
                {
                    var tempValue = settingContainer.Values[settingName.ToString()].ToString();
                    Enum.TryParse(typeof(T), tempValue, out var result);
                    return (T)result;
                }
                else
                {
                    return (T)settingContainer.Values[settingName.ToString()];
                }
            }
            else
            {
                WriteLocalSetting(settingName, defaultValue);
                return defaultValue;
            }
        }

        /// <inheritdoc/>
        public void WriteLocalSetting<T>(SettingNames settingName, T value)
        {
            var settingContainer = GetSettingContainer();

            if (value is Enum)
            {
                settingContainer.Values[settingName.ToString()] = value.ToString();
            }
            else
            {
                settingContainer.Values[settingName.ToString()] = value;
            }
        }

        /// <inheritdoc/>
        public void DeleteLocalSetting(SettingNames settingName)
        {
            var settingContainer = GetSettingContainer();

            if (IsSettingKeyExist(settingName))
            {
                settingContainer.Values.Remove(settingName.ToString());
            }
        }

        /// <inheritdoc/>
        public bool IsSettingKeyExist(SettingNames settingName)
            => GetSettingContainer().Values.ContainsKey(settingName.ToString());

        private ApplicationDataContainer GetSettingContainer()
            => ApplicationData.Current.LocalSettings.CreateContainer(AppConstants.SettingContainerName, ApplicationDataCreateDisposition.Always);
    }
}
