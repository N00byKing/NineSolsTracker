using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Models;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Newtonsoft.Json;
using NineSolsTracker;
using UnityEngine;
using UnityEngine.UIElements;

// Sync: APSaveManager.cs in AP mod
public class APConnectionData {
    public string? hostname;
    public int port;
    public string? slotName;
    public string password = "";
    public string? roomId;
}
// Sync: APSaveManager.cs in AP mod
public class APRandomizerSaveData {
    public APConnectionData? apConnectionData;
    public Dictionary<string, bool>? locationsChecked;
    public Dictionary<string, int>? itemsAcquired;
    // TODO: scouts and hints
}

[HarmonyPatch]
public static class APConnection {
    public class APItem(bool _local, bool _progression) {
        public bool isLocal = _local;
        public bool isProgression = _progression;
    }
    public static Dictionary<long, APItem>? LocationInfo { get { return session == null ? null : _LocationInfo; } }
    private static readonly Dictionary<long, APItem> _LocationInfo = [];

    private static ArchipelagoSession? session = null;
    private static APRandomizerSaveData? saveData = null;

    // Sync: APSaveManager.cs in AP mod
    private static string APSaveDataPathForSlot(int i) {
        var saveSlotsPath = Application.persistentDataPath;
        var saveSlotVanillaFolderName = SaveManager.GetSlotDirPath(i);
        var saveSlotAPModFileName = saveSlotVanillaFolderName + "_Ixrec_ArchipelagoRandomizer.json";
        return saveSlotsPath + "/" + saveSlotAPModFileName;
    }

    [HarmonyPostfix, HarmonyPatch(typeof(StartMenuLogic), "CreateOrLoadSaveSlotAndPlay")]
    public static void GetAPDetails(StartMenuLogic __instance, int slotIndex, bool SaveExists, bool LoadFromBackup = false, bool memoryChallengeMode = false) {
        string saveSlotAPModFilePath = APSaveDataPathForSlot(slotIndex);
        if (!File.Exists(saveSlotAPModFilePath)) {
            Log.Info("Did not find AP connection data, integration disabled");
            return;
        }
        saveData = JsonConvert.DeserializeObject<APRandomizerSaveData>(File.ReadAllText(saveSlotAPModFilePath));
        if (saveData != null)
            APConnection.ConnectAndGetSlotData(saveData.apConnectionData!.hostname!, saveData.apConnectionData.port, saveData.apConnectionData.slotName!, saveData.apConnectionData.password);
    }

    public static void ConnectAndGetSlotData(string ip, int port, string slot, string password) {
        session = ArchipelagoSessionFactory.CreateSession(ip, port);
        LoginResult result = session.TryConnectAndLogin("Nine Sols", slot, ItemsHandlingFlags.NoItems, null, null, null, password);
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
