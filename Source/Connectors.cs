using System;
using System.Collections.Generic;
using UnityEngine;

static class Connectors {
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
    private static readonly Dictionary<String, (HashSet<InterestPointData>? IPDs, Dictionary<String, Vector3> ConnDict)> ConnectorSetMap = new() {
        {"Minimap_A1_S1_HumanDisposal_Final_Setting", (null, ConnectorDictA1S1)},
        {"Minimap_A1_S2_ConnectionToElevator_Final_Setting", (null, ConnectorDictA1S2)},
        {"Minimap_A1_S3_InnerHumanDisposal_Final_Setting", (null, ConnectorDictA1S3)},
        {"Minimap_A2_S1_ReactorMiddle_Final_Setting", (null, ConnectorDictA2S1)},
        {"Minimap_A2_S2_ReactorRight_Final_Setting", (null, ConnectorDictA2S2)},
        {"Minimap_A2_S3_ReactorLeft_Final_Setting", (null, ConnectorDictA2S3)},
        {"Minimap_A2_S5_BossHorseman_Final_Setting", (null, ConnectorDictA2S5)},
        {"Minimap_A2_S6_LogisticCenter_Final_Setting", (null, ConnectorDictA2S6)},
        
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
