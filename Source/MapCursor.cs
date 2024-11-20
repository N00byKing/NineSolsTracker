using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Com.LuisPedroFonseca.ProCamera2D;
using NineSolsTracker;
using HarmonyLib;
using NineSolsAPI;
using RCGFSM.PoolObjs;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.TerrainTools;

public class MapCursor : MonoBehaviour {
    private GameObject? cursorObj;
    private GameObject? MapMask;
    private Image? cursorImg;
    private Vector2 sizeHovering = new(0.5f, 0.5f);
    private Vector2 sizeNormal = new(0.4f, 0.4f);
    private RectTransform? cursorRect;
    private Rect worldCorners;

    private bool Init() {
        if (cursorObj && MapMask && cursorImg && cursorRect) return true;

        // Get sprite
        // Look for Chest sprite
        Sprite[] Sprites = Resources.FindObjectsOfTypeAll<Sprite>();
        Sprite? TrackerPointSprite = AssetManager.CursorSprite;
        if (!TrackerPointSprite) return false;

        // Get cursor obj
        GameObject MinimapPanel = GameObject.Find("MinimapPanel");
        if (!MinimapPanel) return false;
        MapMask = GameObject.Find("MapMask(Mask2)");
        if (!MapMask) return false;
        cursorObj = MinimapPanel.AddChildrenGameObject("TrackerCursor");
        cursorImg = cursorObj.AddComponent<Image>();
        cursorRect = cursorObj.GetComponent<RectTransform>();
        cursorRect.sizeDelta = sizeNormal;
        cursorImg.sprite = TrackerPointSprite;
        Vector3[] corners = new Vector3[4];
        cursorRect!.GetWorldCorners(corners);
        worldCorners = new(corners[0].x, corners[0].y,corners[2].x-corners[0].x,corners[2].y-corners[0].y);
        return true;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        if (Init() && MapMask!.activeInHierarchy) {
            foreach (MapIconController MIC in MapMask.GetComponentsInChildren<MapIconController>(false)) {
                if (InterestDataMapping.IsValidLocation(MIC.bindingFlag)) {
                    // Check if intersects
                    Vector3[] corners = new Vector3[4];
                    MIC.myRect.GetWorldCorners(corners);
                    Rect rec = new(corners[0].x,corners[0].y,corners[2].x-corners[0].x,corners[2].y-corners[0].y);

                    if (rec.Overlaps(worldCorners)) {
                        Log.Info("Hovering over: '" + InterestDataMapping.GetHumanReadable(MIC.bindingFlag) + "'");
                        cursorRect.sizeDelta = sizeHovering;
                        cursorRect.Rotate(0, 0, 1);
                        return;
                    }
                }
            }
            cursorRect.sizeDelta = sizeNormal;
        }
    }
}
