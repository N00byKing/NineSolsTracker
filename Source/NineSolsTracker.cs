using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace NineSolsTracker;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class NineSolsTracker : BaseUnityPlugin {
    // https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/4_configuration.html

    private Harmony harmony = null!;
    private MapCursor? cursor;

    private void Awake() {
        Log.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);

        cursor = gameObject.AddComponent<MapCursor>();

        // Load patches from any class annotated with @HarmonyPatch
        harmony = Harmony.CreateAndPatchAll(typeof(NineSolsTracker).Assembly);

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        APConnection.ConnectAndGetSlotData("localhost", 38281, "yee");
    }

    private void OnDestroy() {
        // Make sure to clean up resources here to support hot reloading

        harmony.UnpatchSelf();
    }
}
