using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TileEndBehavior : TileBehavior {

    protected override void OnMoveSuccess() {
        StartCoroutine(Delay(2));
    }

    IEnumerator Delay(float time) {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MenuScene");
    }

}
