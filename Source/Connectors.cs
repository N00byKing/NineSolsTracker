using System;
using System.Collections.Generic;
using UnityEngine;

static class Connectors {
    private static readonly Dictionary<String, Vector3> ConnectorDictAGS1 = new() {
        {"AG_S1_AG_ST", new Vector3(2880, -4800)},
        {"AG_S1_AG_S2", new Vector3(3860, -4944)},
        {"AG_S1_A2_S6", new Vector3(5300, -5010)},
        {"AG_S1_A3_S1", new Vector3(5300, -4770)},
        {"AG_S1_A7_S1", new Vector3(200, -5010)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictAGS2 = new() {
        {"AG_S2_AG_S1", new Vector3(2100, -4980)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA0S7 = new() {
        {"A0_S7_A0_S8", new Vector3(5103, -4192)},
        {"A0_S7_A6_S3", new Vector3(1852, -3811)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA0S8 = new() {
        {"A0_S8_A0_S7", new Vector3(-3632, -64)},
        {"A0_S8_A0_S9", new Vector3(-1089, -64)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA0S9 = new() {
        {"A0_S9_A0_S8", new Vector3(-1019, -64)},
        {"A0_S9_A0_S10", new Vector3(446, -64)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA0S10 = new() {
        {"A0_S10_A0_S9", new Vector3(554, -64)},
        {"A0_S10_A2_S6", new Vector3(4112, -64)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA1S1 = new() {
        {"A1_S1_A1_S2", new Vector3(-1550, -3900)}
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA1S2 = new() {
        {"A1_S2_A1_S1", new Vector3(-1500, -3315)},
        {"A1_S2_A1_S3", new Vector3(-1650, -4580)},
        {"A1_S2_A2_S6", new Vector3(2820, -4430)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA1S3 = new() {
        {"A1_S3_A1_S2", new Vector3(-2600, -1010)},
        {"A1_S3_A2_S3", new Vector3(-6330, -1150)},
        {"A1_S3_A6_S1", new Vector3(-6900, 50)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA2S1 = new() {
        {"A2_S1_A2_S3", new Vector3(-6170, -1800)},
        {"A2_S1_A2_S2", new Vector3(1380, -1800)},
        {"A2_S1_A2_S5", new Vector3(-2370, -1880)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA2S2 = new() {
        {"A2_S2_A2_S1", new Vector3(-6950, -1600)},
        {"A2_S2_A2_S6", new Vector3(-2070, -1880)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA2S3 = new() {
        {"A2_S3_A1_S3", new Vector3(-7280, -1620)},
        {"A2_S3_A2_S1", new Vector3(-2350, -1180)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA2S5 = new() {
        {"A2_S5_A2_S1", new Vector3(-6900, -2270)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA2S6 = new() {
        {"A2_S6_AG_S1", new Vector3(5500, -4350)},
        {"A2_S6_A0_S10", new Vector3(3000, -6768)},
        {"A2_S6_A1_S2", new Vector3(4500, -8320)},
        {"A2_S6_A2_S2", new Vector3(7800, -8220)},
        {"A2_S6_A11_S1", new Vector3(7480, -7400)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA3S1 = new() {
        {"A3_S1_A3_SG1", new Vector3(576, -3280)},
        {"A3_S1_A3_SG2", new Vector3(9113, -3200)},
        {"A3_S1_A3_SG4", new Vector3(-1053, -2992)},
        {"A3_S1_AG_S1", new Vector3(-4700, -3100)},
        {"A3_S1_A3_S7", new Vector3(1250, -3500)},
        {"A3_S1_A10_S1", new Vector3(11400, -3200)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA3S2 = new() {
        {"A3_S2_A3_S3", new Vector3(-3329, -1024)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA3S3 = new() {
        {"A3_S3_A3_S7", new Vector3(-6113, -3280)},
        {"A3_S3_A3_S5", new Vector3(6926, -2912)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA3S5 = new() {
        {"A3_S5_A3_S3", new Vector3(-3136, -2288)},
        {"A3_S5_A10_S1", new Vector3(-6239, -2288)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA3S7 = new() {
        {"A3_S7_A3_S1", new Vector3(-7585, 176)},
        {"A3_S7_A3_S3", new Vector3(-6067, -2032)},
        {"A3_S7_A11_S1", new Vector3(-8674, -2028)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA4S1 = new() {
        {"A4_S1_A4_SG7", new Vector3(-1081, -3568)},
        {"A4_S1_A4_S2", new Vector3(-2031, -3168)},
        {"A4_S1_A4_S6", new Vector3(-1296, -4464)},
        {"A4_S1_A5_S1", new Vector3(901, -4368)},
        {"A4_S1_A6_S1", new Vector3(492, -4976)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA4S2 = new() {
        {"A4_S2_A4_SG1", new Vector3(-6085, -4192)},
        {"A4_S2_A4_S1", new Vector3(-1820, -4416)},
        {"A4_S2_A4_S3", new Vector3(-4277, -4128)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA4S3 = new() {
        {"A4_S3_A4_SG4", new Vector3(-4416, -6080)},
        {"A4_S3_A4_S2", new Vector3(-3719, -5776)},
        {"A4_S3_A4_S4", new Vector3(-5664, -6080)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA4S6 = new() {
        {"A4_S6_A4_S3", new Vector3(1907, -3744)},
        {"A4_S6_A4_S1", new Vector3(4914, -3700)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA5S1 = new() {
        {"A5_S1_A4_S1", new Vector3(-2943, -4032)},
        {"A5_S1_A5_S4_1", new Vector3(-1820, -3792)},
        {"A5_S1_A5_S4_2", new Vector3(4128, -3472)},
        {"A5_S1_A5_S4b", new Vector3(4392, -3472)},
        {"A5_S1_A6_S1_1", new Vector3(-1405, -4272)},
        {"A5_S1_A6_S1_2", new Vector3(1481, -4366)},
        {"A5_S1_A6_S1_3", new Vector3(2034, -4260)},
        {"A5_S1_A7_S1", new Vector3(6092, -4247)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA5S2 = new() {
        {"A5_S2_A5_S3", new Vector3(496, -4624)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA5S3 = new() {
        {"A5_S3_A5_S2", new Vector3(-719, -8176)},
        {"A5_S3_A6_S1", new Vector3(3327, -7552)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA5S4 = new() {
        {"A5_S4_A5_S1_1", new Vector3(-1008, -4352)},
        {"A5_S4_A5_S1_2", new Vector3(4816, -4864)},
        {"A5_S4_A5_S4d", new Vector3(-61, -3664)},
        {"A5_S4_A5_S5", new Vector3(2787, -3872)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA5S5 = new() {
        {"A5_S5_A5_S4", new Vector3(-3856, -2288)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA6S1 = new() {
        {"A6_S1_A4_S1", new Vector3(866, -7486)},
        {"A6_S1_A5_S1_1", new Vector3(3165, -7360)},
        {"A6_S1_A5_S1_2", new Vector3(5856, -7184)},
        {"A6_S1_A6_S3", new Vector3(8079, -7488)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA6S3 = new() {
        {"A6_S3_A6_S1", new Vector3(5110, -7408)},
        {"A6_S3_A0_S7", new Vector3(9381, -6288)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA7S1 = new() {
        {"A7_S1_AG_S1", new Vector3(3451, -4448)},
        {"A7_S1_A5_S1", new Vector3(-1294, -4320)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA10S1 = new() {
        {"A10_S1_A3_S1", new Vector3(570, -3695)},
        {"A10_S1_A3_S2", new Vector3(2099, -4493)},
        {"A10_S1_A3_S5", new Vector3(4300, -5328)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA11S1 = new() {
        {"A11_S1_A3_S7", new Vector3(570, -3695)},
    };
    private static readonly Dictionary<String, (HashSet<InterestPointData>? IPDs, Dictionary<String, Vector3> ConnDict)> ConnectorSetMap = new() {
        {"Minimap_AG_S1_SenateHall_Setting", (null, ConnectorDictAGS1)},
        {"Minimap_AG_S2_YiBase_Setting", (null, ConnectorDictAGS2)},
        {"Minimap_A0_S7_CaveReturned_Setting", (null, ConnectorDictA0S7)},
        {"Minimap_A0_S8_VillageReturned_Setting", (null, ConnectorDictA0S8)},
        {"Minimap_A0_S9_AltarReturned_Setting", (null, ConnectorDictA0S9)},
        {"Minimap_A0_S10_SpaceshipYard_Setting", (null, ConnectorDictA0S10)},
        {"Minimap_A1_S1_HumanDisposal_Final_Setting", (null, ConnectorDictA1S1)},
        {"Minimap_A1_S2_ConnectionToElevator_Final_Setting", (null, ConnectorDictA1S2)},
        {"Minimap_A1_S3_InnerHumanDisposal_Final_Setting", (null, ConnectorDictA1S3)},
        {"Minimap_A2_S1_ReactorMiddle_Final_Setting", (null, ConnectorDictA2S1)},
        {"Minimap_A2_S2_ReactorRight_Final_Setting", (null, ConnectorDictA2S2)},
        {"Minimap_A2_S3_ReactorLeft_Final_Setting", (null, ConnectorDictA2S3)},
        {"Minimap_A2_S5_BossHorseman_Final_Setting", (null, ConnectorDictA2S5)},
        {"Minimap_A2_S6_LogisticCenter_Final_Setting", (null, ConnectorDictA2S6)},
        {"Minimap_A3_S1_GardenRuins_Final_Setting", (null, ConnectorDictA3S1)},
        {"Minimap_A3_S2_GreenHouse_Final_Setting", (null, ConnectorDictA3S2)},
        {"Minimap_A3_S3_OxygenChamber_Final_Setting", (null, ConnectorDictA3S3)},
        {"Minimap_A3_S5_BossGouMang_Final_Setting", (null, ConnectorDictA3S5)},
        {"Minimap_A3_S7_DragonWay_Final_Setting", (null, ConnectorDictA3S7)},
        {"Minimap_A4_S1_NewBridgeToWarehouse_Final_Setting", (null, ConnectorDictA4S1)},
        {"Minimap_A4_S2_RouteToControlRoom_Final_Setting", (null, ConnectorDictA4S2)},
        {"Minimap_A4_S3_ControlRoom_Final_Setting", (null, ConnectorDictA4S3)},
        {"Minimap_A4_S6_DaoBase_Final_Setting", (null, ConnectorDictA4S6)},
        {"Minimap_A5_S1_CastleHub_remake_Setting", (null, ConnectorDictA5S1)},
        {"Minimap_A5_S2_Jail_Remake_Final_Setting", (null, ConnectorDictA5S2)},
        {"Minimap_A5_S3_UnderCastle_Remake_4wei_Setting", (null, ConnectorDictA5S3)},
        {"Minimap_A5_S4_CastleMid_Remake_5wei_Setting", (null, ConnectorDictA5S4)},
        {"Minimap_A5_S5_JieChuanHall_Setting", (null, ConnectorDictA5S5)},
        {"Minimap_A6_S1_AbandonMine_Remake_4wei_Setting", (null, ConnectorDictA6S1)},
        {"Minimap_A6_S3_Tutorial_And_SecretBoss_Remake_Setting", (null, ConnectorDictA6S3)},
        {"Minimap_A7_S1_BrainRoom_Remake_Setting", (null, ConnectorDictA7S1)},
        {"Minimap_A10_S1_TombEntrance_remake_Setting", (null, ConnectorDictA10S1)},
        {"Minimap_A11_S1_Hospital_remake_Setting", (null, ConnectorDictA11S1)},
    };

    public static HashSet<InterestPointData> GetConnections(String region) {
        if (!ConnectorSetMap.ContainsKey(region)) return [];
        if (ConnectorSetMap[region].IPDs != null) return ConnectorSetMap[region].IPDs!;

        // Empty, need to initialize
        HashSet<InterestPointData> IPDs = [];
        foreach ((String conn, Vector3 loc) in ConnectorSetMap[region].ConnDict) {
            IPDs.Add(InterestDataMapping.CreateIPD(conn, loc));
        }
        ConnectorSetMap[region] = (IPDs, ConnectorSetMap[region].ConnDict);
        return ConnectorSetMap[region].IPDs!;
    }
}
