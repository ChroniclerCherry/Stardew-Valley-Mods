﻿using Harmony;
using StardewModdingAPI;
using StardewValley;
using System;

namespace SlimeHutchLimit
{
    public class ModEntry : Mod
    {
        private static Config config;
        public override void Entry(IModHelper helper)
        {
            config = Helper.ReadConfig<Config>();

            Helper.ConsoleCommands.Add("SetSlimeHutchLimit", "Changes the max number of slimes that can inhabit a slime hutch.\n\nUsage: SetSlimeHutchLimit <value>\n- value: the number of slimes", ChangeMaxSlimes);

            HarmonyInstance harmony = HarmonyInstance.Create(ModManifest.UniqueID);
            harmony.Patch(AccessTools.Method(typeof(SlimeHutch), nameof(SlimeHutch.isFull)),
                postfix: new HarmonyMethod(typeof(ModEntry), nameof(ModEntry.SlimeHutch_isFull_postfix)));

        }

        private static void SlimeHutch_isFull_postfix(GameLocation __instance, ref bool __result)
        {
            __result = __instance.characters.Count >= (config?.MaxSlimesInHutch ?? 20);
        }

        private void ChangeMaxSlimes(string arg1, string[] arg2)
        {
            config.MaxSlimesInHutch = int.Parse(arg2[0]);
            Helper.WriteConfig(config);
        }
    }

    public class Config
    {
        public int MaxSlimesInHutch { get; set; } = 20;
    }
}
