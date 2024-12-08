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
        // AG_ST: New Kunlun Control Hub

        // AG_S1: New Kunlun Central Hall
        {"AG_S1_SenateHall_[Variable] Picked7c593684-75c0-47b9-97f2-c8b91a58b991", ("New Kunlun Central Hall - Council Tenets", IPDKind.Encyclopedia)},
        {"AG_S1_SenateHall_[Variable] Picked534fac80-3271-4629-ab19-d5afd3e798a3", ("New Kunlun Central Hall - Standard Component", IPDKind.DropItem)},
        {"AG_S1_SenateHall_[Variable] Pickedd21f4407-1618-4b7b-8771-6620becf19ab", ("New Kunlun Central Hall - Council Digital Signage", IPDKind.Encyclopedia)},
        {"AG_S1_SenateHall_[Variable] Pickedc8fcde14-8173-489d-9441-ecabac1ab50f", ("New Kunlun Central Hall - New Kunlun Launch Memorial", IPDKind.Encyclopedia)},
        {"AG_S1_AG_ST", ("New Kunlun Central Hall to New Kunlun Control Hub", IPDKind.Connector)},
        {"AG_S1_AG_S2", ("New Kunlun Central Hall to Four Seasons Pavilion", IPDKind.Connector)},
        {"AG_S1_A2_S6", ("New Kunlun Central Hall to Central Transport Hub", IPDKind.Connector)},
        {"AG_S1_A3_S1", ("New Kunlun Central Hall to Lake Yaochi Ruins", IPDKind.Connector)},
        {"AG_S1_A7_S1", ("New Kunlun Central Hall to Cortex Center", IPDKind.Connector)},

        // AG_S2: Four Seasons Pavilion
        {"AG_S2_YiBase_[Variable] Picked9dcc2b3f-a1bf-4837-bb06-7875498781c7", ("Four Seasons Pavilion - First Floor Chest", IPDKind.DropItem)},
        {"AG_S2_YiBase_[Variable] Pickedd2b3e894-4292-4efe-a627-f8c7e38d4891", ("Four Seasons Pavilion - Second Floor Chest", IPDKind.DropItem)},
        {"AG_S2_AG_S1", ("Four Seasons Pavilion to New Kunlun Central Hall", IPDKind.Connector)},

        // A0_S10: Galactic Dock
        {"A0_S10_SpaceshipYard_[Variable] Picked5dca2439-7a4c-4e9e-9e66-a0282319627b", ("Galactic Dock - Tao Fruit", IPDKind.DropItem)},
        {"A0_S10_SpaceshipYard_[Variable] Pickeddac9a5c3-6ace-48c8-8bbf-7d6b55360d7f", ("Galactic Dock - Galactic Dock Sign", IPDKind.Encyclopedia)},

        // A1_S1: Apeman Facility (Monitoring)
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag27cfe3ad-4c1d-4d51-bcee-80e5c7bebf24", ("Apeman Facility (Monitoring) - Jin Chest near first big enemy", IPDKind.MoneyCrate)},
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag71961019-5ce4-4bcb-834b-c5b84360eb4a", ("Apeman Facility (Monitoring) - Jin Chest in Tunnels", IPDKind.MoneyCrate)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedb7eb8443-41b9-42cc-b809-0ff7dd487e96", ("Apeman Facility (Monitoring) - Item from Meat Machine", IPDKind.DropItem)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedf78b77c1-de42-4a36-87ce-255804e9826d", ("Apeman Facility (Monitoring) - Encyclopedia Item", IPDKind.Encyclopedia)},
        {"A1_S1_A1_S2", ("Apeman Facility (Monitoring) to Apeman Facility (Elevator)", IPDKind.Connector)},

        // A1_S2: Apeman Facility (Elevator)
        {"A1_S2_ConnectionToElevator_Final_MoneyCrateFlage2079e70-0e6f-4c0c-a408-43b35db10eb4", ("Apeman Facility (Elevator) - Behind Hut 1", IPDKind.MoneyCrate)},
        {"A1_S2_ConnectionToElevator_Final_MoneyCrateFlag9323d7ea-46f3-4d59-977f-ff7017f38bb5", ("Apeman Facility (Elevator) - Behind Hut 2", IPDKind.MoneyCrate)},
        {"A1_S2_ConnectionToElevator_Final_MoneyCrateFlag40644c8a-0872-44b2-9a45-d00be083704d", ("Apeman Facility (Elevator) - Jin Chest in center", IPDKind.MoneyCrate)},
        {"A1_S2_ConnectionToElevator_Final_[Variable] Pickedcd29251b-2344-4eeb-a7ba-6e34283f3764", ("Apeman Facility (Elevator) - Basic component at elevator", IPDKind.MoneyCrate)},
        {"A1_S2_ConnectionToElevator_Final_[Variable] Picked11a75a1a-15b0-48d4-ba71-f848ba9d869d", ("Apeman Facility (Elevator) - Boss Drop, Statis Jade", IPDKind.MoneyCrate)},
        {"A1_S2_A1_S1", ("Apeman Facility (Elevator) to Apeman Facility (Monitoring)", IPDKind.Connector)},
        {"A1_S2_A1_S3", ("Apeman Facility (Elevator) to Apeman Facility (Depths)", IPDKind.Connector)},
        {"A1_S2_A2_S6", ("Apeman Facility (Elevator) to Central Transport Hub", IPDKind.Connector)},

        // A1_S3: Apeman Facility (Depths)
        {"A1_S3_A1_S2", ("Apeman Facility (Depths) to Apeman Facility (Elevator)", IPDKind.Connector)},
        {"A1_S3_A2_S3", ("Apeman Facility (Depths) to Power Reservoir (West)", IPDKind.Connector)},
        {"A1_S3_A6_S1", ("Apeman Facility (Depths) to Factory (Underground)", IPDKind.Connector)},

        // A2_S1: Power Reservoir (Central)
        // -2600 -1880 Map Chip NPC
        {"A2_S1_ReactorMiddle_Final_[Variable] Pickedbf33f997-8366-47b4-b81f-9a1d984f970a", ("Power Reservoir (Central) - Big Chest right of pagoda (Steely Jade)", IPDKind.DropItem)},
        {"A2_S1_ReactorMiddle_Final_MoneyCrateFlag44cb9d15-844b-49fc-aabd-f01a9809cd0e", ("Power Reservoir (Central) - Jin Chest right of pagoda", IPDKind.MoneyCrate)},
        {"A2_S1_ReactorMiddle_Final_[Variable] Pickeddb970783-a21b-4fd9-9a39-5b7774ea48b0", ("Power Reservoir (Central) - Parry Puzzle reward (Standard Component)", IPDKind.DropItem)},
        // 180 -1470 Parry puzzle
        {"A2_S1_ReactorMiddle_Final_MoneyCrateFlagc694fc36-34e5-4552-9604-0226b9e745d3", ("Power Reservoir (Central) - Jin Chest right side", IPDKind.MoneyCrate)},
        {"A2_S1_ReactorMiddle_Final_MoneyCrateFlagec457f7a-0242-4506-8f77-5503ebf7c62d", ("Power Reservoir (Central) - Jin Chest behind breakable wall", IPDKind.MoneyCrate)},
        {"A2_S1_A2_S2", ("Power Reservoir (Central) to Power Reservoir (East)", IPDKind.Connector)},
        {"A2_S1_A2_S3", ("Power Reservoir (Central) to Power Reservoir (West)", IPDKind.Connector)},
        {"A2_S1_A2_S5", ("Power Reservoir (Central) to Radiant Pagoda", IPDKind.Connector)},

        // A2_S2: Power Reservoir (East)
        {"A2_S2_ReactorRight_Final_[Variable] Pickedf37a555f-9c4e-49ad-a9bd-14ccb57a87b7", ("Power Reservoir (East) - Mini Boss", IPDKind.DropItem)},
        {"A2_S2_ReactorRight_Final_MoneyCrateFlagf0153130-df56-45de-9e0c-2ac743d30e7d", ("Power Reservoir (East) - Jin Chest", IPDKind.DropItem)},
        // Parry puzzle on right side next to reward
        {"A2_S2_ReactorRight_Final_[Variable] Picked0c38212b-59e4-4171-a5c7-d7a17ca595bc", ("Power Reservoir (East) - Parry Puzzle reward (Basic Component)", IPDKind.DropItem)},
        {"A2_S2_ReactorRight_Final_[Variable] Picked026fbdc5-1f03-4179-b515-2d46a223320e", ("Power Reservoir (East) - Standard Component", IPDKind.DropItem)},
        {"A2_S2_ReactorRight_Final_[Variable] Pickedccefd541-2631-4303-8ec5-1f23a435caff", ("Power Reservoir (East) - Pauper Jade", IPDKind.Connector)},
        {"A2_S2_A2_S1", ("Power Reservoir (East) to Power Reservoir (Central)", IPDKind.Connector)},
        {"A2_S2_A2_S6", ("Power Reservoir (East) to Central Transport Hub", IPDKind.Connector)},

        // A2_S3: Power Reservoir (West)
        {"A2_S3_A1_S3", ("Power Reservoir (West) to Apeman Facility (Depths)", IPDKind.Connector)},
        {"A2_S3_A2_S1", ("Power Reservoir (West) to Power Reservoir (Central)", IPDKind.Connector)},

        // A2_S5: Radiant Pagoda
        {"A2_S5_BossHorseman_Final_[Variable] Picked7637cf70-7b5f-430b-bd2f-eea133a5c780", ("Radiant Pagoda - Boss Horse dude", IPDKind.DropItem)},
        {"A2_S5_BossHorseman_Final_[Variable] Picked165c38cf-fff8-4280-8a76-06e3740f203e", ("Radiant Pagoda - Radiant Pagoda Control Panel", IPDKind.Encyclopedia)},
        {"A2_S5_A2_S1", ("Radiant Pagoda to Power Reservoir (Central)", IPDKind.Connector)},

        // A2_S6: Central Transport Hub
        // 6560 -7720 Parry Puzzle, other one on right side next to puzzle reward
        {"A2_S6_LogisticCenter_Final_[Variable] Picked5067bec8-9b41-4ec4-b519-7ce767f84062", ("Central Transport Hub - Parry Puzzle reward (Shuanshuan gift)", IPDKind.DropItem)},
        {"A2_S6_LogisticCenter_Final_[Variable] Picked80a94da9-9df5-4178-969d-1c034d7f6c62", ("Central Transport Hub - Red Tiger Elite: Yanren", IPDKind.DropItem)},
        {"A2_S6_LogisticCenter_Final_MoneyCrateFlag31ed4e47-d668-414f-8b5f-f650b498c440", ("Central Transport Hub - Jin Chest left of root node", IPDKind.MoneyCrate)},
        {"A2_S6_LogisticCenter_Final_[Variable] Picked935ddd59-08d5-47a8-ae20-f5b1cb11daee", ("Central Transport Hub - Anomalous Root Node", IPDKind.Encyclopedia)},
        {"A2_S6_LogisticCenter_Final_MoneyCrateFlag012abced-9ae0-4dd6-a20f-b1b717745afb", ("Central Transport Hub - Jin Chest at elevator", IPDKind.MoneyCrate)},
        {"A2_S6_AG_S1", ("Central Transport Hub to New Kunlun Central Hall", IPDKind.Connector)},
        {"A2_S6_A0_S10", ("Central Transport Hub to Galactic Dock", IPDKind.Connector)},
        {"A2_S6_A1_S2", ("Central Transport Hub to Apeman Facility (Elevator)", IPDKind.Connector)},
        {"A2_S6_A2_S2", ("Central Transport Hub to Power Reservoir (East)", IPDKind.Connector)},
        {"A2_S6_A11_S1", ("Central Transport Hub to Tiandao Research Center", IPDKind.Connector)},

        // A3_S1: Lake Yaochi Ruins
        {"A3_S1_GardenRuins_Final_[Variable] Picked9b11c0f9-14d8-44cd-b9e4-32a127d9cecf", ("Lake Yaochi Ruins - Chest right exit (Basic Component)", IPDKind.DropItem)},
        {"A3_S1_GardenRuins_Final_[Variable] Picked1b471858-03c9-4c04-ae38-63b469f31a9a", ("Lake Yaochi Ruins - Lake Yaochi Stele", IPDKind.Encyclopedia)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlagcb74e016-48f5-4907-96d6-c15b5016b56a", ("Lake Yaochi Ruins - Jin Chest Daybreak Tower", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlag1594c7e3-d713-4d60-9190-7f167834aecb", ("Lake Yaochi Ruins - Jin Chest in cave", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlagb7f525fd-49a0-4d0a-910c-c146a56da3d0", ("Lake Yaochi Ruins - Suicide Chest 1", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlag4bee4139-bd40-4e2b-ad93-83a0c4dbe8a8", ("Lake Yaochi Ruins - Suicide Chest 2", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_[Variable] Picked3c04a62b-694f-4463-b654-7152735fe2c0", ("Lake Yaochi Ruins - Left Cave Chest (Basic Component)", IPDKind.DropItem)},
        {"A3_S1_AG_S1", ("Lake Yaochi Ruins to New Kunlun Central Hall", IPDKind.Connector)},
        {"A3_S1_A10_S1", ("Lake Yaochi Ruins to Grotto of Scriptures (Entry)", IPDKind.Connector)},
        {"A3_S1_A3_S7", ("Lake Yaochi Ruins to Yinglong Canals", IPDKind.Connector)},
        // A3_SG2: Lake Yaochi Ruins, Right Subroom
        {"A3_SG2_[Variable] Picked3ff42e51-0d4a-4ced-84ca-43520be8fb2f", ("Lake Yaochi Ruins, Right Subroom - Soul Reaper Jade", IPDKind.DropItem)},

        // A3_S7: Yinglong Canals

        // A6_S1: Factory (Underground)

        // A10_S1: Grotto of Scriptures (Entry)
        {"A10_S1_TombEntrance_remake_[Variable] Picked3b7c610d-f1aa-46bd-bf15-f71f24750ded", ("Grotto of Scriptures (Entry) - Ancient Cave Painting", IPDKind.Encyclopedia)},

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
