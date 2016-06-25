using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour {

    public string scene;

    public void OnClick() {
        SceneManager.LoadScene(scene);
    }

}
