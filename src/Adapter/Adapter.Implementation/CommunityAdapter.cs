// Copyright (c) Richasy. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wfa.Adapter.Interfaces;
using Wfa.Models.Community;

namespace Wfa.Adapter
{
    /// <summary>
    /// WFCD 社区数据转换器.
    /// </summary>
    public sealed class CommunityAdapter : ICommunityAdapter
    {
        /// <inheritdoc/>
        public ArchGun ConvertToArchGun(string json, string i18n = "")
            => GenerateGunEntry<ArchGun>(json, i18n);

        /// <inheritdoc/>
        public ArchMelee ConvertToArchMelee(string json, string i18n = "")
        {
            EnsureValidJson(json);
            var jobj = JObject.Parse(json);
            var archMelee = JsonConvert.DeserializeObject<ArchMelee>(json);
            GenerateCombinePolarties(jobj, archMelee);

            if (!string.IsNullOrEmpty(i18n))
            {
                var i18nJobj = JObject.Parse(i18n);
                archMelee.Name = i18nJobj["name"].ToString().Replace("<ARCHWING>", string.Empty).Trim();
                archMelee.Description = i18nJobj["description"].ToString();
            }

            return archMelee;
        }

        /// <inheritdoc/>
        public Archwing ConvertToArchwing(string json, string i18n = "")
        {
            EnsureValidJson(json);
            var archwing = JsonConvert.DeserializeObject<Archwing>(json);
            GenerateCombinePolarties(JObject.Parse(json), archwing);

            if (!string.IsNullOrEmpty(i18n))
            {
                var i18nJobj = JObject.Parse(i18n);
                archwing.Name = i18nJobj["name"].ToString().Replace("<ARCHWING>", string.Empty).Trim();
                archwing.Description = i18nJobj["description"].ToString();

                var abilities = i18nJobj["abilities"].Children().ToArray();
                for (var i = 0; i < abilities.Length; i++)
                {
                    var sourceAbility = archwing.Abilities[i];
                    var i18nAbility = abilities[i];
                    if (i18nAbility != null)
                    {
                        sourceAbility.Name = i18nAbility["abilityName"].ToString();
                        sourceAbility.Description = i18nAbility["description"].ToString();
                    }
                }
            }

            return archwing;
        }

        /// <inheritdoc/>
        public Melee ConvertToMelee(string json, string i18n = "")
        {
            EnsureValidJson(json);
            var jobj = JObject.Parse(json);
            var melee = JsonConvert.DeserializeObject<Melee>(json);
            GenerateCombinePolarties(jobj, melee);

            if (!string.IsNullOrEmpty(i18n))
            {
                var i18nJobj = JObject.Parse(i18n);
                melee.Name = i18nJobj["name"].ToString();
                melee.Description = i18nJobj["description"].ToString();
            }

            return melee;
        }

        /// <inheritdoc/>
        public Primary ConvertToPrimary(string json, string i18n = "")
            => GenerateGunEntry<Primary>(json, i18n);

        /// <inheritdoc/>
        public Secondary ConvertToSecondary(string json, string i18n = "")
            => GenerateGunEntry<Secondary>(json, i18n);

        /// <inheritdoc/>
        public Warframe ConvertToWarframe(string json, string i18n = "")
        {
            EnsureValidJson(json);
            var warframe = JsonConvert.DeserializeObject<Warframe>(json);
            GenerateCombinePolarties(JObject.Parse(json), warframe);

            if (!string.IsNullOrEmpty(i18n))
            {
                var i18nJobj = JObject.Parse(i18n);
                warframe.Name = i18nJobj["name"].ToString();
                warframe.Description = i18nJobj["description"].ToString();
                warframe.PassiveDescription = i18nJobj["passiveDescription"].ToString();

                var abilities = i18nJobj["abilities"].Children().ToArray();
                for (var i = 0; i < abilities.Length; i++)
                {
                    var sourceAbility = warframe.Abilities[i];
                    var i18nAbility = abilities[i];
                    if (i18nAbility != null)
                    {
                        sourceAbility.Name = i18nAbility["abilityName"].ToString();
                        sourceAbility.Description = i18nAbility["description"].ToString();
                    }
                }
            }

            return warframe;
        }

