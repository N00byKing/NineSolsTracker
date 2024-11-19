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
    private RectTransform? cursorRect;

    private bool isInit = false;

    private bool Init() {
        if (isInit) return true;
        // Get sprite
        // Look for Chest sprite
        Sprite[] Sprites = Resources.FindObjectsOfTypeAll<Sprite>();
        Sprite? TrackerPointSprite = null;
        foreach (Sprite s in Sprites) {
            if (s.name == "Map_icon_5") {
                TrackerPointSprite = s;
                break;
            }
        }
        if (!TrackerPointSprite) return false;

        // Get cursor obj
        GameObject MinimapPanel = GameObject.Find("MinimapPanel");
        if (!MinimapPanel) return false;
        MapMask = GameObject.Find("MapMask(Mask2)");
        if (!MapMask) return false;
        cursorObj = MinimapPanel.AddChildrenGameObject("TrackerCursor");
        cursorImg = cursorObj.AddComponent<Image>();
        cursorRect = cursorObj.GetComponent<RectTransform>();
        cursorRect.sizeDelta = new Vector2(0.4f, 0.4f);
        cursorImg.sprite = TrackerPointSprite;
        isInit = true;
        return true;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        if (Init() && MapMask.activeInHierarchy) {
            foreach (MapIconController MIC in MapMask.GetComponentsInChildren<MapIconController>(false)) {
                if (MapPoints.IsValidLocation(MIC.bindingFlag)) {
                    // Check if intersects
                    Vector3[] corners = new Vector3[4];
                    MIC.myRect.GetWorldCorners(corners);
                    Rect rec = new(corners[0].x,corners[0].y,corners[2].x-corners[0].x,corners[2].y-corners[0].y);

                    cursorRect.GetWorldCorners(corners);
                    Rect rec2 = new Rect(corners[0].x, corners[0].y,corners[2].x-corners[0].x,corners[2].y-corners[0].y);
                    if (rec.Overlaps(rec2)) {
                        Log.Info("Yeet");
                    }
                }
            }
        }
    }
}
