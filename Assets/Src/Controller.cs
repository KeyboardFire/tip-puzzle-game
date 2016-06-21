using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject player;

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) player.GetComponent<PlayerScript>().ChangePiece(PlayerScript.PieceType.Bishop);
        if (Input.GetKeyDown(KeyCode.K)) player.GetComponent<PlayerScript>().ChangePiece(PlayerScript.PieceType.King);
        if (Input.GetKeyDown(KeyCode.N)) player.GetComponent<PlayerScript>().ChangePiece(PlayerScript.PieceType.Knight);
        if (Input.GetKeyDown(KeyCode.P)) player.GetComponent<PlayerScript>().ChangePiece(PlayerScript.PieceType.Pawn);
        if (Input.GetKeyDown(KeyCode.Q)) player.GetComponent<PlayerScript>().ChangePiece(PlayerScript.PieceType.Queen);
        if (Input.GetKeyDown(KeyCode.R)) player.GetComponent<PlayerScript>().ChangePiece(PlayerScript.PieceType.Rook);
    }

}
