// Copyright (c) Richasy. All rights reserved.

using System;

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
    }
}
