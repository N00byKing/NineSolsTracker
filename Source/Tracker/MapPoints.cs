using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using Com.LuisPedroFonseca.ProCamera2D;
using HarmonyLib;
using NineSolsAPI;
using UnityEngine;
using UnityEngine.TerrainTools;

[HarmonyPatch]
public static class MapPoints {

    static bool init = false;

    static Sprite? ChestSprite = null;
    static InterestPointConfig IPConfigTemplate = ScriptableObject.CreateInstance<InterestPointConfig>();

    static void Init() {
        if (init) return;

        // Look for Chest sprite
        Sprite[] Sprites = Resources.FindObjectsOfTypeAll<Sprite>();
        foreach (Sprite s in Sprites) {
            if (s.name == "Map_icon_5") {
                ChestSprite = s;
            }
        }

        // Create sprites for others
        #warning TODO

        IPConfigTemplate.PointName = IPConfigTemplate.name = "Custom_IPC_from_template";
        IPConfigTemplate.YOffset = -8;

        init = true;
    }

    [HarmonyPostfix, HarmonyPatch(typeof(InterestPointData), nameof(InterestPointData.VisibleOnMap), MethodType.Getter)]
    private static void ShowInMap(InterestPointData __instance, ref bool __result) {
        __result = __result || ShouldShowIPD(__instance);
    }
    [HarmonyPostfix, HarmonyPatch(typeof(InterestPointData), nameof(InterestPointData.IsShowInWorldMap), MethodType.Getter)]
    private static void ShowInWorldMap(InterestPointData __instance, ref bool __result) {
        __result = __result || ShouldShowIPD(__instance);
    }

    [HarmonyPostfix, HarmonyPatch(typeof(GameLevelMapData), nameof(GameLevelMapData.FindAllInterestPointShouldShowInMinimaps), MethodType.Getter)]
    private static void ShowInMiniMapGLD(GameLevelMapData __instance, ref List<InterestPointData> __result) {
        Init();
        foreach (InterestPointData IPD in __instance.InterestPointsInScene) {
            if (ShouldShowIPD(IPD) && !__result.Contains(IPD)) {
                if (!IPD.InterestPointConfigContent) {
                    IPD.InterestPointConfigContent = IPConfigTemplate;
                }
                IPD.InterestPointConfigContent.showInWorldMapType = InterestPointConfig.ShowInMapType.ShowInMinimapAndWorldMap;
                IPD.InterestPointConfigContent.icon = ChestSprite;
                IPD.PlayerKnowExist.SetCurrentValue(true);
                __result.Add(IPD);
            }
        }
        return;
    }

    private static bool ShouldShowIPD(InterestPointData IPD) {
        if (IPD.InterestPointConfigContent) {
            if (IPD.InterestPointConfigContent.name.Contains("DropItem")) {
                return !IPD.IsSolved;
            }
        }
        if (IPD.name.Contains("MoneyCrate")) {
            return !IPD.IsSolved;
        }
        return false;
    }

    public static void CheckLocation(int loc) {}
}