        /// <inheritdoc/>
        public Mod ConvertToMod(string json, string i18n = "")
        {
            EnsureValidJson(json);
            var jobj = JObject.Parse(json);
            var mod = JsonConvert.DeserializeObject<Mod>(json);

            var levelList = new List<JToken>();
            if (string.IsNullOrEmpty(i18n))
            {
                // 此时不进行本地化，默认为英文.
                // 解析等级条目.
                if (jobj.ContainsKey("levelStats"))
                {
                    levelList = jobj["levelStats"].ToList();
                }
            }
            else
            {
                var i18nJobj = JObject.Parse(i18n);
                mod.Name = i18nJobj["name"].ToString();

                if (i18nJobj.ContainsKey("description"))
                {
                    mod.Description = i18nJobj["description"].ToString();
                }

                if (i18nJobj.ContainsKey("levelStats"))
                {
                    levelList = i18nJobj["levelStats"].ToList();
                }
            }

            mod.Effects = new List<ModEffect>();
            for (var i = 0; i < levelList.Count; i++)
            {
                var effectItem = new ModEffect
                {
                    Level = i + 1,
                    Description = string.Join("\n", levelList[i]["stats"].Select(p => p.ToString())),
                };
                mod.Effects.Add(effectItem);
            }

            if (jobj.ContainsKey("modSetValues"))
            {
                mod.ModSetEffects = string.Join(",", jobj["modSetValues"].Select(p => p.ToString()));
            }

            return mod;
        }

        /// <inheritdoc/>
        public T ConvertToEntry<T>(string json, string i18n = "")
            where T : EntryBase
        {
            EntryBase data = null;
            if (typeof(T) == typeof(ArchGun))
            {
                data = ConvertToArchGun(json, i18n);
            }
            else if (typeof(T) == typeof(ArchMelee))
            {
                data = ConvertToArchMelee(json, i18n);
            }
            else if (typeof(T) == typeof(Archwing))
            {
                data = ConvertToArchwing(json, i18n);
            }
            else if (typeof(T) == typeof(Melee))
            {
                data = ConvertToMelee(json, i18n);
            }
            else if (typeof(T) == typeof(Mod))
            {
                data = ConvertToMod(json, i18n);
            }
            else if (typeof(T) == typeof(Primary))
            {
                data = ConvertToPrimary(json, i18n);
            }
            else if (typeof(T) == typeof(Secondary))
            {
                data = ConvertToSecondary(json, i18n);
            }
            else if (typeof(T) == typeof(Warframe))
            {
                data = ConvertToWarframe(json, i18n);
            }

            return data != null ? (T)data : null;
        }

        private static T GenerateGunEntry<T>(string json, string i18n = "")
            where T : GunBase
        {
            EnsureValidJson(json);
            var jobj = JObject.Parse(json);
            var gun = JsonConvert.DeserializeObject<T>(json);
            GenerateCombinePolarties(jobj, gun);

            if (!string.IsNullOrEmpty(i18n))
            {
                var i18nJobj = JObject.Parse(i18n);
                gun.Name = i18nJobj["name"].ToString();
                gun.Description = i18nJobj["description"].ToString();
                gun.Trigger = i18nJobj["trigger"].ToString();
            }

            return gun;
        }

        private static void GenerateCombinePolarties(JObject jobj, IPolarities polaritiesObj)
        {
            if (jobj.ContainsKey("polarities"))
            {
                polaritiesObj.SelfPolarities = string.Join(",", jobj["polarities"].Children().Select(p => p.ToString()));
            }
        }

        private static void EnsureValidJson(string json, string startCharacter = "{")
        {
            if (string.IsNullOrEmpty(json) || !json.StartsWith(startCharacter))
            {
                throw new ArgumentException("Unexpected json");
            }
        }
    }
}
