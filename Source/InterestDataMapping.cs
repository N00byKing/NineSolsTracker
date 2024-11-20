using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

static class InterestDataMapping {
    public static bool IsValidLocation(InterestPointData IPD) {
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

    private static readonly Dictionary<String, String> ToHumanReadable = new()
    {
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag27cfe3ad-4c1d-4d51-bcee-80e5c7bebf24", "Apeman Facility (Monitoring) - Jin Chest near first big enemy"},
        {"A1_S1_HumanDisposal_Final_MoneyCrateFlag71961019-5ce4-4bcb-834b-c5b84360eb4a", "Apeman Facility (Monitoring) - Jin Chest in Tunnels"},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedb7eb8443-41b9-42cc-b809-0ff7dd487e96", "Apeman Facility (Monitoring) - Item from Meat Machine"},
        {"A1_S1_HumanDisposal_Final_[Variable] Pickedf78b77c1-de42-4a36-87ce-255804e9826d", "Apeman Facility (Monitoring) - Encyclopedia Item"}
    };
    public static String GetHumanReadable(InterestPointData IPD) {
        if (ToHumanReadable.ContainsKey(IPD.name)) return ToHumanReadable[IPD.name];
        return IPD.name;
    }
}
