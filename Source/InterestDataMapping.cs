using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class InterestDataMapping {
    public const long DISABLED_AP = -1;
    public enum IPDKind {
        Unknown,
        MoneyCrate,
        DropItem,
        Encyclopedia,
        Connector,
        Miniboss,
        ArchipelagoNormal,
        ArchipelagoProgression,
    }
    public class IPDInfo(String _name, IPDKind _kind) {
        public String name = _name;
        public IPDKind kind = _kind;
        public long AP_ID = DISABLED_AP;
        public IPDInfo(String _name, IPDKind _kind, long _id) : this(_name, _kind) { AP_ID = _id; }
        public static implicit operator IPDInfo((String _name, IPDKind _kind, long _id) a) => new(a._name, a._kind, a._id);
        public static implicit operator IPDInfo((String _name, IPDKind _kind) a) => new(a._name, a._kind);
    }

    public static bool IsValidLocation(InterestPointData IPD) {
        if (IPD_TABLE.ContainsKey(IPD.name)) return true; // Hand-picked values always OK
        #if DEBUG
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
        #endif
        return false;
    }

    public static Sprite? GetLocSprite(IPDKind kind) {
        return kind switch {
            IPDKind.DropItem => AssetManager.ChestSprite,
            IPDKind.MoneyCrate => AssetManager.MoneySprite,
            IPDKind.Encyclopedia => AssetManager.EncyclopediaSprite,
            IPDKind.Connector => AssetManager.ConnectorSprite,
            IPDKind.Miniboss => AssetManager.MinibossSprite,
            IPDKind.ArchipelagoNormal => AssetManager.APSprite,
            IPDKind.ArchipelagoProgression => AssetManager.APProgSprite,
            IPDKind.Unknown => AssetManager.ChestSprite, // TODO maybe questionmark?
            _ => throw new NotImplementedException(),
        };
    }

    public static readonly Dictionary<String, IPDInfo> IPD_TABLE = new()
    {
        // AG_ST: New Kunlun Control Hub
        {"AG_ST_Hub_[Variable] Pickede97fc5b2-d54b-4c9d-a04f-1deae0fc302a", ("New Kunlun Control Hub - Root Core Monitoring Device", IPDKind.Encyclopedia, DISABLED_AP)},
        {"AG_ST_AG_S1", ("New Kunlun Control Hub to New Kunlun Central Hall", IPDKind.Connector, DISABLED_AP)},

        // AG_S1: New Kunlun Central Hall
        {"AG_S1_SenateHall_[Variable] Picked7c593684-75c0-47b9-97f2-c8b91a58b991", ("Central Hall: Examine Council Tenets", IPDKind.Encyclopedia, 403)},
        {"AG_S1_SenateHall_[Variable] Picked534fac80-3271-4629-ab19-d5afd3e798a3", ("Central Hall: Vents", IPDKind.DropItem, 405)},
        {"AG_S1_SenateHall_[Variable] Pickedd21f4407-1618-4b7b-8771-6620becf19ab", ("Central Hall: Examine Council Sign", IPDKind.Encyclopedia, 401)},
        {"AG_S1_SenateHall_[Variable] Pickedc8fcde14-8173-489d-9441-ecabac1ab50f", ("Central Hall: Examine Launch Memoral", IPDKind.Encyclopedia, 402)},
        {"AG_S1_AG_ST", ("New Kunlun Central Hall to New Kunlun Control Hub", IPDKind.Connector)},
        {"AG_S1_AG_S2", ("New Kunlun Central Hall to Four Seasons Pavilion", IPDKind.Connector)},
        {"AG_S1_A2_S6", ("New Kunlun Central Hall to Central Transport Hub", IPDKind.Connector)},
        {"AG_S1_A3_S1", ("New Kunlun Central Hall to Lake Yaochi Ruins", IPDKind.Connector)},
        {"AG_S1_A7_S1", ("New Kunlun Central Hall to Cortex Center", IPDKind.Connector)},

        // AG_S2: Four Seasons Pavilion
        {"AG_S2_YiBase_[Variable] Picked9dcc2b3f-a1bf-4837-bb06-7875498781c7", ("FSP: Half-Grown Tree Chest", IPDKind.DropItem, 510)},
        {"AG_S2_YiBase_[Variable] Picked479cd19b-95e3-4e08-864c-9042d4688ba9", ("Four Seasons Pavilion - Second Floor Chest 1", IPDKind.DropItem)},
        {"AG_S2_YiBase_[Variable] Pickedd2b3e894-4292-4efe-a627-f8c7e38d4891", ("Four Seasons Pavilion - Second Floor Chest 2", IPDKind.DropItem)},
        {"AG_S2_AG_S1", ("Four Seasons Pavilion to New Kunlun Central Hall", IPDKind.Connector)},

        // A0_S7: Underground Cave
        {"A0_S7_CaveReturned_[Variable] Pickeded3de528-ecbf-4180-be47-fa647646cb4f", ("Underground Cave - Yellow Snake", IPDKind.DropItem)},
        {"A0_S7_CaveReturned_[Variable] Picked_竹簡 (A0_S4共用)", ("Underground Cave - Camp Scroll", IPDKind.Encyclopedia)},
        {"A0_S7_CaveReturned_[Variable] Picked_山洞屍體 (A0_S4共用)", ("Underground Cave - Dead Person's Note", IPDKind.Encyclopedia)},
        {"A0_S7_CaveReturned_[Variable] Pickede1ca6546-ee89-44d5-8f44-9c3f2f8475c4", ("Underground Cave - Cave Stone Inscription", IPDKind.Encyclopedia)},
        {"A0_S7_A6_S3", ("Underground Cave to Abandoned Mines", IPDKind.Connector)},
        {"A0_S7_A0_S8", ("Underground Cave to 95th Livestock Pen", IPDKind.Connector)},

        // A0_S8: 95th Livestock Pen
        {"A0_S8_A0_S7", ("95th Livestock Pen to Underground Cave", IPDKind.Connector)},
        {"A0_S8_A0_S9", ("95th Livestock Pen to Livestock Harvesting Platform", IPDKind.Connector)},

        // A0_S9: Livestock Harvesting Platform
        {"A0_S9_AltarReturned_[Variable] Picked8cc70dad-1c5d-45ab-b37a-c3ad3bc769f4", ("Livestock Harvesting Platform - Miniboss reward", IPDKind.Miniboss)},
        {"A0_S9_A0_S8", ("Livestock Harvesting Platform to 95th Livestock Pen", IPDKind.Connector)},
        {"A0_S9_A0_S10", ("Livestock Harvesting Platform to Galactic Dock", IPDKind.Connector)},

        // A0_S10: Galactic Dock
        {"A0_S10_SpaceshipYard_[Variable] Picked5dca2439-7a4c-4e9e-9e66-a0282319627b", ("Galactic Dock - Tao Fruit", IPDKind.DropItem)},
        {"A0_S10_SpaceshipYard_[Variable] Pickeddac9a5c3-6ace-48c8-8bbf-7d6b55360d7f", ("Galactic Dock - Galactic Dock Sign", IPDKind.Encyclopedia)},
        {"A0_S10_A0_S9", ("Galactic Dock to Livestock Harvesting Platform", IPDKind.Connector)},
        {"A0_S10_A2_S6", ("Galactic Dock to Central Transport Hub", IPDKind.Connector)},

        // A1_S1: Apeman Facility (Monitoring)
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag27cfe3ad-4c1d-4d51-bcee-80e5c7bebf24", ("AF (Monitoring): Upper Right", IPDKind.MoneyCrate, 102)},
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag71961019-5ce4-4bcb-834b-c5b84360eb4a", ("AF (Monitoring): Lower Vent", IPDKind.MoneyCrate, 103)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedb7eb8443-41b9-42cc-b809-0ff7dd487e96", ("AF (Monitoring): Break Corpse", IPDKind.DropItem, 101)},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedf78b77c1-de42-4a36-87ce-255804e9826d", ("AF (Monitoring): Examine Apeman Surveillance", IPDKind.Encyclopedia, 104)},
        {"A1_S1_A1_S2", ("Apeman Facility (Monitoring) to Apeman Facility (Elevator)", IPDKind.Connector)},

        // A1_S2: Apeman Facility (Elevator)
        {"A1_S2_ConnectionToElevator_Final_[Variable] Picked197f5bc8-8c52-444c-ace3-bff4d4ecf7ad", ("AF (Elevator): Hack Statue", IPDKind.DropItem, 205)},
        {"A1_S2_ConnectionToElevator_Final_MoneyCrateFlagcc1fb972-f2d3-4d52-9034-c714ddc16fbb", ("Apeman Facility (Elevator) - Jin Chest bottom middle", IPDKind.MoneyCrate)},
        {"A1_S2_ConnectionToElevator_Final_MoneyCrateFlage2079e70-0e6f-4c0c-a408-43b35db10eb4", ("AF (Elevator): Hidden Atop Upper Level Pagoda (Left Chest)", IPDKind.MoneyCrate, 201)},
        {"A1_S2_ConnectionToElevator_Final_MoneyCrateFlag9323d7ea-46f3-4d59-977f-ff7017f38bb5", ("AF (Elevator): Hidden Atop Upper Level Pagoda (Right Chest)", IPDKind.MoneyCrate, 207)},
        {"A1_S2_ConnectionToElevator_Final_MoneyCrateFlag40644c8a-0872-44b2-9a45-d00be083704d", ("Apeman Facility (Elevator) - Jin Chest in center", IPDKind.MoneyCrate)},
        {"A1_S2_ConnectionToElevator_Final_[Variable] Pickedcd29251b-2344-4eeb-a7ba-6e34283f3764", ("AF (Elevator): Elevator Shaft", IPDKind.MoneyCrate, 203)},
        {"A1_S2_ConnectionToElevator_Final_[Variable] Picked11a75a1a-15b0-48d4-ba71-f848ba9d869d", ("AF (Elevator): Defeat Red Tiger Elite: Baichang", IPDKind.Miniboss, 204)},
        {"A1_S2_A1_S1", ("Apeman Facility (Elevator) to Apeman Facility (Monitoring)", IPDKind.Connector)},
        {"A1_S2_A1_S3", ("Apeman Facility (Elevator) to Apeman Facility (Depths)", IPDKind.Connector)},
        {"A1_S2_A2_S6", ("Apeman Facility (Elevator) to Central Transport Hub", IPDKind.Connector)},

        // A1_S3: Apeman Facility (Depths)
        {"A1_S3_InnerHumanDisposal_Final_MoneyCrateFlag7aae1bfb-7415-4e03-b8c8-0e593375aff5", ("Apeman Facility (Depths) - Jin Chest below root node", IPDKind.MoneyCrate)},
        {"A1_S3_InnerHumanDisposal_Final_[Variable] Picked377bcd9c-fa57-4c7b-8df1-ae20569fdd6c", ("Apeman Facility (Depths) - Chest top platforming section", IPDKind.DropItem)},
        {"A1_S3_InnerHumanDisposal_Final_[Variable] Pickeded76b409-ea0c-40ca-b7cf-9902da09c6a4", ("Apeman Facility (Depths) - Miniboss reward", IPDKind.Miniboss)},
        {"A1_S3_InnerHumanDisposal_Final_[Variable] Picked2ffa4954-a06a-499f-abe8-d6720db93633", ("Apeman Facility (Depths) - Chest top of map", IPDKind.DropItem)},
        {"A1_S3_InnerHumanDisposal_Final_[Variable] Picked1d9a273a-d817-4734-b0ae-8effef275a71", ("AF (Depths): Tianhou Flower Under Elevator", IPDKind.DropItem, 308)},
        {"A1_S3_InnerHumanDisposal_Final_[Variable] Picked1262b326-7739-4f95-90b9-37a45c74c87c", ("Apeman Facility (Depths) - Parry puzzle reward", IPDKind.DropItem)},
        {"A1_S3_InnerHumanDisposal_Final_[Variable] Picked845baff4-7334-48a1-97c2-b69652cbd95f", ("Apeman Facility (Depths) - Chest behind breakable wall bottom", IPDKind.DropItem)},
        {"A1_S3_InnerHumanDisposal_Final_MoneyCrateFlag37b820ea-4a5a-40a2-a581-767d5362ed5f", ("Apeman Facility (Depths) - Jin Chest bottom middle", IPDKind.MoneyCrate)},
        {"A1_S3_A1_S2", ("Apeman Facility (Depths) to Apeman Facility (Elevator)", IPDKind.Connector)},
        {"A1_S3_A2_S3", ("Apeman Facility (Depths) to Power Reservoir (West)", IPDKind.Connector)},
        {"A1_S3_A6_S1", ("Apeman Facility (Depths) to Factory (Underground)", IPDKind.Connector)},

        // A2_S1: Power Reservoir (Central)
        {"A2_S1_ReactorMiddle_Final_[Variable] Pickeda21f893b-20be-4c37-afaf-3529b441641f", ("Power Reservoir (Central) - Chest left of center", IPDKind.DropItem)},
        {"A2_S1_ReactorMiddle_Final_[Variable] Pickedcf40600c-ea9a-46fd-8137-9d080827c353", ("PR (Central): Examine Energy Meter", IPDKind.Encyclopedia, 907)},
        {"A2_S1_ReactorMiddle_Final_NPC Solvable84c24574-9409-4636-b0b9-368db7447ef9", ("PR (Central): Retrieve Chip From Shanhai 9000", IPDKind.DropItem, 906)},
        {"A2_S1_ReactorMiddle_Final_[Variable] Picked068fb326-30a8-4b21-8027-975494d830ef", ("Power Reservoir (Central) - Chest left side", IPDKind.DropItem)},
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
        {"A2_S2_ReactorRight_Final_MoneyCrateFlag9282cfd0-1b21-43bf-83a4-68788aa4734c", ("Power Reservoir (East) - Jin chest near right entry", IPDKind.MoneyCrate)},
        {"A2_S2_ReactorRight_Final_[Variable] Pickedf37a555f-9c4e-49ad-a9bd-14ccb57a87b7", ("Power Reservoir (East) - Mini Boss", IPDKind.Miniboss)},
        {"A2_S2_ReactorRight_Final_MoneyCrateFlagf0153130-df56-45de-9e0c-2ac743d30e7d", ("Power Reservoir (East) - Jin Chest", IPDKind.DropItem)},
        // Parry puzzle on right side next to reward
        {"A2_S2_ReactorRight_Final_[Variable] Picked0c38212b-59e4-4171-a5c7-d7a17ca595bc", ("Power Reservoir (East) - Parry Puzzle reward (Basic Component)", IPDKind.DropItem)},
        {"A2_S2_ReactorRight_Final_[Variable] Picked026fbdc5-1f03-4179-b515-2d46a223320e", ("Power Reservoir (East) - Standard Component", IPDKind.DropItem)},
        {"A2_S2_ReactorRight_Final_[Variable] Pickedccefd541-2631-4303-8ec5-1f23a435caff", ("Power Reservoir (East) - Pauper Jade", IPDKind.Connector)},
        {"A2_S2_A2_S1", ("Power Reservoir (East) to Power Reservoir (Central)", IPDKind.Connector)},
        {"A2_S2_A2_S6", ("Power Reservoir (East) to Central Transport Hub", IPDKind.Connector)},

        // A2_S3: Power Reservoir (West)
        {"A2_S3_ReactorLeft_Final_MoneyCrateFlagec4d369c-3f41-48c5-9cb9-fa73fe2538a8", ("Power Reservoir (West) - Jin Chest top right", IPDKind.MoneyCrate)},
        {"A2_S3_ReactorLeft_Final_[Variable] Pickedda45e2f2-f9c8-451c-971b-5d7a5dc14012", ("Power Reservoir (West) - Parry puzzle reward", IPDKind.DropItem)},
        {"A2_S3_ReactorLeft_Final_[Variable] Picked2752954a-64fc-454a-99cd-00820b9ad9d1", ("PR (West): Dusk Guardian Recording Device", IPDKind.Encyclopedia, 1005)},
        {"A2_S3_ReactorLeft_Final_[Variable] Picked37adc43b-6e63-4f05-8641-7fa1e034a9ac", ("PR (West): Tianhou Flower", IPDKind.DropItem, 1006)},
        {"A2_S3_ReactorLeft_Final_MoneyCrateFlag03e9ea2f-20b0-4f36-95d1-882a05cb29f1", ("Power Reservoir (West) - Jin Chest tunnel top left", IPDKind.MoneyCrate)},
        {"A2_S3_ReactorLeft_Final_[Variable] Picked1ee72d3f-7344-4052-b528-593dfceaaf39", ("PR (West): Guarded By Turret", IPDKind.DropItem, 1002)},
        {"A2_S3_ReactorLeft_Final_MoneyCrateFlag944372cd-7a31-4020-9177-3aab3a11b690", ("Power Reservoir (West) - Jin Chest bottom left", IPDKind.MoneyCrate)},
        {"A2_S3_A1_S3", ("Power Reservoir (West) to Apeman Facility (Depths)", IPDKind.Connector)},
        {"A2_S3_A2_S1", ("Power Reservoir (West) to Power Reservoir (Central)", IPDKind.Connector)},

        // A2_S5: Radiant Pagoda
        {"A2_S5_BossHorseman_Final_[Variable] Picked7637cf70-7b5f-430b-bd2f-eea133a5c780", ("RP: Defeat General Yingzhao", IPDKind.DropItem, 1102)},
        {"A2_S5_BossHorseman_Final_[Variable] Picked165c38cf-fff8-4280-8a76-06e3740f203e", ("RP: Examine Radiant Pagoda Control Panel", IPDKind.Encyclopedia, 1101)},
        {"A2_S5_A2_S1", ("Radiant Pagoda to Power Reservoir (Central)", IPDKind.Connector)},

        // A2_S6: Central Transport Hub
        // 6560 -7720 Parry Puzzle, other one on right side next to puzzle reward
        {"A2_S6_LogisticCenter_Final_[Variable] Picked5067bec8-9b41-4ec4-b519-7ce767f84062", ("Central Transport Hub - Parry Puzzle reward (Shuanshuan gift)", IPDKind.DropItem)},
        {"A2_S6_LogisticCenter_Final_[Variable] Picked80a94da9-9df5-4178-969d-1c034d7f6c62", ("Central Transport Hub - Red Tiger Elite: Yanren", IPDKind.Miniboss)},
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
        {"A3_S1_A3_S7", ("Lake Yaochi Ruins to Yinglong Canals", IPDKind.Connector)},
        {"A3_S1_A9_S4", ("Lake Yaochi Ruins to Sky Tower", IPDKind.Connector)},
        {"A3_S1_A10_S1", ("Lake Yaochi Ruins to Grotto of Scriptures (Entry)", IPDKind.Connector)},
        // A3_SG1: Left Subroom
        {"A3_SG1_[Variable] Picked2f0dfca8-7590-4804-9d4c-63744a851dfd", ("Lake Yaochi Ruins, Left Subroom - Immovable Jade", IPDKind.DropItem)},
        // A3_SG2: Right Subroom
        {"A3_SG2_[Variable] Picked3ff42e51-0d4a-4ced-84ca-43520be8fb2f", ("Lake Yaochi Ruins, Right Subroom - Soul Reaper Jade", IPDKind.DropItem)},
        // A3_SG4: Daybreak Tower
        {"A3_SG4_[Variable] Picked2a9ba109-3700-4e08-95a5-4226e91e01e4", ("Lake Yaochi Ruins, Daybreak Tower - Penglai Ballad reward (Pipe Vial)", IPDKind.DropItem)},

        // A3_S2: Greenhouse
        {"A3_S2_GreenHouse_Final_[Variable] Pickedfab9aced-8002-4d5f-ab46-1c9a06936ce4", ("Greenhouse: Defeat Celestial Spectre: Shuigui", IPDKind.Miniboss, 1309)},
        {"A3_S2_GreenHouse_Final_[Variable] Pickeded1c6448-cea6-43c7-99e1-b861a422144c", ("Greenhouse: Examine Mutated Crops", IPDKind.Encyclopedia, 1308)},
        {"A3_S2_GreenHouse_Final_NPC Solvable8c866b75-fda1-40ff-b0ab-5cff852cbf24", ("Greenhouse: Retrieve Chip From Shanhai 9000", IPDKind.DropItem, 1303)},
        {"A3_S2_GreenHouse_Final_[Variable] Picked087f3bea-fcd1-48bc-9c61-cb5590c53f9f", ("Greenhouse - Chest with explodey dudes (Basic Component)", IPDKind.DropItem)},
        {"A3_S2_GreenHouse_Final_MoneyCrateFlagb0ec8e30-c2b6-4b5a-9591-48f83de2be58", ("Greenhouse - Jin Chest top middle", IPDKind.MoneyCrate)},
        {"A3_S2_GreenHouse_Final_MoneyCrateFlagbedd789f-d0fb-4a43-ac57-d14bd6202411", ("Greenhouse - Jin Chest top right", IPDKind.MoneyCrate)},
        {"A3_S2_GreenHouse_Final_[Variable] Picked75562e82-3eba-43a5-ba19-f9899ae5f677", ("Greenhouse: Vase In Water Above Root Node", IPDKind.DropItem, 1306)},
        {"A3_S2_GreenHouse_Final_[Variable] Pickedb0faa71f-2c27-43a4-b382-88d98f6e509c", ("Greenhouse: Examine Water Report", IPDKind.Encyclopedia, 1302)},
        {"A3_S2_GreenHouse_Final_[Variable] Picked02e66a87-4ef0-476e-b2f9-92aa8df1491e", ("Greenhouse - Chest hidden next to broken elevator (Standard Component)", IPDKind.DropItem)},
        {"A3_S2_A3_S3", ("Greenhouse to Water & Oxygen Synthesis", IPDKind.Connector)},

        // A3_S3: Water & Oxygen Synthesis
        {"A3_S3_OxygenChamber_Final_[Variable] Pickedb6d0b6bc-15c5-4945-9471-fec6c473edd3", ("Water & Oxygen Synthesis - Chest near elevator remains (Herb Catalyst)", IPDKind.DropItem)},
        // MISSING CHEST -3757.625 -2848 0 Jin Chest big
        {"A3_S3_OxygenChamber_Final_[Variable] Pickedf1eeb10f-e851-4b32-b20f-1172e57285bb", ("W&OS: Examine Pipeline Panel", IPDKind.Encyclopedia, 1405)},
        {"A3_S3_OxygenChamber_Final_[Variable] Picked994df5a4-716f-4a08-9730-161c9162fce2", ("W&OS: Tianhou Flower", IPDKind.DropItem, 1403)},
        {"A3_S3_OxygenChamber_Final_[Variable] Picked8c3c3017-8a7b-45b9-80da-d3c73e0beca7", ("W&OS: Dusk Guardian Recording Device", IPDKind.Encyclopedia, 1404)},
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
        {"A3_S7_DragonWay_Final_[Variable] Pickeda45d597d-c75e-410a-885b-2aba4a80ac48", ("Yinglong Canal: Examine Farmland Markings", IPDKind.Encyclopedia, 1603)},
        {"A3_S7_DragonWay_Final_[Variable] Picked400ddccf-6635-4e23-8fbb-fb98e56570a2", ("Yinglong Canal: Break Center Yinglong Egg", IPDKind.DropItem, 1606)},
        {"A3_S7_DragonWay_Final_[Variable] Pickedbb64a512-d249-4b1f-8bf7-6cf0b76ecb9c", ("Yinglong Canal - Chest below soup (Basic Component)", IPDKind.DropItem)},
        {"A3_S7_DragonWay_Final_MoneyCrateFlagbddd6c18-ed4f-48da-853e-eb6f138c42e9", ("Yinglong Canal - Jin Chest near Root Node", IPDKind.MoneyCrate)},
        {"A3_S7_A3_S1", ("Yinglong Canal to Lake Yaochi Ruins", IPDKind.Connector)},
        {"A3_S7_A3_S3", ("Yinglong Canal to Water & Oxygen Synthesis", IPDKind.Connector)},
        {"A3_S7_A11_S1", ("Yinglong Canal to Tiandao Research Center", IPDKind.Connector)},

        // A4_S1: Outer Warehouse
        {"A4_S1_NewBridgeToWarehouse_Final_NPC Solvablef6102162-4024-4fcb-b815-9f1e930f399d", ("OW: Retrieve Chip From Shanhai 9000", IPDKind.DropItem, 1901)},
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Pickeda16ceeef-c56e-4189-81ec-cecafea7edb9", ("OW: Examine Warehouse Database", IPDKind.Encyclopedia, 1907)},
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Picked26f8d6ec-3799-4df9-aa99-0816d2c5e686", ("OW: Inside Crate Dropped From Vent Hack", IPDKind.DropItem, 1906)},
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Pickede8951183-10da-49e5-abe0-6547c01e9aa1", ("Outer Warehouse - Top chest virtual reality", IPDKind.DropItem)},
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Pickedea866f01-642a-4738-9c7c-31363f92f694", ("Outer Warehouse - Chest below transport line", IPDKind.DropItem)},
        {"A4_S1_NewBridgeToWarehouse_Final_MoneyCrateFlag25db906a-3065-49a3-a1b6-84ed75d19d14", ("Outer Warehouse - Jin Chest near Root Node", IPDKind.MoneyCrate)},
        {"A4_S1_A4_SG7", ("Outer Warehouse to Subroom", IPDKind.Connector)},
        {"A4_S1_A4_S2", ("Outer Warehouse to Inner Warehouse", IPDKind.Connector)},
        {"A4_S1_A4_S6", ("Outer Warehouse to Yangu Hall", IPDKind.Connector)},
        {"A4_S1_A5_S1", ("Outer Warehouse to Factory (Great Hall)", IPDKind.Connector)},
        {"A4_S1_A6_S1", ("Outer Warehouse to Factory (Underground)", IPDKind.Connector)},
        // A4_SG7: Subroom
        {"A4_SG7_ZRoom_Arena_[Variable] Picked8ca3d9cc-26f8-4958-b507-0843324bc4e3", ("Outer Warehouse, Subroom - Breather Jade", IPDKind.DropItem)},

        // A4_S2: Inner Warehouse
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked72e2240c-cf0e-4d85-b326-c19728f42a80", ("IW: Shielded Walking Chest", IPDKind.DropItem, 2002)},
        {"A4_S2_RouteToControlRoom_Final_[Variable] Pickede08b2605-4d1f-4a4e-b864-8a105f7af52b", ("IW: Tianhou Flower", IPDKind.DropItem, 2006)},
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked4e731e33-f14c-4fdb-8b33-a68c9a7ee4c8", ("IW: Dusk Guardian Recording Device", IPDKind.Encyclopedia, 2005)},
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked31b60681-27dd-4b68-b926-6f997c82781e", ("IW: Defeat Celestial Enforcer: Tieyan", IPDKind.Miniboss, 2003)},
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked39bc402c-d3ba-467a-89eb-e8ca0d60d18d", ("IW: Hack 3 Statues", IPDKind.DropItem, 2001)},
        {"A4_S2_A4_S1", ("Inner Warehouse to Outer Warehouse", IPDKind.Connector)},
        // A4_SG1: Subroom
        {"A4_SG1_[Variable] Picked871df5ec-61b5-4067-a074-d6e5ed2ceed5", ("IW: Nymph Puzzle Room", IPDKind.DropItem, 2004)},

        // A4_S3: Boundless Repository
        {"A4_S3_ControlRoom_Final_[Variable] Picked111b64cc-23fd-4b0d-b1df-f2893008b0b1", ("BR: Near Xingtian Console", IPDKind.DropItem, 2102)},
        {"A4_S3_ControlRoom_Final_[Variable] Picked06199e42-2157-4137-aba7-31c33f054b41", ("BR: Examine Console", IPDKind.Encyclopedia, 2101)},
        {"A4_S3_A4_SG4", ("Boundless Repository to Sealed Chamber (Subroom)", IPDKind.Connector)},
        {"A4_S3_A4_S2", ("Boundless Repository to Inner Warehouse", IPDKind.Connector)},
        {"A4_S3_A4_S4", ("Boundless Repository to Chase Sequence (Subroom)", IPDKind.Connector)},
        {"A4_S3_A4_S6", ("Boundless Repository to Yangu Hall", IPDKind.Connector)},
        // A4_S4: Boundless Repository, Chase Sequence
        {"A4_S4_Container_Final_MoneyCrateFlagfe419e8b-4a6a-4780-a2d4-ca17814811d3", ("BR: Gauntlet Part 1 Chest", IPDKind.MoneyCrate, 2103)},
        {"A4_S4_Container_Final_MoneyCrateFlag882bb707-0a28-4617-9ed5-d3ee23b99dc7", ("Boundless Repository, Chase Sequence - Jin Chest room 2 left", IPDKind.MoneyCrate)},
        {"A4_S4_Container_Final_MoneyCrateFlag2a945d87-332c-4d93-92f1-3629767e9250", ("Boundless Repository, Chase Sequence - Jin Chest room 2 middle", IPDKind.MoneyCrate)},
        {"A4_S4_Container_Final_MoneyCrateFlag9b37e76e-5f57-4c79-8020-8b0a5d2bee41", ("Boundless Repository, Chase Sequence - Jin Chest room 2 right", IPDKind.MoneyCrate)},
        // A4_SG4: Boundless Repository, Sealed Chamber
        {"A4_SG4_[Variable] Picked5e5c0008-7a05-4528-a7a7-1cb43b437ddb", ("BR: Vault 1st Chest", IPDKind.DropItem, 2107)},
        {"A4_SG4_[Variable] Pickedad2a9fa0-3e02-478c-a33b-59d0bd7af7a6", ("BR: Vault 2nd Chest", IPDKind.DropItem, 2108)},
        {"A4_SG4_[Variable] Picked108b9d73-3ad1-44cd-9271-8edfe83886bb", ("BR: Examine Vault Scroll", IPDKind.Encyclopedia, 2109)},
        {"A4_SG4_[Variable] Picked111b64cc-23fd-4b0d-b1df-f2893008b0b1", ("BR: Vault 3rd Chest", IPDKind.DropItem, 2110)},
        {"A4_SG4_[Variable] Picked39047b0a-4a21-4f81-8b0e-a333de6bd4e1", ("BR: Vault 4th Chest", IPDKind.DropItem, 2111)},


        // A4_S6: Yangu Hall
        {"A4_S6_DaoBase_Final_[Variable] IsFlowerPicked18a7e188-ce20-495f-8ff9-49b2b0a78515", ("Yangu Hall - Boss reward (Greater Tao Fruit)", IPDKind.DropItem, 2201)},

        // A5_S1: Factory (Great Hall)
        {"A5_S1_CastleHub_remake_[Variable] Picked6eb21645-15b6-4a6f-9b34-de64648fdaa0", ("Factory (Great Hall) - Chest near subroom", IPDKind.DropItem)},
        {"A5_S1_CastleHub_remake_MoneyCrateFlag651ef607-f1b4-4c07-a5c4-3eea7ea58ba5", ("Factory (Great Hall) - Jin chest upper floor", IPDKind.DropItem)},
        {"A5_S1_CastleHub_remake_[Variable] Picked6575c205-b84b-4fd2-9f20-6c6d83aff439", ("Factory (Great Hall) - Transmutation Furnace Monitor", IPDKind.Encyclopedia)},
        {"A5_S1_CastleHub_remake_[Variable] Picked6eef7fd0-7feb-493f-80ab-849699cc874b", ("Factory (Great Hall) - Tao Fruit under hammer bros", IPDKind.DropItem)},
        {"A5_S1_CastleHub_remake_MoneyCrateFlag7950dc61-26eb-4740-a8c4-f0c1c4087c16", ("Factory (Great Hall) - Jin Chest near drop to Underground", IPDKind.MoneyCrate)},
        // MISSING CHEST 1980.125 -3952 0 Chest standard component. Maybe offscreen wrong loc?
        {"A5_S1_CastleHub_remake_[Variable] Pickedeb34355c-2e45-4d64-b716-6d1f50761656", ("Factory (Great Hall) - Chest at bottom right (Basic Component)", IPDKind.DropItem)},
        {"A5_S1_A4_S1", ("Factory (Great Hall) to Outer Warehouse", IPDKind.Connector)},
        {"A5_S1_A5_S4_1", ("Factory (Great Hall) to Factory (Production Area) using left elevator", IPDKind.Connector)},
        {"A5_S1_A5_S4_2", ("Factory (Great Hall) to Factory (Production Area) using right elevator", IPDKind.Connector)},
        {"A5_S1_A5_S4b", ("Factory (Great Hall) to Subroom", IPDKind.Connector)},
        {"A5_S1_A6_S1_1", ("Factory (Great Hall) to Factory (Underground) using elevator", IPDKind.Connector)},
        {"A5_S1_A6_S1_2", ("Factory (Great Hall) to Factory (Underground) using dropdown", IPDKind.Connector)},
        {"A5_S1_A6_S1_3", ("Factory (Great Hall) to Factory (Underground) using shortcut", IPDKind.Connector)},
        {"A5_S1_A7_S1", ("Factory (Great Hall) to Cortex Center", IPDKind.Connector)},
        // A5_S4b: Factory (Great Hall), Subroom (Yes, _S4b is subroom for _S1)
        {"A5_S4b_HerbRoom_Remake_[Variable] Pickedca5aaa95-152f-4234-907e-1ecf2fb44ca8", ("Factory (Great Hall), Subroom - GM Fertilizer", IPDKind.DropItem)},
        {"A5_S4b_HerbRoom_Remake_[Variable] Picked1f8ffa9b-4c37-4d2d-aa67-dab223bce589", ("Factory (Great Hall), Subroom - Dusk Guardian Recording Device 4", IPDKind.Encyclopedia)},
        {"A5_S4b_HerbRoom_Remake_[Variable] Picked37393fc2-f05b-4c94-8bb2-1e3f81e64ff8", ("Factory (Great Hall), Subroom - Tao Fruit", IPDKind.DropItem)},

        // A5_S2: Prison
        {"A5_S2_Jail_Remake_Final_[Variable] Picked7c7fbff1-62de-4ed0-9a69-4b328315a76c", ("Prison: Defeat Kanghui", IPDKind.Miniboss, 2409)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlag468d5e12-8853-46b9-b96c-3ffd8d8be0bc", ("Prison - Escape reward Jin Chest 2", IPDKind.MoneyCrate)},
        {"A5_S2_Jail_Remake_Final_[Variable] Pickedd72d6afb-71b2-46c6-9dd4-6a0f3a0001fa", ("Prison - Escape reward Chest", IPDKind.DropItem)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlagee52d134-b47a-4269-8484-88588a15f0f9", ("Prison - Escape reward Jin Chest 1", IPDKind.MoneyCrate)},
        {"A5_S2_Jail_Remake_Final_[Variable] Pickedcb293a54-c643-4cf4-99ad-6e7203ce153b", ("Prison - Prisoner's Bamboo Scroll (II)", IPDKind.Encyclopedia)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlag8c449e70-479a-4dd7-830d-1da255d8bda3", ("Prison - Chest top right (Standard Component)", IPDKind.DropItem)},
        {"A5_S2_Jail_Remake_Final_MoneyCrateFlagba51acb0-87d1-4aae-862d-d61e30eb2112", ("Prison - Jin Chest left of statue", IPDKind.MoneyCrate)},
        {"A5_S2_Jail_Remake_Final_[Variable] Picked8f746de4-30c8-45c9-9c69-33ce39d72a69", ("Prison: Lower Left Cell", IPDKind.DropItem, 2402)},
        {"A5_S2_Jail_Remake_Final_[Variable] Pickeda4c0a164-ab17-4445-b889-ed495cdd4b3e", ("Prison - Prisoner's Bamboo Scroll (I)", IPDKind.Encyclopedia)},
        {"A5_S2_A5_S3", ("Prison to Factory (Machine Room)", IPDKind.Connector)},

        // A5_S3: Factory (Machine Room)
        {"A5_S3_UnderCastle_Remake_4wei_MoneyCrateFlag80735180-69fd-4718-8b45-0c44b544ad56", ("Factory (Machine Room) - Jin Chest near elevator top", IPDKind.MoneyCrate)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked37adc43b-6e63-4f05-8641-7fa1e034a9ac", ("Factory (MR): Tianhou Flower Above Right Elevator", IPDKind.DropItem, 2507)},
        {"A5_S3_UnderCastle_Remake_4wei_MoneyCrateFlag1ca7c85f-f131-469a-b6d5-56b652ff6f3b", ("Factory (Machine Room) - Jin Chest top", IPDKind.MoneyCrate)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked268a9df6-e099-49e4-a807-b40a3009b39b", ("Factory (MR): Walking Chest Above Green Pillar", IPDKind.DropItem, 2504)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked4d19d7f5-0f37-44d3-bb5c-7c73425083c0", ("Factory (Machine Room) - Chest behind red parry right (Herb Catalyst)", IPDKind.DropItem)},
        {"A5_S3_UnderCastle_Remake_4wei_[Variable] Picked345ea8d3-2391-437d-b605-2ba1b5d31592", ("Factory (Machine Room) - Chest behind red parry left", IPDKind.DropItem)},
        {"A5_S3_UnderCastle_Remake_4wei_MoneyCrateFlag484a0068-3348-44f8-9d4c-3546f74e818a", ("Factory (Machine Room) - Jin Chest near bottom elevator", IPDKind.MoneyCrate)},
        {"A5_S3_A5_S2", ("Factory (Machine Room) to Prison", IPDKind.Connector)},
        {"A5_S3_A6_S1", ("Factory (Machine Room) to Factory (Underground)", IPDKind.Connector)},

        // A5_S4: Factory (Production Area)
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Picked13fd16f9-f8e7-4881-9395-166536db7110", ("Factory (Production Area) - Chest top right red pulse area", IPDKind.DropItem)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Pickeda132dfc7-454d-4289-be25-108fe3a45a7f", ("Factory (PA): Defeat Celestial Sentinel: Wuqiang", IPDKind.Miniboss, 2614)},
        {"A5_S4_CastleMid_Remake_5wei_MoneyCrateFlagbd976538-90da-47ce-bcbb-0851b7a6c88e", ("Factory (Production Area) - Jin Chest right edge", IPDKind.DropItem)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Picked9ddbdb2d-2e8f-41a3-8f8d-65bd8ad55dd0", ("Factory (PA): Defeat Shanhai 9000", IPDKind.DropItem, 2611)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Pickede934ff58-45e1-4934-a47c-c249e5396736", ("Factory (Production Area) - Chest bottom right red pulse area", IPDKind.DropItem)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Picked29ac9657-da22-4da1-84a4-8aa2bd1b45c4", ("Factory (Production Area) - Chest below root node", IPDKind.DropItem)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Pickeda33def8d-d607-43d9-93fe-1b4dba674ba2", ("Factory (PA): Examine Production Station", IPDKind.Encyclopedia, 2605)},
        {"A5_S4_CastleMid_Remake_5wei_MoneyCrateFlag8e602c46-0c36-40e5-9909-cf04fad9f9fb", ("Factory (Production Area) - Jin Chest near enemy factory", IPDKind.DropItem)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Pickedbbfe2e7c-8079-43c0-b53c-ec1731510b40", ("Factory (Production Area) - Chest lower red pulse area", IPDKind.DropItem)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Picked29fafa51-eaef-4281-a290-d932afeca6b9", ("Factory (Production Area) - Chest upper left tunnels", IPDKind.DropItem)},
        {"A5_S4_CastleMid_Remake_5wei_MoneyCrateFlag2fd7983d-69a6-42fb-ae90-d0297fa6f650", ("Factory (Production Area) - Jin Chest near left elevator", IPDKind.MoneyCrate)},
        {"A5_S4_CastleMid_Remake_5wei_[Variable] Picked82751eb1-81bf-4408-99b8-cba6f5ded526", ("Factory (Production Area) - Chest in bottom left tunnels", IPDKind.DropItem)},
        {"A5_S4_A5_S4d", ("Factory (Production Area) to Subroom", IPDKind.Connector)},
        {"A5_S4_A5_S1_1", ("Factory (Production Area) to Factory (Great Hall) using left elevator", IPDKind.Connector)},
        {"A5_S4_A5_S1_2", ("Factory (Production Area) to Factory (Great Hall) using right elevator", IPDKind.Connector)},
        {"A5_S4_A5_S5", ("Factory (Production Area) to Shengwu Hall", IPDKind.Connector)},
        // A5_S4d: Factory (Production Area), Subroom
        {"A5_S4d_PoisonRoom_[Variable] Picked617afb1a-6899-4f67-9cde-3d4e875a3459", ("Factory (PA): Examine Pharmacy Panel", IPDKind.Encyclopedia, 2608)},
        {"A5_S4d_PoisonRoom_[Variable] Picked2a17819c-238f-477e-b329-6de22a10f5e2", ("Factory (Production Area), Subroom - Gene Eradicator", IPDKind.DropItem)},

        // A5_S5: Shengwu Hall
        {"A5_S5_JieChuanHall_[Variable] Pickedab35513a-bb8b-48c5-9ba8-60c8dbcf68c7", ("Shengwu Hall - Chest near Sol Seal", IPDKind.DropItem, 2704)},
        {"A5_S5_JieChuanHall_[Variable] IsFlowerPicked42e16c3d-e649-417c-909f-400422f8418b", ("Shengwu Hall - Boss Reward (Greater Tao Fruit)", IPDKind.DropItem, 2703)},
        {"A5_S5_JieChuanHall_[Variable] Picked47ce3ebe-da42-4fe9-9904-0c7b1e2a3291", ("Shengwu Hall - Chest right side (Sword of Jie)", IPDKind.DropItem, 2702)},
        {"A5_S5_JieChuanHall_[Variable] Pickedb343cef0-1f01-4da7-aa40-d23bd78c4bfd", ("Shengwu Hall - Haotian Sphere Model", IPDKind.Encyclopedia, 2701)},
        {"A5_S5_A5_S4", ("Shengwu Hall to Factory (Production Area)", IPDKind.Connector)},

        // A6_S1: Factory (Underground)
        {"A5_S1_CastleHub_remake_[Variable] Picked5a43bd18-db43-4c4a-8d4e-b72897ff8b60", ("Factory (Underground) - Jie Clan Family Precept", IPDKind.Encyclopedia)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Picked3335aace-6687-4232-9651-654234c0694a", ("Factory (Underground) - Chest at dropdown (Swift Descent)", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Picked4784d1a9-7d01-4dd9-96ac-349d997c15d1", ("Factory (Underground) - Evacuation Notice for Miners", IPDKind.Encyclopedia)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Picked1cdbb937-1c31-45fc-999f-bbb90edf653c", ("Factory (Underground) - Shanhai 9000", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Pickeddc5cd101-21bd-4738-a3d4-48416158a2cb", ("Factory (Underground) - Chest near top elevator (Standard Component)", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_MoneyCrateFlaga9f9b1fe-5fa9-4ece-9e6b-81b60d88b2b8", ("Factory (Underground) - Jin Chest bottom left", IPDKind.MoneyCrate)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Picked3b68c09d-7491-416b-8988-77ff63276220", ("Factory (Underground) - Chest above Root Node (Basic Component)", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_MoneyCrateFlag93d51245-cf6e-4c8e-af18-8fc715c9c3a0", ("Factory (Underground) - Jin chest near downward elevator", IPDKind.MoneyCrate)},
        // Parry puzzle bot 5472 -7952 0
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Pickedbbeb3b5d-b0e4-4c6f-9ef5-0add24781c9d", ("Factory (Underground) - Parry puzzle reward", IPDKind.DropItem)},
        {"A6_S1_AbandonMine_Remake_4wei_[Variable] Pickededbeab36-c776-4d36-9a13-8cec36d57999", ("Factory (Underground) - Miniboss reward (Standard Component)", IPDKind.Miniboss)},
        {"A6_S1_A4_S1", ("Factory (Underground) to Outer Warehouse", IPDKind.Connector)},
        {"A6_S1_A5_S1_1", ("Factory (Underground) to Factory (Great Hall) using elevator", IPDKind.Connector)},
        {"A6_S1_A5_S1_2", ("Factory (Underground) to Factory (Great Hall) using shortcut", IPDKind.Connector)},
        {"A6_S1_A6_S3", ("Factory (Underground) to Abandoned Mines", IPDKind.Connector)},

        // A6_S3: Abandoned Mines
        {"A6_S3_Tutorial_And_SecretBoss_Remake_[Variable] Picked698409a7-a500-49f4-80db-b7338a50eb70", ("Abandoned Mines - Miniboss reward", IPDKind.Miniboss)},
        {"A6_S3_Tutorial_And_SecretBoss_Remake_MoneyCrateFlag3974847e-b748-4e1e-8de8-9d2132c1e376", ("Abandoned Mines - Jin chest 2 near tao fruit", IPDKind.MoneyCrate)},
        {"A6_S3_Tutorial_And_SecretBoss_Remake_MoneyCrateFlagffcaefa8-aadf-4d30-8e66-169df3037d84", ("Abandoned Mines - Jin chest 1 near tao fruit", IPDKind.MoneyCrate)},
        {"A6_S3_Tutorial_And_SecretBoss_Remake_[Variable] Pickedb70de28f-4097-406c-8cf8-583870dc4ab5", ("Abandoned Mines - Tao Fruit", IPDKind.DropItem)},
        {"A6_S3_Tutorial_And_SecretBoss_Remake_[Variable] Pickedb74a6c8e-612a-4542-a373-f2f0d8597732", ("Abandoned Mines - Walking Chest below root node", IPDKind.DropItem)},
        {"A6_S3_Tutorial_And_SecretBoss_Remake_MoneyCrateFlagb25c053c-5d77-45ce-a1d6-8db85937b59a", ("Abandoned Mines - Jin Chest near breakable wall", IPDKind.MoneyCrate)},
        {"A6_S3_Tutorial_And_SecretBoss_Remake_MoneyCrateFlagb0ec8e30-c2b6-4b5a-9591-48f83de2be58", ("Abandoned Mines - Jin Chest far left", IPDKind.MoneyCrate)},
        {"A6_S3_A6_S1", ("Abandoned Mines to Factory (Underground)", IPDKind.Connector)},
        {"A6_S3_A0_S7", ("Abandoned Mines to Abandoned Mines", IPDKind.Connector)},

        // A7_S1: Cortex Center
        {"A7_S1_BrainRoom_Remake_[Variable] Pickedb70de28f-4097-406c-8cf8-583870dc4ab5", ("Cortex Center: Lady Ethereal's Tianhou Flower", IPDKind.DropItem, 602)},
        {"A7_S1_BrainRoom_Remake_NPC Solvableb89702e1-e5eb-4f06-af30-8a6aeddea266", ("Cortex Center: Retrieve Chip From Shanhai 9000", IPDKind.DropItem, 603)},
        {"A7_S1_BrainRoom_Remake_[Variable] Picked0b37affd-62a8-4cc5-b016-8926d8bf3b21", ("Cortex Center: Near Left Exit", IPDKind.DropItem, 606)},
        {"A7_S1_BrainRoom_Remake_MoneyCrateFlag93a7e61c-c9ac-43d8-818c-e374860c848c", ("Cortex Center - Jin Chest across spikes", IPDKind.MoneyCrate)},
        {"A7_S1_BrainRoom_Remake_MoneyCrateFlag406ea702-4cd4-4eed-888a-11e8769fa102", ("Cortex Center - Jin Chest top", IPDKind.MoneyCrate)},
        {"A7_S1_AG_S1", ("Cortex Center to New Kunlun Central Hall", IPDKind.Connector)},
        {"A7_S1_A5_S1", ("Cortex Center to Factory (Great Hall)", IPDKind.Connector)},

        // A9_S1: Empyrean Dist. (Passages)
        {"A9_S1_Remake_4wei_[Variable] Picked353c6a35-962d-4a83-b651-7eeb6931ba7d", ("Empyrean Dist. (Passages) - Chest right laser passage", IPDKind.DropItem)},
        {"A9_S1_Remake_4wei_MoneyCrateFlag3fcbd78a-3a0a-4f83-8c81-9cf612c9fe0c", ("Empyrean Dist. (Passages) - Jin Chest right between laser dropdown", IPDKind.MoneyCrate)},
        {"A9_S1_Remake_4wei_[Variable] Pickedc51a43cf-a933-495f-9b09-611db24870d9", ("Empyrean Dist. (Passages) - Underground Water Tower", IPDKind.Encyclopedia)},
        {"A9_S1_Remake_4wei_MoneyCrateFlagb8082ed5-fb34-4893-9791-50135e7bb5e4", ("Empyrean Dist. (Passages) - Jin Chest top red parry", IPDKind.MoneyCrate)},
        {"A9_S1_Remake_4wei_[Variable] Picked8b8ca1b2-39db-4850-94a2-15c35b2ddba2", ("Empyrean Dist. (Passages) - Miniboss reward (Dark steel)", IPDKind.Miniboss)},
        {"A9_S1_Remake_4wei_[Variable] Pickedaf0efbc3-1c92-42cb-a76c-5351ea54d7ff", ("Empyrean Dist. (Passages) - Chest below big axe dude (Herb catalyst)", IPDKind.DropItem)},
        {"A9_S1_Remake_4wei_MoneyCrateFlage1f2e39b-45cc-422e-8727-606fed5d6a5b", ("Empyrean Dist. (Passages) - Jin Chest above big axe dude", IPDKind.MoneyCrate)},
        {"A9_S1_Remake_4wei_MoneyCrateFlag8cf0ec81-b76c-45cd-b226-adbf7bf8f911", ("Empyrean Dist. (Passages) - Jin Chest at upper middle light rails", IPDKind.DropItem)},
        {"A9_S1_Remake_4wei_[Variable] Picked52ae9ae8-58ed-4ec5-a091-47bfdd2c3daf", ("Empyrean Dist. (Passages) - Parry puzzle reward", IPDKind.DropItem)},
        {"A9_S1_Remake_4wei_[Variable] Picked73b45211-eefc-4d17-9240-be7b704352ab", ("Empyrean Dist. (Passages) - Chest at right edge", IPDKind.DropItem)},
        {"A9_S1_Remake_4wei_MoneyCrateFlagf6efad04-fbf9-4de6-ae8c-17f778df1a31", ("Empyrean Dist. (Passages) - Jin Chest 1 at lower middle light rails", IPDKind.MoneyCrate)},
        {"A9_S1_Remake_4wei_MoneyCrateFlag6c3c166c-e2f9-405a-8046-3d9db6e8afc3", ("Empyrean Dist. (Passages) - Jin Chest 2 at lower middle light rails", IPDKind.MoneyCrate)},
        {"A9_S1_A9_S2", ("Empyrean Dist. (Passages) to Empyrean Dist. (Living Area)", IPDKind.Connector)},
        {"A9_S1_A9_S4", ("Empyrean Dist. (Passages) to Sky Tower", IPDKind.Connector)},
        {"A9_S1_A10_S4_1", ("Empyrean Dist. (Passages) to Grotto of Scriptures (West), Upper Path", IPDKind.Connector)},
        {"A9_S1_A10_S4_2", ("Empyrean Dist. (Passages) to Grotto of Scriptures (West), Lower Path", IPDKind.Connector)},

        // A9_S2: Empyrean Dist. (Living Area)
        {"A9_S2_Remake_4wei_[Variable] Pickedb4dd6db6-9b54-4916-aca1-34d54d8ae8bc", ("Empyrean Dist. (Living Area) - Walking Chest top left (Turtle Scorpion)", IPDKind.DropItem)},
        {"A9_S2_Remake_4wei_[Variable] Picked27e2bb4b-d90d-4482-96a0-1bc67de74e15", ("Empyrean Dist. (Living Area) - Walking Chest top left (Turtle Scorpion)", IPDKind.DropItem)},
        {"A9_S2_Remake_4wei_MoneyCrateFlagbb150c1b-d522-4012-9fe2-4241527d3a1c", ("Empyrean Dist. (Living Area) - Jin Chest near backer room", IPDKind.MoneyCrate)},
        {"A9_S2_Remake_4wei_[Variable] Pickedde0c4095-ef45-4467-8c5a-c22861589c8f", ("Empyrean Dist. (Living Area) - Dusk Guardian Recording Device 5", IPDKind.Encyclopedia)},
        {"A9_S2_Remake_4wei_[Variable] Pickede08b2605-4d1f-4a4e-b864-8a105f7af52b", ("Empyrean Dist. (Living Area) - Tao fruit top right second house", IPDKind.DropItem)},
        {"A9_S2_Remake_4wei_[Variable] Pickeddff9bb84-4dd6-48e5-ae71-13057ab4cd1e", ("Empyrean Dist. (Living Area) - Chest top right first house (Penglai Recipe Collection)", IPDKind.DropItem)},
        {"A9_S2_Remake_4wei_MoneyCrateFlag90ea00fe-516e-4a4a-8c40-9c150d9089a6", ("Empyrean Dist. (Living Area) - Jin Chest middle elevator second floor", IPDKind.MoneyCrate)},
        {"A9_S2_Remake_4wei_MoneyCrateFlag940ace56-85b2-40ec-b00c-b26e97775751", ("Empyrean Dist. (Living Area) - Jin Chest above root node", IPDKind.MoneyCrate)},
        {"A9_S2_Remake_4wei_[Variable] Picked8bb82593-33d9-4271-b815-2b50b7c2b4bf", ("Empyrean Dist. (Living Area) - Walking Chest right", IPDKind.DropItem)},
        {"A9_S2_Remake_4wei_[Variable] Pickede82731e3-d3f6-4cdb-aa1d-858ca4daf644", ("Empyrean Dist. (Living Area) - Empyrean Bulletin Board", IPDKind.Encyclopedia)},
        {"A9_S2_Remake_4wei_NPC Solvable655dde03-898a-4cea-9cec-2d88d623cfbb", ("Empyrean Dist. (Living Area) - Shanhai 9000", IPDKind.DropItem)},
        {"A9_S2_Remake_4wei_MoneyCrateFlag7c04622c-095c-4bbd-b428-bcffea2807c6", ("Empyrean Dist. (Living Area) - Jin Chest right edge", IPDKind.MoneyCrate)},
        {"A9_S2_A9_SG1", ("Empyrean Dist. (Living Area) to Empyrean Dist. (Living Area), Backer Room", IPDKind.Connector)},
        {"A9_S2_A9_S1", ("Empyrean Dist. (Living Area) to Empyrean Dist. (Passages)", IPDKind.Connector)},
        // A9_SG1: Empyrean Dist. (Living Area), Backer Room
        {"A9_SG1_[Variable] Pickedb4dd6db6-9b54-4916-aca1-34d54d8ae8bc", ("Empyrean Dist. (Living Area), Backer Room - Chest 1", IPDKind.DropItem)},
        {"A9_SG1_[Variable] Picked581f8b7d-3438-4713-9a12-a48a1d61e469", ("Empyrean Dist. (Living Area), Backer Room - Chest 2", IPDKind.DropItem)},
        {"A9_SG1_[Variable] Pickedd8b140df-1f6c-4fb5-902f-cabb3c979440", ("Empyrean Dist. (Living Area), Backer Room - Chest 3", IPDKind.DropItem)},

        // A9_S3: Empyrean Dist. (Sanctum)
        {"A9_S3_[Variable] Picked425e30c1-796a-4a14-9832-1c14bbfc195e", ("Empyrean Dist. (Sanctum) - Enemy drop right of elevator", IPDKind.DropItem)},
        {"A9_S3_[Variable] Picked6bcdda38-749f-478d-8991-bfaa1989b6f3", ("Empyrean Dist. (Sanctum) - Item left edge in shadowy room", IPDKind.DropItem)},
        {"A9_S3_[Variable] Picked868a27fc-a116-45aa-8603-d13bb6aaab06", ("Empyrean Dist. (Sanctum) - Item left edge on ground (Elevator Access Token)", IPDKind.DropItem)},
        {"A9_S3_[Variable] Picked3e6aabbd-131e-4176-989b-12c9c964cbc5", ("Empyrean Dist. (Sanctum) - Enemy drop above big elevator", IPDKind.DropItem)},
        {"A9_S3_[Variable] Pickedc07619c9-c2da-4940-8f25-cd27902fdc96", ("Empyrean Dist. (Sanctum) - Chest top right", IPDKind.DropItem)},
        {"A9_S3_MoneyCrateFlagd8245daf-8cbb-46d1-9ed9-f95ce5cda820", ("Empyrean Dist. (Sanctum) - Jin Chest right middle", IPDKind.MoneyCrate)},
        {"A9_S3_[Variable] Pickedb8f06bfb-a38b-4196-b9a2-60428afa0e5f", ("Empyrean Dist. (Sanctum) - Vital Sanctum Tower Monitoring Panel", IPDKind.Encyclopedia)},
        {"A9_S3_[Variable] Picked1ff756a9-e127-4e4d-984c-ae935ab7dfbd", ("Empyrean Dist. (Sanctum) - Walking Chest (Computing Unit)", IPDKind.DropItem)},
        {"A9_S3_A9_S5", ("Empyrean Dist. (Sanctum) to Nobility Hall", IPDKind.Connector)},

        // A9_S4: Sky Tower
        {"A9_S4_[Variable] Picked5abcce5d-161e-4d5c-ab58-5bc702cef327", ("Sky Tower - Chest top right edge", IPDKind.DropItem)},
        {"A9_S4_[Variable] Picked3b54e07d-ef21-4316-979c-9dfc3462df9c", ("Sky Tower - Chest near root node", IPDKind.DropItem)},
        {"A9_S4_[Variable] Picked1ff756a9-e127-4e4d-984c-ae935ab7dfbd", ("Sky Tower - Urn top right", IPDKind.DropItem)},
        {"A9_S4_[Variable] Pickedde0c4095-ef45-4467-8c5a-c22861589c8f", ("Sky Tower - Stowaway's Corpse", IPDKind.Encyclopedia)},
        {"A9_S4_[Variable] Picked97d45562-89e6-463d-8740-4d1e012cfd18", ("Sky Tower - Tao Fruit", IPDKind.DropItem)},
        {"A9_S4_MoneyCrateFlag5c521005-5675-4333-ad0a-f7a2f34c1752", ("Sky Tower - Chest third of the way up", IPDKind.MoneyCrate)},
        {"A9_S4_[Variable] Picked6fa8a9cb-7c36-47ff-a20f-b7aa36efcec5", ("Sky Tower - Chest third of the way up", IPDKind.DropItem)},
        {"A9_S4_MoneyCrateFlag8f77b919-eef6-4147-843b-2da88ff082e8", ("Sky Tower - Jin Chest just above elevator", IPDKind.MoneyCrate)},
        {"A9_S4_A3_S1", ("Sky Tower to Lake Yaochi Ruins", IPDKind.Connector)},
        {"A9_S4_A9_S1", ("Sky Tower to Empyrean Dist. (Passages)", IPDKind.Connector)},

        // A9_S5: Nobility Hall
        {"A9_S5_風氏_[Variable] Pickeddedc5a97-07da-4f6b-b4e5-e437cbe3414d", ("Nobility Hall - Chest past boss (Tianhuo Serum)", IPDKind.DropItem)},
        {"A9_S5_風氏_[Variable] IsFlowerPicked7212c72d-ce6c-4b5f-a536-4ec7a4e2934e", ("Nobility Hall - Twin Tao Fruit", IPDKind.DropItem)},
        {"A9_S5_A9_S3", ("Nobility Hall to Empyrean Dist. (Sanctum)", IPDKind.Connector)},

        // A10_S1: Grotto of Scriptures (Entry)
        // missing chest top right and above root node
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag44ed77f3-ca0d-40e8-80a6-71d36cdf962c", ("Grotto of Scriptures (Entry) - Jin Chest above root node 1", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag43eed1d5-068e-44b1-8e0c-8458b9a1ae77", ("Grotto of Scriptures (Entry) - Jin Chest above root node 2", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag65cb6edc-04c4-48bf-9a8d-6558a9df4511", ("Grotto of Scriptures (Entry) - Jin Chest above root node 3", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag8a661130-727e-4ac6-87aa-b2dcee945e24", ("Grotto of Scriptures (Entry) - Jin Chest above root node 4", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag3a1b4f16-7bad-48ac-b5b1-662a9e579af3", ("Grotto of Scriptures (Entry) - Jin Chest above root node 5", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_[Variable] Picked41eb5a6f-1015-42d6-be65-2775f80414f9", ("Grotto of Scriptures (Entry) - Chest under glass ceiling", IPDKind.DropItem)},
        {"A10_S1_TombEntrance_remake_[Variable] Picked18a5cc6a-9fcc-44c0-aa8f-5e14bd6ad3e4", ("Grotto of Scriptures (Entry) - Coffin Inscription", IPDKind.Encyclopedia)},
        {"A10_S1_TombEntrance_remake_[Variable] Pickedd0049033-8af9-4657-8046-3b31d94153ee", ("Grotto of Scriptures (Entry) - Chest above glass ceiling (Qiankun Board)", IPDKind.DropItem)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag5fb4bef3-fdcb-4b42-b6c8-84823e38a322", ("Grotto of Scriptures (Entry) - Jin Chest at teleport door right 3", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_[Variable] Picked6c059e7f-241f-4cbb-b678-0f663bc61dfa", ("Grotto of Scriptures (Entry) - Chest at teleport door right (Basic Component)", IPDKind.DropItem)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag784dca53-caf7-4cbb-b496-8bf23dd62a4f", ("Grotto of Scriptures (Entry) - Jin Chest at teleport door right 1", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag292ce9cd-c4ff-4a01-bc4e-bb9d870fe15e", ("Grotto of Scriptures (Entry) - Jin Chest right of rafters", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag7ce9f944-3134-4ca0-bc5f-bbd4b2361acc", ("Grotto of Scriptures (Entry) - Jin Chest left of rafters", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_[Variable] Picked07f3b10d-9898-4ca5-95ad-db4eb15cc565", ("Grotto of Scriptures (Entry) - Chest at teleport door bottom (Dark Steel)", IPDKind.DropItem)},
        {"A10_S1_TombEntrance_remake_MoneyCrateFlag1d7fbaf8-1d57-4c9c-b825-651f3f9abf41", ("Grotto of Scriptures (Entry) - Jin Chest near spikes", IPDKind.MoneyCrate)},
        {"A10_S1_TombEntrance_remake_[Variable] Picked3b7c610d-f1aa-46bd-bf15-f71f24750ded", ("Grotto of Scriptures (Entry) - Ancient Cave Painting", IPDKind.Encyclopedia)},
        {"A10_S1_A3_S1", ("Grotto of Scriptures (Entry) to Lake Yaochi Ruins", IPDKind.Connector)},
        {"A10_S1_A3_S2", ("Grotto of Scriptures (Entry) to Greenhouse", IPDKind.Connector)},
        {"A10_S1_A3_S5", ("Grotto of Scriptures (Entry) to Agrarian Hall", IPDKind.Connector)},
        {"A10_S1_A10_S3", ("Grotto of Scriptures (Entry) to Grotto of Scriptures (East)", IPDKind.Connector)},
        {"A10_S1_A10_S4", ("Grotto of Scriptures (Entry) to Grotto of Scriptures (West)", IPDKind.Connector)},

        // A10_S3: Grotto of Scriptures (East)
        {"A10_S3_HistoryTomb_Right_[Variable] Picked1ec54b98-6227-4b46-9ba6-19c8fb52dbd2", ("Grotto of Scriptures (East) - Vase near left subroom", IPDKind.DropItem)},
        {"A10_S3_HistoryTomb_Right_MoneyCrateFlag3c2b3cbb-2202-4035-ab25-d2b43f3c7889", ("Grotto of Scriptures (East) - Jin chest top right", IPDKind.MoneyCrate)},
        {"A10_S3_HistoryTomb_Right_[Variable] Picked33e5917a-336d-49aa-87d3-23bb457f9771", ("Grotto of Scriptures (East) - Chest under top right shortcut", IPDKind.DropItem)},
        {"A10_S3_HistoryTomb_Right_MoneyCrateFlagf153eabb-2c73-439f-923b-2954c9ba0fb3", ("Grotto of Scriptures (East) - Jin Chest under top right shortcut 2", IPDKind.MoneyCrate)},
        {"A10_S3_HistoryTomb_Right_MoneyCrateFlag78bae5b8-b50a-4970-9f76-7894a24a3398", ("Grotto of Scriptures (East) - Jin Chest under top right shortcut 1", IPDKind.MoneyCrate)},
        {"A10_S3_HistoryTomb_Right_[Variable] Picked0c180e40-c8e7-4c43-ab22-cc982383740a", ("Grotto of Scriptures (East) - Stone Carvings", IPDKind.Encyclopedia)},
        {"A10_S3_HistoryTomb_Right_MoneyCrateFlage959755d-d9a3-4b65-b84b-7b78bc57c624", ("Grotto of Scriptures (East) - Jin Chest under root node", IPDKind.MoneyCrate)},
        {"A10_S3_HistoryTomb_Right_MoneyCrateFlag7b22620b-58d9-487a-9e6e-97a7ea0ee436", ("Grotto of Scriptures (East) - Jin Chest below rolling boulders", IPDKind.DropItem)},
        {"A10_S3_HistoryTomb_Right_[Variable] Picked39bc402c-d3ba-467a-89eb-e8ca0d60d18d", ("Grotto of Scriptures (East) - Walking Chest", IPDKind.DropItem)},
        {"A10_S3_HistoryTomb_Right_[Variable] Picked876f1c20-3b3a-4d00-96e3-dce61dea0b1f", ("Grotto of Scriptures (East) - Chest bottom right", IPDKind.DropItem)},
        {"A10_S3_HistoryTomb_Right_MoneyCrateFlag1f9b1969-97c9-4399-ae09-38e1dd1653d5", ("Grotto of Scriptures (East) - Jin chest above lower left exit", IPDKind.MoneyCrate)},
        {"A10_S3_HistoryTomb_Right_MoneyCrateFlag3fbb5dff-f813-4cb4-87e0-ffca15ecb6a9", ("Grotto of Scriptures (East) - Jin chest near lower left exit", IPDKind.MoneyCrate)},
        {"A10_S3_A10_S1", ("Grotto of Scriptures (East) to Grotto of Scriptures (Entry)", IPDKind.Connector)},
        {"A10_S3_A10_S4_1", ("Grotto of Scriptures (East) to Grotto of Scriptures (West), Upper Path", IPDKind.Connector)},
        {"A10_S3_A10_S4_2", ("Grotto of Scriptures (East) to Grotto of Scriptures (West), Lower Path", IPDKind.Connector)},
        {"A10_S3_A10_SG1", ("Grotto of Scriptures (East) to Right Subroom", IPDKind.Connector)},
        {"A10_S3_A10_SG2", ("Grotto of Scriptures (East) to Left Subroom", IPDKind.Connector)},
        // A10_SG1: Grotto of Scriptures (East), Right Subroom
        {"A10_SG1_Cave1_[Variable] Pickedb0faa71f-2c27-43a4-b382-88d98f6e509c", ("Grotto of Scriptures (East), Right Subroom - Secret Mural I", IPDKind.Encyclopedia)},
        // A10_SG2: Grotto of Scriptures (East), Left Subroom
        {"A10_SG2_Cave2_[Variable] Picked1d34ac00-9842-4133-a0e8-460854492d5b", ("Grotto of Scriptures (East), Left Subroom - Secret Mural II", IPDKind.Encyclopedia)},

        // A10_S4: Grotto of Scriptures (West)
        {"A10_S4_HistoryTomb_Left_[Variable] Picked9225907a-d6dd-43fc-83ab-73eb2349ea2e", ("Grotto of Scriptures (West) - Chest near rotate puzzle", IPDKind.DropItem)},
        {"A10_S4_HistoryTomb_Left_[Variable] Pickedb4dd6db6-9b54-4916-aca1-34d54d8ae8bc", ("Grotto of Scriptures (West) - Chest on cliffside", IPDKind.DropItem)},
        {"A10_S4_HistoryTomb_Left_MoneyCrateFlagf94f74ad-a349-473a-9000-3d0a30da7b57", ("Grotto of Scriptures (Entry) - Jin Chest top", IPDKind.MoneyCrate)},
        {"A10_S4_HistoryTomb_Left_[Variable] Picked4c61e512-3cc3-449f-9b24-748e46ce4532", ("Grotto of Scriptures (West) - Chest near elevator", IPDKind.Connector)},
        {"A10_S4_A9_S1_1", ("Grotto of Scriptures (West) to Empyrean Dist. (Passages), Upper Path", IPDKind.Connector)},
        {"A10_S4_A9_S1_2", ("Grotto of Scriptures (West) to Empyrean Dist. (Passages), Lower Path", IPDKind.Connector)},
        {"A10_S4_A10_S1", ("Grotto of Scriptures (West) to Grotto of Scriptures (Entry)", IPDKind.Connector)},
        {"A10_S4_A10_S3_1", ("Grotto of Scriptures (West) to Grotto of Scriptures (East), Upper Path", IPDKind.Connector)},
        {"A10_S4_A10_S3_2", ("Grotto of Scriptures (West) to Grotto of Scriptures (East), Lower Path", IPDKind.Connector)},
        {"A10_S4_A10_S5", ("Grotto of Scriptures (West) to Ancient Stone Pillar", IPDKind.Connector)},
        {"A10_S4_A10_SG4", ("Grotto of Scriptures (West) to Upper Subroom", IPDKind.Connector)},
        {"A10_S4_A10_SG5", ("Grotto of Scriptures (West) to Lower Subroom", IPDKind.Connector)},
        // A10_SG4: Grotto of Scriptures (West), Upper Subroom
        {"A10_SG4_Cave4_[Variable] Picked55e121a7-ef68-4d8d-97d1-4ece39c156c0", ("Grotto of Scriptures (West), Upper Subroom - Secret Mural III", IPDKind.Encyclopedia)},
        // A10_SG5: Grotto of Scriptures (West), Lower Subroom
        {"A10_SG5_LearZone_[Variable] Picked5e282a99-9049-4628-9eac-3d5820c72bcd", ("Grotto of Scriptures (West), Lower Subroom - Tao Fruit 1", IPDKind.DropItem)},
        {"A10_SG5_LearZone_[Variable] Picked7531bdad-dc7e-47db-a640-386674f940dd", ("Grotto of Scriptures (West), Lower Subroom - Tao Fruit 2", IPDKind.DropItem)},
        {"A10_SG5_LearZone_[Variable] Picked951a38fc-8260-4803-89ba-b8b7affd96c4", ("Grotto of Scriptures (West), Lower Subroom - Tao Fruit 3", IPDKind.DropItem)},
        {"A10_SG5_LearZone_[Variable] Picked96ea9cc4-ac43-47ec-801d-d5d321339f7d", ("Grotto of Scriptures (West), Lower Subroom - Chest (Rhizomatic Bomb)", IPDKind.DropItem)},

        // A10_S5: Ancient Stone Pillar
        {"A10_S5_Boss_Jee_NPC Solvable7aae40ed-c16e-4a81-aadc-5473311a11fe", ("Ancient Stone Pillar - Prototype Shanhai 9000", IPDKind.DropItem)},
        {"A10_S5_Boss_Jee_[Variable] Picked65606b8a-9cf7-4667-8e2a-109870107032", ("Ancient Stone Pillar - Boss Reward", IPDKind.DropItem)},
        {"A10_S5_A10_S5", ("Ancient Stone Pillar to Grotto of Scriptures (West)", IPDKind.Connector)},

        // A11_S1: Tiandao Research Center
        {"A11_S1_Hospital_remake_[Variable] Picked9ea6a6a0-2a80-479f-ae3d-cacf4ba93724", ("Tiandao Research Center - Chien", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_MoneyCrateFlage9b38286-7d81-4cc9-a4ab-f998ca480aef", ("Tiandao Research Center - PonR Jin Chest 1", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_MoneyCrateFlag07b155cd-8f3a-49cd-beff-2f377b8a204b", ("Tiandao Research Center - PonR Jin Chest 2", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_MoneyCrateFlag84b0ec71-463a-414c-8fd3-421f39177a19", ("Tiandao Research Center - PonR Jin Chest 3", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_MoneyCrateFlag9933cac6-bfad-4c30-8b1c-3d60e4842f77", ("Tiandao Research Center - PonR Jin Chest 4", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_MoneyCrateFlagc45aa65d-d1d4-4e91-b91d-43c5ecaf2812", ("Tiandao Research Center - PonR Jin Chest 5", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_[Variable] Picked423420c4-e1e0-4dd9-85d5-371157264ee5", ("Tiandao Research Center - Dusk Guardian Headquarters Screen", IPDKind.Encyclopedia)},
        {"A11_S1_Hospital_remake_[Variable] Picked90311789-af88-4804-82d8-f71e107a45a0", ("Tiandao Research Center - Chest hanging from zombie bottom", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_MoneyCrateFlag3315a98b-b182-46b1-815b-13e50e99aacc", ("Tiandao Research Center - Jin Chest just above bottom", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_MoneyCrateFlag1b4db2d2-fe44-4b63-a6a6-f0ec9c1c2be1", ("Tiandao Research Center - Jin Chest bottom left", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_[Variable] Pickedf8ef2dd9-169f-45c2-934c-0ebfed19ad1a", ("Tiandao Research Center - Item through spiky tunnels", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_[Variable] Pickedde0c4095-ef45-4467-8c5a-c22861589c8f", ("Tiandao Research Center - Dusk Guardian Recording Device 6", IPDKind.Encyclopedia)},
        {"A11_S1_Hospital_remake_[Variable] Picked122932d3-2add-48bf-b166-7cc6bba24f2a", ("Tiandao Research Center - Enemy drop top left", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_[Variable] Pickedd96cac9f-6975-4a5e-b573-f81d50c89407", ("Tiandao Research Center - Chest hanging from zombie top middle", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_MoneyCrateFlag9f278edd-2582-4ed1-868e-87621a7b1e33", ("Tiandao Research Center - Jin Chest above statue", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_MoneyCrateFlag4b403ac9-9c63-4c7c-ab6f-44a5ef85e104", ("Tiandao Research Center - Jin Chest on spiky floor", IPDKind.MoneyCrate)},
        {"A11_S1_Hospital_remake_[Variable] Pickedab5215c1-e905-4d74-a488-d94fd775474a", ("Tiandao Research Center - Chest hanging from zombie near statue", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_[Variable] Pickeddff9bb84-4dd6-48e5-ae71-13057ab4cd1e", ("Tiandao Research Center - Chest in explodey puzzle room", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_[Variable] Picked9b71d272-3924-4f89-b85b-1dc2bc4d428a", ("Tiandao Research Center - Shanhai 9000", IPDKind.DropItem)},
        {"A11_S1_Hospital_remake_[Variable] Picked1a9d1d74-32ad-41e2-8897-48b6514d9976", ("Tiandao Research Center - Tiandao Academy Periodical", IPDKind.DropItem)},
        {"A11_S1_A2_S6", ("Tiandao Research Center to Central Transport Hub", IPDKind.Connector)},
        {"A11_S1_A3_S7", ("Tiandao Research Center to Yinglong Canal", IPDKind.Connector)},
        {"A11_S1_A11_S2", ("Tiandao Research Center to Tianhuo Research Institute", IPDKind.Connector)},

        // A11_S2: Tianhuo Research Institute
        {"A11_S2_A11_S1", ("Tianhuo Research Institute to Tiandao Research Center", IPDKind.Connector)},
        {"A11_S2_Laboratory_remake_MoneyCrateFlag2d1c1ade-eb47-424f-b68b-fa99bd02f354", ("Tianhuo Research Institute - Last Jin Chest pre point of no return", IPDKind.MoneyCrate)},
    };
    public static (String name, IPDKind kind) GetHumanReadable(InterestPointData IPD) {
        if (IPD_TABLE.ContainsKey(IPD.name)) return (IPD_TABLE[IPD.name].name, IPD_TABLE[IPD.name].kind);
        // Fallback
        if (IPD.name.Contains("MoneyCrate")) return (IPD.name, IPDKind.MoneyCrate);
        if (IPD.name.Contains("Picked")) return (IPD.name, IPDKind.DropItem);
        return (IPD.name, IPDKind.Unknown);
    }

    private static readonly Dictionary<IPDKind, InterestPointConfig> InterestPointConfigs = [];
    public static void SetIPC(InterestPointData IPD) {
        if (IPD.InterestPointConfigContent && IPD.InterestPointConfigContent.name == "Custom_IPC_from_template") return;
        if (!InterestPointConfigs.Any()) {
            // Need to initialize
            foreach (IPDKind k in Enum.GetValues(typeof(IPDKind))) {
                InterestPointConfigs[k] = CreateIPC(k);
            }
        }
        IPDKind kind = GetHumanReadable(IPD).kind;
        IPD.InterestPointConfigContent = InterestPointConfigs[kind];
    }
    private static InterestPointConfig CreateIPC(IPDKind kind) {
        InterestPointConfig IPConfigTemplate = ScriptableObject.CreateInstance<InterestPointConfig>();
        IPConfigTemplate.PointName = IPConfigTemplate.name = "Custom_IPC_from_template";
        IPConfigTemplate.YOffset = -8;
        IPConfigTemplate.icon = InterestDataMapping.GetLocSprite(kind);
        IPConfigTemplate.showInWorldMapType = kind switch {
            IPDKind.Connector => InterestPointConfig.ShowInMapType.ShowInMinimapOnly,
            _ => InterestPointConfig.ShowInMapType.ShowInMinimapAndWorldMap,
        };
        return IPConfigTemplate;
    }
    public static InterestPointData CreateIPD(String name, Vector3 pos) {
        InterestPointData IPD = ScriptableObject.CreateInstance<InterestPointData>();
        IPD.name = name;
        IPD.worldPosition = pos;
        SetIPC(IPD);
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
