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
        // AG_S1: New Kunlun Central Hall

        // A0_S10: Galactic Dock

        // A1_S1: Apeman Facility (Monitoring)
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag27cfe3ad-4c1d-4d51-bcee-80e5c7bebf24", ("Apeman Facility (Monitoring) - Jin Chest near first big enemy", IPDKind.MoneyCrate)},
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag71961019-5ce4-4bcb-834b-c5b84360eb4a", ("Apeman Facility (Monitoring) - Jin Chest in Tunnels", IPDKind.MoneyCrate)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedb7eb8443-41b9-42cc-b809-0ff7dd487e96", ("Apeman Facility (Monitoring) - Item from Meat Machine", IPDKind.DropItem)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedf78b77c1-de42-4a36-87ce-255804e9826d", ("Apeman Facility (Monitoring) - Encyclopedia Item", IPDKind.Encyclopedia)},
        {"A1_S1_A1_S2", ("Apeman Facility (Monitoring) to Apeman Facility (Elevator)", IPDKind.Connector)},

        // A1_S2: Apeman Facility (Elevator)
        {"A1_S2_A1_S1", ("Apeman Facility (Elevator) to Apeman Facility (Monitoring)", IPDKind.Connector)},
        {"A1_S2_A1_S3", ("Apeman Facility (Elevator) to Apeman Facility (Depths)", IPDKind.Connector)},
        {"A1_S2_A2_S6", ("Apeman Facility (Elevator) to Central Transport Hub", IPDKind.Connector)},

        // A1_S3: Apeman Facility (Depths)
        {"A1_S3_A1_S2", ("Apeman Facility (Depths) to Apeman Facility (Elevator)", IPDKind.Connector)},
        {"A1_S3_A2_S3", ("Apeman Facility (Depths) to Power Reservoir (West)", IPDKind.Connector)},
        {"A1_S3_A6_S1", ("Apeman Facility (Depths) to Factory (Underground)", IPDKind.Connector)},

        // A2_S1: Power Reservoir (Central)
        {"A2_S1_A2_S2", ("Power Reservoir (Central) to Power Reservoir (East)", IPDKind.Connector)},
        {"A2_S1_A2_S3", ("Power Reservoir (Central) to Power Reservoir (West)", IPDKind.Connector)},
        {"A2_S1_A2_S5", ("Power Reservoir (Central) to Radiant Pagoda", IPDKind.Connector)},

        // A2_S2: Power Reservoir (East)
        {"A2_S2_A2_S1", ("Power Reservoir (East) to Power Reservoir (Central)", IPDKind.Connector)},
        {"A2_S2_A2_S6", ("Power Reservoir (East) to Central Transport Hub", IPDKind.Connector)},

        // A2_S3: Power Reservoir (West)
        {"A2_S3_A1_S3", ("Power Reservoir (West) to Apeman Facility (Depths)", IPDKind.Connector)},
        {"A2_S3_A2_S1", ("Power Reservoir (West) to Power Reservoir (Central)", IPDKind.Connector)},

        // A2_S5: Radiant Pagoda
        {"A2_S5_A2_S1", ("Radiant Pagoda to Power Reservoir (Central)", IPDKind.Connector)},

        // A2_S6: Central Transport Hub
        {"A2_S6_AG_S1", ("Central Transport Hub to New Kunlun Central Hall", IPDKind.Connector)},
        {"A2_S6_A0_S10", ("Central Transport Hub to Galactic Dock", IPDKind.Connector)},
        {"A2_S6_A1_S2", ("Central Transport Hub to Apeman Facility (Elevator)", IPDKind.Connector)},
        {"A2_S6_A2_S2", ("Central Transport Hub to Power Reservoir (East)", IPDKind.Connector)},
        {"A2_S6_A11_S1", ("Central Transport Hub to Tiandao Research Center", IPDKind.Connector)},

        // A6_S1: Factory (Underground)

        // A11_S1: Tiandao Research Center
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
