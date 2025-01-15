using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using Com.LuisPedroFonseca.ProCamera2D;
using HarmonyLib;
using UnityEngine;
using UnityEngine.TerrainTools;

[HarmonyPatch]
public static class RevealMap {

    class FlagFieldConstBool : FlagFieldBool {
        public override bool CurrentValue { get => true; set => _currentValue = true; }
    }

    static FlagFieldConstBool defTrue = new FlagFieldConstBool();

    [HarmonyPostfix, HarmonyPatch(typeof(MinimapAreaUIButton), nameof(MinimapAreaUIButton.EverVisited), MethodType.Getter)]
    private static void EverVisited(MinimapAreaUIButton __instance, ref bool __result) {
        __result = true;
    }
    [HarmonyPostfix, HarmonyPatch(typeof(GameLevelMapData), nameof(GameLevelMapData.CanShowDetailMap), MethodType.Getter)]
    private static void ShowDetailMap(GameLevelMapData __instance, ref bool __result) {
        __instance.MistMapDataEntries.Clear();
        __result = true;
    }
    [HarmonyPostfix, HarmonyPatch(typeof(GameLevelMapData), nameof(GameLevelMapData.FindAllInterestPointShouldShowInMinimaps), MethodType.Getter)]
    private static void ShowInMiniMapGLD(GameLevelMapData __instance) {
        __instance.Unlocked.CurrentValue = true;
        __instance.MistMapDataEntries.Clear();
    }
}
