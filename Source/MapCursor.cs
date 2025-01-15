using NineSolsTracker;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapCursor : MonoBehaviour {
    private GameObject? cursorObj;
    private GameObject? hoverUIObj;
    private GameObject? MapMask;
    private Vector2 sizeHovering = new(0.5f, 0.5f);
    private Vector2 sizeNormal = new(0.4f, 0.4f);
    private RectTransform? cursorRect;
    private Rect worldCorners;
    private TextMeshProUGUI? hoverUI;
    private Vector3 hoverUILocalPos = new(-750, -200, 0);

    private bool Init() {
        // Last one initialized => all initialized
        if (hoverUI) return true;

        // Get sprite
        Sprite? TrackerPointSprite = AssetManager.CursorSprite;
        if (!TrackerPointSprite) return false;

        // Needed for further steps
        GameObject MinimapPanel = GameObject.Find("MinimapPanel");
        if (!MinimapPanel) {
            // Not ingame yet probably
            return false;
        }

        // Get cursor obj
        if (cursorRect == null) {
            MapMask = GameObject.Find("MapMask(Mask2)");
            if (!MapMask) return false;
            cursorObj = MinimapPanel.AddChildrenGameObject("TrackerCursor");
            Image cursorImg = cursorObj.AddComponent<Image>();
            cursorRect = cursorObj.GetComponent<RectTransform>();
            cursorRect.sizeDelta = sizeNormal;
            cursorImg.sprite = TrackerPointSprite;
            Vector3[] corners = new Vector3[4];
            cursorRect!.GetWorldCorners(corners);
            worldCorners = new(corners[0].x, corners[0].y,corners[2].x-corners[0].x,corners[2].y-corners[0].y);
        }

        // Get a TMPro object for the hover UI
        // Just reuse NoMapData
        if (!hoverUIObj) {
            hoverUIObj = MinimapPanel.GetComponent<MapPanelController>().NoMapMessagePanel.gameObject;
            if (!hoverUIObj) return false;
            hoverUI = hoverUIObj.GetComponent<TextMeshProUGUI>();
            hoverUI.transform.localPosition = hoverUILocalPos;
            hoverUI.fontSize = 20;
            hoverUIObj.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
            hoverUIObj.SetActive(true);
        }
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
                        (string IPDname, InterestDataMapping.IPDKind kind) = InterestDataMapping.GetHumanReadable(MIC.bindingFlag);
                        #if DEBUG
                        Log.Info("Hovering over: '" + InterestDataMapping.GetHumanReadable(MIC.bindingFlag) + "'");
                        #endif
                        hoverUI!.text = "Location Info:<br>" + IPDname + "<br>Type: " + kind.ToString();
                        if (MIC.bindingFlag.IsSolved) hoverUI.text += "<br>Location already checked!";
                        hoverUIObj!.SetActive(true);
                        
                        cursorRect!.sizeDelta = sizeHovering;
                        cursorRect.Rotate(0, 0, 1);
                        return;
                    }
                }
            }
            cursorRect!.sizeDelta = sizeNormal;
            hoverUIObj!.SetActive(false);
        }
    }
}
