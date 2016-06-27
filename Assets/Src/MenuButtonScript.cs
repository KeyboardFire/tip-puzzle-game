using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour {

    static Vector2 btnSize = new Vector2(200, 50);
    static int btnPadding = 10;

    public void Start() {
        int n = 0;
        foreach (Object levelFile in Resources.LoadAll("Levels")) {
            TextAsset level = (TextAsset) levelFile;

            var btnObj = new GameObject();
            btnObj.transform.parent = transform;

            var rect = btnObj.AddComponent<RectTransform>();
            rect.sizeDelta = btnSize;
            rect.localScale = Vector2.one;
            rect.anchoredPosition = new Vector2(0,
                    n * -(btnSize.y + btnPadding) - btnPadding);
            rect.pivot = new Vector2(0.5f, 1f);

            var btn = btnObj.AddComponent<Button>();
            btn.onClick.AddListener(() => {
                GlobalData.currentLevel = level;
                SceneManager.LoadScene("MainScene");
            });

            var img = btnObj.AddComponent<Image>();

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
    }

}
