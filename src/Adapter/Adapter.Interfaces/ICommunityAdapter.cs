// Copyright (c) Richasy. All rights reserved.

using Wfa.Models.Community;

namespace Wfa.Adapter.Interfaces
{
    /// <summary>
    /// WFCD 社区数据转换工具接口.
    /// </summary>
    public interface ICommunityAdapter
    {
        /// <summary>
        /// 将JSON转换为<see cref="ArchGun"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="ArchMelee"/>.</returns>
        ArchGun ConvertToArchGun(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为<see cref="ArchMelee"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="ArchMelee"/>.</returns>
        ArchMelee ConvertToArchMelee(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为<see cref="Archwing"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="Archwing"/>.</returns>
        Archwing ConvertToArchwing(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为<see cref="Melee"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="Melee"/>.</returns>
        Melee ConvertToMelee(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为<see cref="Primary"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="Primary"/>.</returns>
        Primary ConvertToPrimary(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为<see cref="Secondary"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="Secondary"/>.</returns>
        Secondary ConvertToSecondary(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为<see cref="Warframe"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="Warframe"/>.</returns>
        Warframe ConvertToWarframe(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为<see cref="Mod"/>.
        /// </summary>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see cref="Mod"/>.</returns>
        Mod ConvertToMod(string json, string i18n = "");

        /// <summary>
        /// 将JSON转换为指定类型.
        /// </summary>
        /// <typeparam name="T">条目类型.</typeparam>
        /// <param name="json">源数据.</param>
        /// <param name="i18n">本地化文本.</param>
        /// <returns><see href="T"/>.</returns>
        T ConvertToEntry<T>(string json, string i18n = "")
            where T : EntryBase;
    }
}
