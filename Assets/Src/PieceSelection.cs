using UnityEngine;

public class PieceSelection : MonoBehaviour {

    public GameObject _player;
    PlayerScript playerScript;

    void Awake() {
        playerScript = _player.GetComponent<PlayerScript>();
    }

    public void OnClick() {
        switch(gameObject.name) {
            case "king":
                playerScript.ChangePiece(Piece.Type.King);
                break;
            case "queen":
                playerScript.ChangePiece(Piece.Type.Queen);
                break;
            case "bishop":
                playerScript.ChangePiece(Piece.Type.Bishop);
                break;
            case "rook":
                playerScript.ChangePiece(Piece.Type.Rook);
                break;
            case "knight":
                playerScript.ChangePiece(Piece.Type.Knight);
                break;
            case "pawn":
                playerScript.ChangePiece(Piece.Type.Pawn);
                break;
        }
    }
}
