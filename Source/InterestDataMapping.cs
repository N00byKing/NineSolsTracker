using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

static class InterestDataMapping {
    public enum IPDKind {
        Unknown,
        MoneyCrate,
        DropItem,
        Encyclopedia,
        Connector
    }

    public static bool IsValidLocation(InterestPointData IPD) {
        if (ToHumanReadable.ContainsKey(IPD.name)) return true; // Hand-picked values always OK
        if (IPD.InterestPointConfigContent) {
            if (IPD.InterestPointConfigContent.name.Contains("DropItem")) {
                return true;
            }
        }
        if (IPD.name.Contains("MoneyCrate")) {
            return true;
        }
        if (IPD.name.Contains("Picked")) {
            return true;
        }
        return false;
    }

    public static Sprite? GetLocSprite(InterestPointData IPD) {
        IPDKind kind = GetHumanReadable(IPD).kind;
        switch (kind) {
            case IPDKind.DropItem: return AssetManager.ChestSprite;
            case IPDKind.MoneyCrate: return AssetManager.MoneySprite;
            case IPDKind.Encyclopedia: return AssetManager.EncyclopediaSprite;
            case IPDKind.Connector: return AssetManager.ConnectorSprite;
        }
        // Fallback
        if (IPD.name.Contains("MoneyCrate")) return AssetManager.MoneySprite;
        if (IPD.InterestPointConfigContent.name.Contains("DropItem")) return AssetManager.ChestSprite;
        return AssetManager.ChestSprite;
    }

    private static readonly Dictionary<String, (String name, IPDKind kind)> ToHumanReadable = new()
    {
        // A1_S1: Apeman Facility (Monitoring)
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag27cfe3ad-4c1d-4d51-bcee-80e5c7bebf24", ("Apeman Facility (Monitoring) - Jin Chest near first big enemy", IPDKind.MoneyCrate)},
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag71961019-5ce4-4bcb-834b-c5b84360eb4a", ("Apeman Facility (Monitoring) - Jin Chest in Tunnels", IPDKind.MoneyCrate)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedb7eb8443-41b9-42cc-b809-0ff7dd487e96", ("Apeman Facility (Monitoring) - Item from Meat Machine", IPDKind.DropItem)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedf78b77c1-de42-4a36-87ce-255804e9826d", ("Apeman Facility (Monitoring) - Encyclopedia Item", IPDKind.Encyclopedia)},
        {"A1_S1_A1_S2", ("Connector Apeman Facility (Monitoring) to Apeman Facility (Elevator)", IPDKind.Connector)}
    };
    public static (String name, IPDKind kind) GetHumanReadable(InterestPointData IPD) {
        if (ToHumanReadable.ContainsKey(IPD.name)) return ToHumanReadable[IPD.name];
        return (IPD.name, IPDKind.Unknown);
    }

    public static void CreateIPC(InterestPointData IPD) {
        InterestPointConfig IPConfigTemplate = ScriptableObject.CreateInstance<InterestPointConfig>();
        IPConfigTemplate.PointName = IPConfigTemplate.name = "Custom_IPC_from_template";
        IPConfigTemplate.YOffset = -8;
        IPD.InterestPointConfigContent = IPConfigTemplate;
    }
    public static InterestPointData CreateIPD(String name, Vector3 pos) {
        InterestPointData IPD = ScriptableObject.CreateInstance<InterestPointData>();
        IPD.name = name;
        IPD.worldPosition = pos;
        InterestDataMapping.CreateIPC(IPD);
        IPD.InterestPointConfigContent.showInWorldMapType = InterestPointConfig.ShowInMapType.ShowInMinimapOnly;
        IPD.InterestPointConfigContent.icon = InterestDataMapping.GetLocSprite(IPD);
        IPD.PlayerKnowExist = new();
        IPD.PlayerKnowExist.SetCurrentValue(true);
        IPD.OverwriteMapIcon = new();
        FieldInfo fI = IPD.GetType().GetField("solved", BindingFlags.NonPublic | BindingFlags.Instance);
        fI.SetValue(IPD, new FlagFieldBool());
        IPD.solvedDataBool = ScriptableObject.CreateInstance<ScriptableDataBool>();
        IPD.solvedDataBool.field = new();
        IPD.solvedDataBool.flagValueChangeEvent = new();
        IPD.OverwriteMapIcon = new();
        IPD.DestroyedOnMap = new();
        IPD.CullingEntered = new();
        IPD.NPCPinned = new();
        IPD.NPCPinnedAnimationPlayed = new();
        IPD.gameStateType = GameFlagBase.GameStateType.AutoUnique;
        return IPD;
    }
}
