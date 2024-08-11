using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
// using NineSolsAPI;

namespace ExampleMod;

// [BepInDependency(NineSolsAPICore.PluginGUID)] if you want to use the API
[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class ExampleMod : BaseUnityPlugin {
    // https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/4_configuration.html
    private ConfigEntry<bool> enableSomethingConfig;
    private ConfigEntry<KeyboardShortcut> somethingKeyboardShortcut;

    private void Awake() {
        Log.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);

        enableSomethingConfig = Config.Bind("General.Something", "Enable", true, "Enable the thing");
        somethingKeyboardShortcut = Config.Bind("General.Something", "Shortcut",
            new KeyboardShortcut(KeyCode.Q, KeyCode.LeftShift), "Shortcut to execute");

        // If you want to use the modding API, enable it in the .csproj.
        // It provides utilities like the KeybindManager, utilities for Instantiating objects including the 
        // NineSols lifecycle hooks, displaying toast messages and preloading objects from other scenes.
        // If you do use the API make sure do download the [NineSolsAPI.dll](https://github.com/nine-sols-modding/NineSolsAPI/releases/)
        // and put it in BepInEx/plugins/.

        // KeybindManager.Add(this, TestMethod, () => somethingKeyboardShortcut.Value);

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void TestMethod() {
        if (!enableSomethingConfig.Value) return;
        // ToastManager.Toast("Shortcut activated");
    }

    private void OnDestroy() {
        // Make sure to clean up resources here to support hot reloading
    }
}