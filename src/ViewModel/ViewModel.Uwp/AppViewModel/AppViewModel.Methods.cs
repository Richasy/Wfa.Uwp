// Copyright (c) Richasy. All rights reserved.

using System;
using System.Threading.Tasks;
using Wfa.Models.Data.Local;
using Wfa.Models.Enums;

namespace Wfa.ViewModel
{
    /// <summary>
    /// 应用视图模型.
    /// </summary>
    public sealed partial class AppViewModel
    {
        private void OnStateTimerTick(object sender, object e)
            => RequestWorldStateCommand.Execute().Subscribe();

        private void OnWorldStateChanged(object sender, EventArgs e)
        {
            IsShowNightwave = _stateProvider.GetNightwave() != null;
            WriteMessage("世界状态已经更新");
        }

        /// <summary>
        /// 检查更新.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        private async Task CheckAppUpdateAsync()
        {
            var data = await _updateProvider.GetGithubLatestReleaseAsync();
            var currentVersion = _appToolkit.GetPackageVersion();
            var ignoreVersion = _settingsToolkit.ReadLocalSetting(SettingNames.IgnoreVersion, string.Empty);
            var args = new AppUpgradeEventArgs(data);
            if (args.Version != currentVersion && args.Version != ignoreVersion)
            {
                RequestShowAppUpgradeDialog?.Invoke(this, args);
            }
        }
    }
}
