using UnityEngine;

public class UndoButtonScript : MonoBehaviour {

    public GameObject _player;
    PlayerScript playerScript;

    void Awake() {
        playerScript = _player.GetComponent<PlayerScript>();
    }

    public void OnClick() {
        playerScript.UndoMove();
    }
}
