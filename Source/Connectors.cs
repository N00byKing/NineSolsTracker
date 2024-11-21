using System;
using System.Collections.Generic;
using UnityEngine;

static class Connectors {
    private static readonly Dictionary<String, Vector3> ConnectorDictA1 = new() {
        {"A1_S1_A1_S2", new Vector3(-1550, -3900)}
    };
    private static readonly Dictionary<String, HashSet<InterestPointData>?> ConnectorSetMap = new() {
        {"Minimap_A1_S1_HumanDisposal_Final_Setting", null}
    };

    public static HashSet<InterestPointData> GetConnections(String region) {
        if (!ConnectorSetMap.ContainsKey(region)) return [];
        if (ConnectorSetMap[region] != null) return ConnectorSetMap[region]!;

        // Empty, need to initialize
        ConnectorSetMap[region] = [];
        foreach ((String conn, Vector3 loc) in ConnectorDictA1) {
            ConnectorSetMap[region]!.Add(InterestDataMapping.CreateIPD(conn, loc));
        }
        return ConnectorSetMap[region]!;
    }
}
