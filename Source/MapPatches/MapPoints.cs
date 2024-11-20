using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using Com.LuisPedroFonseca.ProCamera2D;
using HarmonyLib;
using NineSolsAPI;
using NineSolsTracker;
using UnityEngine;
using UnityEngine.TerrainTools;

[HarmonyPatch]
public static class MapPoints {

    [HarmonyPostfix, HarmonyPatch(typeof(InterestPointData), nameof(InterestPointData.VisibleOnMap), MethodType.Getter)]
    private static void ShowInMap(InterestPointData __instance, ref bool __result) {
        __result = __result || InterestDataMapping.IsValidLocation(__instance);
    }
    [HarmonyPostfix, HarmonyPatch(typeof(InterestPointData), nameof(InterestPointData.IsShowInWorldMap), MethodType.Getter)]
    private static void ShowInWorldMap(InterestPointData __instance, ref bool __result) {
        __result = __result || InterestDataMapping.IsValidLocation(__instance);
    }

    [HarmonyPostfix, HarmonyPatch(typeof(GameLevelMapData), nameof(GameLevelMapData.FindAllInterestPointShouldShowInMinimaps), MethodType.Getter)]
    private static void ShowInMiniMapGLD(GameLevelMapData __instance, ref List<InterestPointData> __result) {
        foreach (InterestPointData IPD in __instance.InterestPointsInScene) {
            if (InterestDataMapping.IsValidLocation(IPD) && !__result.Contains(IPD)) {
                if (!IPD.InterestPointConfigContent) {
                    InterestPointConfig IPConfigTemplate = ScriptableObject.CreateInstance<InterestPointConfig>();
                    IPConfigTemplate.PointName = IPConfigTemplate.name = "Custom_IPC_from_template";
                    IPConfigTemplate.YOffset = -8;
                    IPD.InterestPointConfigContent = IPConfigTemplate;
                }
                IPD.InterestPointConfigContent.showInWorldMapType = InterestPointConfig.ShowInMapType.ShowInMinimapAndWorldMap;
                IPD.InterestPointConfigContent.icon = InterestDataMapping.GetLocSprite(IPD);
                IPD.PlayerKnowExist.SetCurrentValue(true);
                __result.Add(IPD);
            }
        }
        return;
    }

    public static void CheckLocation(int loc) {}
}
