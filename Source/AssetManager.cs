using System;
using System.IO;
using System.Reflection;
using NineSolsTracker;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

static class AssetManager {
    public static Sprite? ChestSprite { get { Init(); return _chestsprite; } }
    private static Sprite? _chestsprite;

    public static Sprite? MoneySprite { get { Init(); return _moneysprite; } }
    private static Sprite? _moneysprite;

    public static Sprite? EncyclopediaSprite { get { Init(); return _encyclopediasprite; } }
    private static Sprite? _encyclopediasprite;
    public static Sprite? CursorSprite { get { Init(); return _cursorSprite; } }
    private static Sprite? _cursorSprite;
    public static Sprite? ConnectorSprite { get { Init(); return _connectorSprite; } }
    private static Sprite? _connectorSprite;
    public static Sprite? MinibossSprite { get { Init(); return _minibossSprite; } }
    private static Sprite? _minibossSprite;
    public static Sprite? APSprite { get { Init(); return _apSprite; } }
    private static Sprite? _apSprite;
    public static Sprite? APProgSprite { get { Init(); return _approgSprite; } }
    private static Sprite? _approgSprite;
    private static void Init() {
        if (_approgSprite) return; // If last one is initialized, all are
        _chestsprite = InitSprite("NineSolsTracker.ChestTexture.png");
        _moneysprite = InitSprite("NineSolsTracker.MoneyTexture.png");
        _encyclopediasprite = InitSprite("NineSolsTracker.EncyclopediaTexture.png");
        _cursorSprite = InitSprite("NineSolsTracker.CursorTexture.png");
        _connectorSprite = InitSprite("NineSolsTracker.ConnectorTexture.png");
        _minibossSprite = InitSprite("NineSolsTracker.MinibossTexture.png");
        _apSprite = InitSprite("NineSolsTracker.APTexture.png");
        _approgSprite = InitSprite("NineSolsTracker.APProgressionTexture.png");
    }

    private static Sprite? InitSprite(string assetname) {
        Assembly assembly = typeof(NineSolsTracker.NineSolsTracker).Assembly;
        Stream? resource = assembly.GetManifestResourceStream(assetname);
        if (resource == null) {
            Log.Error("Could not find Texture!");
            return null;
        }
        MemoryStream MemStr = new();
        resource.CopyTo(MemStr);
        Texture2D tex = new(92, 92, DefaultFormat.LDR, TextureCreationFlags.None);
        tex.LoadImage(MemStr.ToArray());
        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, 92, 92), new Vector2(0.5f, 0.5f), 2);
        newSprite.name = assetname;
        return newSprite;
    }
}
