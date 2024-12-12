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
        {"A3_S1_GardenRuins_Final_[Variable] Pickedaa277dfc-d0e8-4363-8e56-3cb2302ee6cb", ("Lake Yaochi Ruins - Chest above Root Node", IPDKind.DropItem)},
        {"A3_S1_GardenRuins_Final_[Variable] Picked9b11c0f9-14d8-44cd-b9e4-32a127d9cecf", ("Lake Yaochi Ruins - Chest right exit (Basic Component)", IPDKind.DropItem)},
        {"A3_S1_GardenRuins_Final_[Variable] Picked1b471858-03c9-4c04-ae38-63b469f31a9a", ("Lake Yaochi Ruins - Lake Yaochi Stele", IPDKind.Encyclopedia)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlagcb74e016-48f5-4907-96d6-c15b5016b56a", ("Lake Yaochi Ruins - Jin Chest Daybreak Tower", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlag1594c7e3-d713-4d60-9190-7f167834aecb", ("Lake Yaochi Ruins - Jin Chest in cave", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlagb7f525fd-49a0-4d0a-910c-c146a56da3d0", ("Lake Yaochi Ruins - Suicide Chest 1", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_MoneyCrateFlag4bee4139-bd40-4e2b-ad93-83a0c4dbe8a8", ("Lake Yaochi Ruins - Suicide Chest 2", IPDKind.MoneyCrate)},
        {"A3_S1_GardenRuins_Final_[Variable] Picked3c04a62b-694f-4463-b654-7152735fe2c0", ("Lake Yaochi Ruins - Left Cave Chest (Basic Component)", IPDKind.DropItem)},
        {"A3_S1_A3_SG1", ("Lake Yaochi Ruins to Left Subroom", IPDKind.Connector)},
        {"A3_S1_A3_SG2", ("Lake Yaochi Ruins to Right Subroom", IPDKind.Connector)},
        {"A3_S1_A3_SG4", ("Lake Yaochi Ruins to Daybreak Tower (Subroom)", IPDKind.Connector)},
        {"A3_S1_AG_S1", ("Lake Yaochi Ruins to New Kunlun Central Hall", IPDKind.Connector)},
        {"A3_S1_A10_S1", ("Lake Yaochi Ruins to Grotto of Scriptures (Entry)", IPDKind.Connector)},
        {"A3_S1_A3_S7", ("Lake Yaochi Ruins to Yinglong Canals", IPDKind.Connector)},
        // A3_SG1: Left Subroom
        {"A3_SG1_[Variable] Picked2f0dfca8-7590-4804-9d4c-63744a851dfd", ("Lake Yaochi Ruins, Left Subroom - Immovable Jade", IPDKind.DropItem)},
        // A3_SG2: Right Subroom
        {"A3_SG2_[Variable] Picked3ff42e51-0d4a-4ced-84ca-43520be8fb2f", ("Lake Yaochi Ruins, Right Subroom - Soul Reaper Jade", IPDKind.DropItem)},
        // A3_SG4: Daybreak Tower
        {"A3_SG4_[Variable] Picked2a9ba109-3700-4e08-95a5-4226e91e01e4", ("Lake Yaochi Ruins, Daybreak Tower - Penglai Ballad reward (Pipe Vial)", IPDKind.DropItem)},

        // A3_S2: Greenhouse
        {"A3_S2_GreenHouse_Final_[Variable] Pickedfab9aced-8002-4d5f-ab46-1c9a06936ce4", ("Greenhouse - Miniboss (Avarice Jade)", IPDKind.Encyclopedia)},
        {"A3_S2_GreenHouse_Final_[Variable] Pickeded1c6448-cea6-43c7-99e1-b861a422144c", ("Greenhouse - Mutated Crops", IPDKind.Encyclopedia)},
        // Map Chip NPC -1689 -144 0
        {"A3_S2_GreenHouse_Final_[Variable] Picked087f3bea-fcd1-48bc-9c61-cb5590c53f9f", ("Greenhouse - Chest with explodey dudes (Basic Component)", IPDKind.DropItem)},
        {"A3_S2_GreenHouse_Final_MoneyCrateFlagb0ec8e30-c2b6-4b5a-9591-48f83de2be58", ("Greenhouse - Jin Chest top middle", IPDKind.MoneyCrate)},
        {"A3_S2_GreenHouse_Final_MoneyCrateFlagbedd789f-d0fb-4a43-ac57-d14bd6202411", ("Greenhouse - Jin Chest top right", IPDKind.MoneyCrate)},
        {"A3_S2_GreenHouse_Final_[Variable] Picked75562e82-3eba-43a5-ba19-f9899ae5f677", ("Greenhouse - Hidden in Vase (Unknown Seed)", IPDKind.DropItem)},
        {"A3_S2_GreenHouse_Final_[Variable] Pickedb0faa71f-2c27-43a4-b382-88d98f6e509c", ("Greenhouse - Yellow Water Report", IPDKind.Encyclopedia)},
        {"A3_S2_GreenHouse_Final_[Variable] Picked02e66a87-4ef0-476e-b2f9-92aa8df1491e", ("Greenhouse - Chest hidden next to broken elevator (Standard Component)", IPDKind.DropItem)},
        {"A3_S2_A3_S3", ("Greenhouse to Water & Oxygen Synthesis", IPDKind.Connector)},

        // A3_S3: Water & Oxygen Synthesis
        {"A3_S3_OxygenChamber_Final_[Variable] Pickedb6d0b6bc-15c5-4945-9471-fec6c473edd3", ("Water & Oxygen Synthesis - Chest near elevator remains (Herb Catalyst)", IPDKind.DropItem)},
        // MISSING CHEST -3757.625 -2848 0 Jin Chest big
        {"A3_S3_OxygenChamber_Final_[Variable] Pickedf1eeb10f-e851-4b32-b20f-1172e57285bb", ("Water & Oxygen Synthesis - Water Synthesis Pipeline Panel", IPDKind.Encyclopedia)},
        {"A3_S3_OxygenChamber_Final_[Variable] Picked994df5a4-716f-4a08-9730-161c9162fce2", ("Water & Oxygen Synthesis - Tao Fruit", IPDKind.DropItem)},
        {"A3_S3_OxygenChamber_Final_[Variable] Picked8c3c3017-8a7b-45b9-80da-d3c73e0beca7", ("Water & Oxygen Synthesis - Dusk Guardian Recording Device 2", IPDKind.Encyclopedia)},
        {"A3_S3_OxygenChamber_Final_MoneyCrateFlag5d8b61fe-0f32-483a-a904-4294d04c00bb", ("Water & Oxygen Synthesis - Jin Chest in rafters", IPDKind.MoneyCrate)},
        {"A3_S3_A3_S5", ("Water & Oxygen Synthesis to Agrarian Hall", IPDKind.Connector)},
        {"A3_S3_A3_S7", ("Water & Oxygen Synthesis to Yinglong Canal", IPDKind.Connector)},

        // A3_S5: Agrarian Hall
        {"A3_S5_BossGouMang_Final_[Variable] Picked8eb3e3b2-0036-418f-93de-d0695cbd0987", ("Agrarian Hall - Goumang reward (Ready-to-Eat Rations + Standard Component)", IPDKind.DropItem)},
        {"A3_S5_A3_S3", ("Agrarian Hall to Water & Oxygen Synthesis", IPDKind.Connector)},
        {"A3_S5_A10_S1", ("Agrarian Hall to Grotto of Scriptures (Entry)", IPDKind.Connector)},

        // A3_S7: Yinglong Canal
        {"A3_S7_DragonWay_Final_[Variable] Picked852c4a56-f020-4fcd-a386-b6f769cf72e8", ("Yinglong Canal - Big chest on top (Cultivation Jade)", IPDKind.DropItem)},
        {"A3_S7_DragonWay_Final_MoneyCrateFlag4b8ac072-d1b6-4eac-92ef-ebe3852534b3", ("Yinglong Canal - Jin Crate left of clean room", IPDKind.MoneyCrate)},
        {"A3_S7_DragonWay_Final_[Variable] Picked738a6f0e-5a52-4369-adb3-3fd70897da7c", ("Yinglong Canal - Clean room (Advanced Component)", IPDKind.DropItem)},
        {"A3_S7_DragonWay_Final_[Variable] Pickeda45d597d-c75e-410a-885b-2aba4a80ac48", ("Yinglong Canal - Farmland Markings", IPDKind.Encyclopedia)},
        {"A3_S7_DragonWay_Final_[Variable] Picked400ddccf-6635-4e23-8fbb-fb98e56570a2", ("Yinglong Canal - Golden Yinglong Egg", IPDKind.DropItem)},
        {"A3_S7_DragonWay_Final_[Variable] Pickedbb64a512-d249-4b1f-8bf7-6cf0b76ecb9c", ("Yinglong Canal - Chest below soup (Basic Component)", IPDKind.DropItem)},
        {"A3_S7_DragonWay_Final_MoneyCrateFlagbddd6c18-ed4f-48da-853e-eb6f138c42e9", ("Yinglong Canal - Jin Chest near Root Node", IPDKind.MoneyCrate)},
        {"A3_S7_A3_S1", ("Yinglong Canal to Lake Yaochi Ruins", IPDKind.Connector)},
        {"A3_S7_A3_S3", ("Yinglong Canal to Water & Oxygen Synthesis", IPDKind.Connector)},
        {"A3_S7_A11_S1", ("Yinglong Canal to Tiandao Research Center", IPDKind.Connector)},

        // A4_S1: Outer Warehouse
        {"A4_S2_RouteToControlRoom_Final_[Variable] Pickede08b2605-4d1f-4a4e-b864-8a105f7af52b", ("Outer Warehouse - Tao Fruit", IPDKind.DropItem)},
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked4e731e33-f14c-4fdb-8b33-a68c9a7ee4c8", ("Outer Warehouse - Dusk Guardian Recording Device 3", IPDKind.Encyclopedia)},
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Pickeda16ceeef-c56e-4189-81ec-cecafea7edb9", ("Outer Warehouse - Warehouse Database", IPDKind.Encyclopedia)},
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Picked26f8d6ec-3799-4df9-aa99-0816d2c5e686", ("Outer Warehouse - Dropped box with Nymph (Advanced Component)", IPDKind.DropItem)},
        // A4_S1_NewBridgeToWarehouse_Final_[Variable] Pickede8951183-10da-49e5-abe0-6547c01e9aa1 Top chest virtual reality, wrong location! Correct: -177.5 -2624 0
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Pickedea866f01-642a-4738-9c7c-31363f92f694", ("Outer Warehouse - Chest below transport line", IPDKind.DropItem)},
        {"A4_S1_NewBridgeToWarehouse_Final_MoneyCrateFlag25db906a-3065-49a3-a1b6-84ed75d19d14", ("Outer Warehouse - Jin Chest near Root Node", IPDKind.MoneyCrate)},
        //{"A4_S1_NewBridgeToWarehouse_Final_NPC Solvablef6102162-4024-4fcb-b815-9f1e930f399d", ("Outer Warehouse - Shanhai 9000", IPDKind.DropItem)}, Causes nullptr. Need different method?
        {"A4_S1_A4_SG7", ("Outer Warehouse to Subroom", IPDKind.Connector)},
        {"A4_S1_A4_S2", ("Outer Warehouse to Inner Warehouse", IPDKind.Connector)},
        {"A4_S1_A4_S6", ("Outer Warehouse to Yangu Hall", IPDKind.Connector)},
        {"A4_S1_A5_S1", ("Outer Warehouse to Factory (Great Hall)", IPDKind.Connector)},
        {"A4_S1_A6_S1", ("Outer Warehouse to Factory (Underground)", IPDKind.Connector)},
        // A4_SG7: Subroom
        {"A4_SG7_ZRoom_Arena_[Variable] Picked8ca3d9cc-26f8-4958-b507-0843324bc4e3", ("Outer Warehouse, Subroom - Breather Jade", IPDKind.DropItem)},

        // A4_S2: Inner Warehouse
        // A4_S2_RouteToControlRoom_Final_[Variable] Picked31b60681-27dd-4b68-b926-6f997c82781e Enemy dropped item, wrong location! Correct: -4509.125 -3376 0
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked31b60681-27dd-4b68-b926-6f997c82781e", ("Inner Warehouse - Miniboss reward (Firestorm Ring)", IPDKind.DropItem)},
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked39bc402c-d3ba-467a-89eb-e8ca0d60d18d", ("Inner Warehouse - Parry puzzle reward (Herb Catalyst)", IPDKind.DropItem)},
        {"A4_S2_A4_S1", ("Inner Warehouse to Outer Warehouse", IPDKind.Connector)},
        // A4_SG1: Subroom
        {"A4_SG1_[Variable] Picked871df5ec-61b5-4067-a074-d6e5ed2ceed5", ("Inner Warehouse, Subroom - Molted Tianma Hide", IPDKind.DropItem)},

        // A4_S3: Boundless Repository
        {"A4_S3_ControlRoom_Final_[Variable] Picked111b64cc-23fd-4b0d-b1df-f2893008b0b1", ("Boundless Repository - Chest past miniboss (Thunderburst Bomb)", IPDKind.DropItem)},
        {"A4_S3_ControlRoom_Final_[Variable] Picked06199e42-2157-4137-aba7-31c33f054b41", ("Boundless Repository - Ancient Weapon Console", IPDKind.Encyclopedia)},
        {"A4_S3_A4_SG4", ("Boundless Repository to Sealed Chamber (Subroom)", IPDKind.Connector)},
        {"A4_S3_A4_S2", ("Boundless Repository to Inner Warehouse", IPDKind.Connector)},
        {"A4_S3_A4_S4", ("Boundless Repository to Chase Sequence (Subroom)", IPDKind.Connector)},
        {"A4_S3_A4_S6", ("Boundless Repository to Yangu Hall", IPDKind.Connector)},
        // A4_S4: Boundless Repository, Chase Sequence
        {"A4_S4_Container_Final_MoneyCrateFlagfe419e8b-4a6a-4780-a2d4-ca17814811d3", ("Boundless Repository, Chase Sequence - Jin Chest room 1", IPDKind.MoneyCrate)},
        {"A4_S4_Container_Final_MoneyCrateFlag882bb707-0a28-4617-9ed5-d3ee23b99dc7", ("Boundless Repository, Chase Sequence - Jin Chest room 2 left", IPDKind.MoneyCrate)},
        {"A4_S4_Container_Final_MoneyCrateFlag2a945d87-332c-4d93-92f1-3629767e9250", ("Boundless Repository, Chase Sequence - Jin Chest room 2 middle", IPDKind.MoneyCrate)},
        {"A4_S4_Container_Final_MoneyCrateFlag9b37e76e-5f57-4c79-8020-8b0a5d2bee41", ("Boundless Repository, Chase Sequence - Jin Chest room 2 right", IPDKind.MoneyCrate)},
        // A4_SG4: Boundless Repository, Sealed Chamber
        {"A4_SG4_[Variable] Picked5e5c0008-7a05-4528-a7a7-1cb43b437ddb", ("Boundless Repository, Sealed Chamber - Chest 1", IPDKind.DropItem)},
        {"A4_SG4_[Variable] Pickedad2a9fa0-3e02-478c-a33b-59d0bd7af7a6", ("Boundless Repository, Sealed Chamber - Chest 2", IPDKind.DropItem)},
        {"A4_SG4_[Variable] Picked108b9d73-3ad1-44cd-9271-8edfe83886bb", ("Boundless Repository, Sealed Chamber - Hexachrem Vault Scroll", IPDKind.Encyclopedia)},
        {"A4_SG4_[Variable] Picked111b64cc-23fd-4b0d-b1df-f2893008b0b1", ("Boundless Repository, Sealed Chamber - Chest 3", IPDKind.DropItem)},
        // A4_SG4_[Variable] Picked39047b0a-4a21-4f81-8b0e-a333de6bd4e1 Chest 4, wrong location! Correct: 2823.5 -5744 0


        // A4_S6: Yangu Hall
        {"A4_S6_DaoBase_Final_[Variable] IsFlowerPicked18a7e188-ce20-495f-8ff9-49b2b0a78515", ("Yangu Hall - Boss reward (Greater Tao Fruit)", IPDKind.DropItem)},

        // A5_S1: Factory (Great Hall)
        {"A4_S1_NewBridgeToWarehouse_Final_NPC Solvablef6102162-4024-4fcb-b815-9f1e930f399d", ("Outer Warehouse - Shanhai 9000", IPDKind.DropItem)},
        {"A5_S1_CastleHub_remake_[Variable] Picked6575c205-b84b-4fd2-9f20-6c6d83aff439", ("Factory (Great Hall) - Transmutation Furnace Monitor", IPDKind.Encyclopedia)},
        {"A5_S1_CastleHub_remake_[Variable] Picked6eef7fd0-7feb-493f-80ab-849699cc874b", ("Factory (Great Hall) - Tao Fruit under hammer bros", IPDKind.DropItem)},
        {"A5_S1_CastleHub_remake_MoneyCrateFlag7950dc61-26eb-4740-a8c4-f0c1c4087c16", ("Factory (Great Hall) - Jin Chest near drop to Underground", IPDKind.MoneyCrate)},
        // MISSING CHEST 1980.125 -3952 0 Chest standard component. Maybe offscreen wrong loc?
        {"A5_S1_CastleHub_remake_[Variable] Pickedeb34355c-2e45-4d64-b716-6d1f50761656", ("Factory (Great Hall) - Chest at bottom right (Basic Component)", IPDKind.DropItem)},
        {"A5_S1_A4_S1", ("Factory (Great Hall) to Outer Warehouse", IPDKind.Connector)},
        {"A5_S1_A6_S1", ("Factory (Great Hall) to Factory (Underground)", IPDKind.Connector)},
        {"A5_S1_A7_S1", ("Factory (Great Hall) to Cortex Center", IPDKind.Connector)},

        // A5_S2: Prison
        {"A5_S2_Jail_Remake_Final_[Variable] Picked7c7fbff1-62de-4ed0-9a69-4b328315a76c", ("Prison - Miniboss reward (Noble Ring)", IPDKind.DropItem)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlag468d5e12-8853-46b9-b96c-3ffd8d8be0bc", ("Prison - Escape reward Jin Chest 2", IPDKind.MoneyCrate)},
        {"A5_S2_Jail_Remake_Final_[Variable] Pickedd72d6afb-71b2-46c6-9dd4-6a0f3a0001fa", ("Prison - Escape reward Chest", IPDKind.DropItem)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlagee52d134-b47a-4269-8484-88588a15f0f9", ("Prison - Escape reward Jin Chest 1", IPDKind.MoneyCrate)},
        {"A5_S2_Jail_Remake_Final_[Variable] Pickedcb293a54-c643-4cf4-99ad-6e7203ce153b", ("Prison - Prisoner's Bamboo Scroll (II)", IPDKind.Encyclopedia)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlag8c449e70-479a-4dd7-830d-1da255d8bda3", ("Prison - Chest top right (Standard Component)", IPDKind.DropItem)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlagba51acb0-87d1-4aae-862d-d61e30eb2112", ("Prison - Jin Chest left of statue", IPDKind.MoneyCrate)},
        {"A5_S2_Jail_Remake_Final_[Variable] Picked8f746de4-30c8-45c9-9c69-33ce39d72a69", ("Prison - Chest bottom left", IPDKind.DropItem)},
        {"A5_S2_Jail_Remake_Final_[Variable] Pickeda4c0a164-ab17-4445-b889-ed495cdd4b3e", ("Prison - Prisoner's Bamboo Scroll (I)", IPDKind.Encyclopedia)},
        {"A5_S2_A5_S3", ("Prison to Factory (Machine Room)", IPDKind.Connector)},

        // A5_S3: Factory (Machine Room)
        {"A5_S3_UnderCastle_Remake_4wei_MoneyCrateFlag80735180-69fd-4718-8b45-0c44b544ad56", ("Factory (Machine Room) - Jin Chest near elevator top", IPDKind.MoneyCrate)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked37adc43b-6e63-4f05-8641-7fa1e034a9ac", ("Factory (Machine Room) - Greater Tao Fruit", IPDKind.DropItem)},
        {"A5_S3_UnderCastle_Remake_4wei_MoneyCrateFlag1ca7c85f-f131-469a-b6d5-56b652ff6f3b", ("Factory (Machine Room) - Jin Chest top", IPDKind.MoneyCrate)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked268a9df6-e099-49e4-a807-b40a3009b39b", ("Factory (Machine Room) - Walking Chest", IPDKind.DropItem)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked4d19d7f5-0f37-44d3-bb5c-7c73425083c0", ("Factory (Machine Room) - Chest behind red parry right (Herb Catalyst)", IPDKind.DropItem)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked345ea8d3-2391-437d-b605-2ba1b5d31592", ("Factory (Machine Room) - Chest behind red parry left", IPDKind.DropItem)},
        {"A5_S3_UnderCastle_Remake_4wei_MoneyCrateFlag484a0068-3348-44f8-9d4c-3546f74e818a", ("Factory (Machine Room) - Jin Chest near bottom elevator", IPDKind.MoneyCrate)},
        {"A5_S3_A5_S2", ("Factory (Machine Room) to Prison", IPDKind.Connector)},
        {"A5_S3_A6_S1", ("Factory (Machine Room) to Factory (Underground)", IPDKind.Connector)},

        // A6_S1: Factory (Underground)
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Pickeddc5cd101-21bd-4738-a3d4-48416158a2cb", ("Factory (Underground) - Chest near top elevator (Standard Component)", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_MoneyCrateFlaga9f9b1fe-5fa9-4ece-9e6b-81b60d88b2b8", ("Factory (Underground) - Jin Chest bottom left", IPDKind.MoneyCrate)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Picked3b68c09d-7491-416b-8988-77ff63276220", ("Factory (Underground) - Chest above Root Node (Basic Component)", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_MoneyCrateFlag93d51245-cf6e-4c8e-af18-8fc715c9c3a0", ("Factory (Underground) - Jin chest near downward elevator", IPDKind.MoneyCrate)},
        // Parry puzzle bot 5472 -7952 0
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Pickedbbeb3b5d-b0e4-4c6f-9ef5-0add24781c9d", ("Factory (Underground) - Parry puzzle reward", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Pickededbeab36-c776-4d36-9a13-8cec36d57999", ("Factory (Underground) - Miniboss reward (Standard Component)", IPDKind.DropItem)},
        {"A6_S1_A4_S1", ("Factory (Underground) to Outer Warehouse", IPDKind.Connector)},
        {"A6_S1_A5_S1", ("Factory (Underground) to Factory (Great Hall)", IPDKind.Connector)},

        // A7_S1: Cortex Center
        {"A7_S1_BrainRoom_Remake_[Variable] Picked0b37affd-62a8-4cc5-b016-8926d8bf3b21", ("Cortex Center - Chest near exit", IPDKind.DropItem)},
        {"A7_S1_BrainRoom_Remake_MoneyCrateFlag93a7e61c-c9ac-43d8-818c-e374860c848c", ("Cortex Center - Jin Chest across spikes", IPDKind.MoneyCrate)},
        {"A7_S1_BrainRoom_Remake_MoneyCrateFlag406ea702-4cd4-4eed-888a-11e8769fa102", ("Cortex Center - Jin Chest top", IPDKind.MoneyCrate)},
        {"A7_S1_AG_S1", ("Cortex Center to New Kunlun Central Hall", IPDKind.Connector)},

        // A10_S1: Grotto of Scriptures (Entry)
        {"A10_S1_TombEntrance_remake_[Variable] Picked18a5cc6a-9fcc-44c0-aa8f-5e14bd6ad3e4", ("Grotto of Scriptures (Entry) - Coffin Inscription", IPDKind.Encyclopedia)},
        {"A10_S1_TombEntrance_remake_[Variable] Pickedd0049033-8af9-4657-8046-3b31d94153ee", ("Grotto of Scriptures (Entry) - Chest above glass ceiling (Qiankun Board)", IPDKind.DropItem)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag5fb4bef3-fdcb-4b42-b6c8-84823e38a322", ("Grotto of Scriptures (Entry) - Jin Chest at teleport door right 3", IPDKind.MoneyCrate)},
        // Maybe remove chest 2?
        {"A10_S1_TombEntrance_remake_[Variable] Picked6c059e7f-241f-4cbb-b678-0f663bc61dfa", ("Grotto of Scriptures (Entry) - Chest at teleport door right ()", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag8cef9deb-2e71-4e0f-9048-1d08932110f8", ("Grotto of Scriptures (Entry) - Jin Chest at teleport door right 2", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag784dca53-caf7-4cbb-b496-8bf23dd62a4f", ("Grotto of Scriptures (Entry) - Jin Chest at teleport door right 1", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag292ce9cd-c4ff-4a01-bc4e-bb9d870fe15e", ("Grotto of Scriptures (Entry) - Jin Chest right of rafters", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag7ce9f944-3134-4ca0-bc5f-bbd4b2361acc", ("Grotto of Scriptures (Entry) - Jin Chest left of rafters", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_[Variable] Picked07f3b10d-9898-4ca5-95ad-db4eb15cc565", ("Grotto of Scriptures (Entry) - Chest at teleport door bottom (Dark Steel)", IPDKind.DropItem)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag1d7fbaf8-1d57-4c9c-b825-651f3f9abf41", ("Grotto of Scriptures (Entry) - Jin Chest near spikes", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_[Variable] Picked3b7c610d-f1aa-46bd-bf15-f71f24750ded", ("Grotto of Scriptures (Entry) - Ancient Cave Painting", IPDKind.Encyclopedia)},
        {"A10_S1_A3_S1", ("Grotto of Scriptures (Entry) to Lake Yaochi Ruins", IPDKind.Connector)},
        {"A10_S1_A3_S2", ("Grotto of Scriptures (Entry) to Greenhouse", IPDKind.Connector)},
        {"A10_S1_A3_S5", ("Grotto of Scriptures (Entry) to Agrarian Hall", IPDKind.Connector)},

        // A11_S1: Tiandao Research Center
        {"A11_S1_Hospital_remake_[Variable] Picked1a9d1d74-32ad-41e2-8897-48b6514d9976", ("Tiandao Research Center - Tiandao Academy Periodical", IPDKind.DropItem)},
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
