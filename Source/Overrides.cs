using System;
using System.Collections.Generic;
using UnityEngine;

static class Overrides {
    private static readonly Dictionary<String, Vector2> locOverrides = new() {
        {"AG_S2_YiBase_[Variable] Picked479cd19b-95e3-4e08-864c-9042d4688ba9", new(3752, -4448)},
        {"A0_S7_CaveReturned_[Variable] Pickeded3de528-ecbf-4180-be47-fa647646cb4f", new(2387, -3840)},
        {"A4_S1_NewBridgeToWarehouse_Final_[Variable] Pickede8951183-10da-49e5-abe0-6547c01e9aa1", new(-178, -2624)},
        {"A4_S2_RouteToControlRoom_Final_[Variable] Picked72e2240c-cf0e-4d85-b326-c19728f42a80", new(-4509, -3376)},
        {"A4_SG4_[Variable] Picked39047b0a-4a21-4f81-8b0e-a333de6bd4e1", new(2824, -5744)},
        {"A6_S3_Tutorial_And_SecretBoss_Remake_[Variable] Picked698409a7-a500-49f4-80db-b7338a50eb70", new(5837, -6288)},
    };

    public static void ProcessOverrides(InterestPointData IPD) {
        // Wrong locations
        if (locOverrides.ContainsKey(IPD.name)) {
            IPD.worldPosition = locOverrides[IPD.name];
        }

        // AP Integration
        if (APConnection.LocationInfo != null && APConnection.LocationInfo.ContainsKey(InterestDataMapping.IPD_TABLE[IPD.name].AP_ID)) {
            APConnection.APItem item = APConnection.LocationInfo[InterestDataMapping.IPD_TABLE[IPD.name].AP_ID];
            if (!item.isLocal) {
                if (item.isProgression) {
                    InterestDataMapping.IPD_TABLE[IPD.name].kind = InterestDataMapping.IPDKind.ArchipelagoProgression;
                } else {
                    InterestDataMapping.IPD_TABLE[IPD.name].kind = InterestDataMapping.IPDKind.ArchipelagoNormal;
                }
            }
        }
    }
}
