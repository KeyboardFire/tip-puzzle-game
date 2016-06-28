using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour {

    static Vector2 btnSize = new Vector2(200, 50);
    const int btnSpacing = 10;

    public void Start() {
        // create menu buttons
        var buttons = new GameObject();

        var buttonsRect = buttons.AddComponent<RectTransform>();
        buttonsRect.sizeDelta = new Vector2(0, 600);

        var buttonsLayout = buttons.AddComponent<VerticalLayoutGroup>();
        buttonsLayout.spacing = btnSize.y;
        buttonsLayout.padding = new RectOffset((int)btnSize.y, (int)btnSize.y,
                (int)btnSize.y, (int)btnSize.y);
        buttonsLayout.childAlignment = TextAnchor.UpperCenter;

        int n = 0;
        foreach (Object levelFile in Resources.LoadAll("Levels")) {
            var level = (TextAsset) levelFile;

            var btnObj = new GameObject();
            btnObj.transform.parent = buttons.transform;

            var rect = btnObj.AddComponent<RectTransform>();
            rect.sizeDelta = btnSize;

            var btn = btnObj.AddComponent<Button>();
            btn.onClick.AddListener(() => {
                GlobalData._currentLevel = level;
                SceneManager.LoadScene("MainScene");
            });

            var imgObj = new GameObject();
            imgObj.transform.parent = btnObj.transform;

            var img = imgObj.AddComponent<Image>();

            var imgRect = img.GetComponent<RectTransform>();
            imgRect.sizeDelta = new Vector2(btnSize.x, btnSize.y - btnSpacing);
            imgRect.localScale = Vector2.one;
            imgRect.anchoredPosition = Vector2.zero;

            var txtObj = new GameObject();
            txtObj.transform.parent = btnObj.transform;

            var txt = txtObj.AddComponent<Text>();
            txt.text = "Level " + (n+1);
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.color = Color.black;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.resizeTextForBestFit = true;
            txt.resizeTextMinSize = 14;
            txt.resizeTextMaxSize = 30;

            var txtRect = txt.GetComponent<RectTransform>();
            txtRect.sizeDelta = btnSize;
            txtRect.localScale = Vector2.one;
            txtRect.anchoredPosition = Vector2.zero;

            ++n;
        }

        // make it scrollable
        var viewport = new GameObject();
        viewport.AddComponent<RectTransform>();

        viewport.transform.SetParent(transform);
        viewport.transform.localPosition = new Vector3(0, -btnSize.y, 0);
        viewport.transform.localScale = Vector3.one;

        buttons.transform.SetParent(viewport.transform);
        // hahahaha
        buttons.transform.localPosition = new Vector3(0, -10000, 0);
        buttons.transform.localScale = Vector3.one;

        var scroller = gameObject.AddComponent<ScrollRect>();
        scroller.viewport = viewport.GetComponent<RectTransform>();
        scroller.content = buttons.GetComponent<RectTransform>();
        scroller.horizontal = false;
        scroller.scrollSensitivity = 10;
    }

}
