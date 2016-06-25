using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenuButton : MonoBehaviour {

    public void OnClick() {
        SceneManager.LoadScene("MenuScene");
    }

}
