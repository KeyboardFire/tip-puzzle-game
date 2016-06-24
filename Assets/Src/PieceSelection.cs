using UnityEngine;

public class PieceSelection : MonoBehaviour {

    public GameObject player;
    PlayerScript playerScript;

    void Awake() {
        playerScript = player.GetComponent<PlayerScript>();
    }

    public void OnClick() {
        switch(gameObject.name) {
            case "king":
                playerScript.ChangePiece(PlayerScript.PieceType.King);
                break;
            case "queen":
                playerScript.ChangePiece(PlayerScript.PieceType.Queen);
                break;
            case "bishop":
                playerScript.ChangePiece(PlayerScript.PieceType.Bishop);
                break;
            case "rook":
                playerScript.ChangePiece(PlayerScript.PieceType.Rook);
                break;
            case "knight":
                playerScript.ChangePiece(PlayerScript.PieceType.Knight);
                break;
            case "pawn":
                playerScript.ChangePiece(PlayerScript.PieceType.Pawn);
                break;
        }
    }
}
