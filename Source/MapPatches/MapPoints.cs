using System.Collections.Generic;
using HarmonyLib;

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
        // First, add item nodes
        foreach (InterestPointData IPD in __instance.InterestPointsInScene) {
            if (InterestDataMapping.IsValidLocation(IPD)) {
                InterestDataMapping.SetIPC(IPD);
                IPD.PlayerKnowExist.SetCurrentValue(true);
                if (!__result.Contains(IPD)) __result.Add(IPD);
            }
        }
        // Now, custom connection nodes
        HashSet<InterestPointData> connIPDs = Connectors.GetConnections(__instance.name);
        foreach (InterestPointData IPD in connIPDs) {
            if (!__result.Contains(IPD)) __result.Add(IPD);
            if (!__instance.InterestPointsInScene.Contains(IPD)) {
                __instance.InterestPointsInScene.Add(IPD);
            }
        }
        return;
    }
}
