using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour {

    public string levelName;

    public void OnClick() {
        GlobalData.currentLevel = levelName;
        SceneManager.LoadScene("MainScene");
    }

}
