using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour {

    public string _scene;

    public void OnClick() {
        SceneManager.LoadScene(_scene);
    }

}
