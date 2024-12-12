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
    private static readonly Dictionary<String, Vector3> ConnectorDictA10S1 = new() {
        {"A10_S1_A3_S1", new Vector3(570, -3695)},
        {"A10_S1_A3_S5", new Vector3(4300, -5328)},
    };
    private static readonly Dictionary<String, Vector3> ConnectorDictA11S1 = new() {
        {"A11_S1_A3_S7", new Vector3(570, -3695)},
    };
    private static readonly Dictionary<String, (HashSet<InterestPointData>? IPDs, Dictionary<String, Vector3> ConnDict)> ConnectorSetMap = new() {
        {"Minimap_AG_S1_SenateHall_Setting", (null, ConnectorDictAGS1)},
        {"Minimap_AG_S2_YiBase_Setting", (null, ConnectorDictAGS2)},
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
