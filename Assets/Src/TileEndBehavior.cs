using UnityEngine;
using UnityEngine.SceneManagement;

public class TileEndBehavior : TileBehavior {

    protected override void OnMoveSuccess() {
        SceneManager.LoadScene("MenuScene");
    }

}
