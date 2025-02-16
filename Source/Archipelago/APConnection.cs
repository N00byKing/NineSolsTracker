using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using NineSolsTracker;
using UnityEngine;
using UnityEngine.UIElements;

public static class APConnection {
    public class APItem(bool _local, bool _progression) {
        public bool isLocal = _local;
        public bool isProgression = _progression;
    }
    public static Dictionary<long, APItem>? LocationInfo { get { return session == null ? null : _LocationInfo; } }
    private static readonly Dictionary<long, APItem> _LocationInfo = [];

    private static ArchipelagoSession? session = null;

    public static void ConnectAndGetSlotData(String ip, int port, String slot) {
        ip = "localhost";
        port = 38281;
        session = ArchipelagoSessionFactory.CreateSession(ip, port);
        LoginResult result = session.TryConnectAndLogin("Nine Sols", slot, ItemsHandlingFlags.NoItems);
        if (!result.Successful) {
            Log.Error("Could not connect to Archipelago!");
            return;
        }
        Log.Info("Connected to Archipelago");
        var success = (LoginSuccessful)result;
        // success.SlotData parsing
        List<long> loc_ids = [];
        foreach (var x in InterestDataMapping.IPD_TABLE) {
            if (x.Value.AP_ID == InterestDataMapping.DISABLED_AP) continue;
            loc_ids.Add(x.Value.AP_ID);
        }
        Task scoutTask = Task.Run(() => session.Locations.ScoutLocationsAsync([.. loc_ids]).ContinueWith(LocationScoutHandler));
        if (!scoutTask.Wait(TimeSpan.FromSeconds(5))) {
            Log.Error("Could not scout locations!");
        }
    }

    private static void LocationScoutHandler(Task<Dictionary<long, ScoutedItemInfo>> task) {
        foreach ( (long id, ScoutedItemInfo itemInfo) in task.Result) {
            _LocationInfo[id] = new(itemInfo.Player == session!.Players.ActivePlayer, itemInfo.Flags.HasFlag(ItemFlags.Advancement));
        }
    }
}
