using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

    public GameObject _player;
    PlayerScript playerScript;

    void Awake() {
        playerScript = _player.GetComponent<PlayerScript>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            playerScript.ChangePiece(Piece.Type.Bishop);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            playerScript.ChangePiece(Piece.Type.King);
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            playerScript.ChangePiece(Piece.Type.Knight);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            playerScript.ChangePiece(Piece.Type.Pawn);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            playerScript.ChangePiece(Piece.Type.Queen);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            playerScript.ChangePiece(Piece.Type.Rook);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            SceneManager.LoadScene("MainScene");
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene("MenuScene");
        }
    }

}
