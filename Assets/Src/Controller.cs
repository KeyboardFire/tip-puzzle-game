using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

    public GameObject player;
    PlayerScript playerScript;

    void Awake() {
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) playerScript.ChangePiece(PlayerScript.PieceType.Bishop);
        if (Input.GetKeyDown(KeyCode.K)) playerScript.ChangePiece(PlayerScript.PieceType.King);
        if (Input.GetKeyDown(KeyCode.N)) playerScript.ChangePiece(PlayerScript.PieceType.Knight);
        if (Input.GetKeyDown(KeyCode.P)) playerScript.ChangePiece(PlayerScript.PieceType.Pawn);
        if (Input.GetKeyDown(KeyCode.Q)) playerScript.ChangePiece(PlayerScript.PieceType.Queen);
        if (Input.GetKeyDown(KeyCode.R)) playerScript.ChangePiece(PlayerScript.PieceType.Rook);
        if (Input.GetKeyDown(KeyCode.E)) SceneManager.LoadScene("MainScene");
        if (Input.GetKeyDown(KeyCode.M)) SceneManager.LoadScene("MenuScene");
    }

}
