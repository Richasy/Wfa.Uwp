// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using Wfa.Models.Data.Constants;

namespace Wfa.ViewModel.Base
{
    /// <summary>
    /// ViewModel的基类.
    /// </summary>
    public class ViewModelBase : ReactiveObject
    {
        /// <summary>
        /// 记录错误信息.
        /// </summary>
        /// <param name="exception">错误信息.</param>
        protected void LogException(Exception exception)
        {
            Debug.WriteLine($"{exception.Message}\n{exception.StackTrace}");
            this.Log().Error(exception);
        }

        /// <summary>
        /// 这是一种退避策略，当调用时，通常意味着重新导航到了加载过的页面，
        /// 此时视频集合中已经有了数十条数据，如果直接让 UI 渲染，会出现导航动画与列表渲染的资源竞争，导致 UI 卡顿。
        /// 这里的延时会让 UI 先渲染加载状态，等页面可以被用户感知到的时候再进行已有视频条目的渲染。
        /// 这个 250ms 是一个估计值，并不能够完全确保 UI 不会卡顿（绝大部分情况下可以）.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        protected Task FakeLoadingAsync()
            => Task.Delay(AppConstants.FakeLoadingMilliseconds);

        /// <summary>
        /// 执行一个具有最小运行时间的任务.
        /// </summary>
        /// <param name="work">需要执行的任务.</param>
        /// <returns><see cref="Task"/>.</returns>
        protected Task RunDelayTask(Task work)
            => Task.WhenAll(
                    Task.Run(async () => await work),
                    Task.Delay(AppConstants.FakeLoadingMilliseconds));

        /// <summary>
        /// 尝试清除集合，仅在集合内有数据时才执行.
        /// </summary>
        /// <typeparam name="T">数据类型.</typeparam>
        /// <param name="collection">集合.</param>
        protected void TryClear<T>(ObservableCollection<T> collection)
        {
            if (collection.Count > 0)
            {
                collection.Clear();
            }
        }

        /// <summary>
        /// 输出调试信息.
        /// </summary>
        /// <param name="msg">调试窗口需要显示的信息.</param>
        protected void WriteMessage(string msg)
            => Debug.WriteLine(msg);
    }
}
