using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    public GameObject player;
    PlayerScript playerScript;

    public GameObject bishopPiece;
    public GameObject kingPiece;
    public GameObject knightPiece;
    public GameObject pawnPiece;
    public GameObject queenPiece;
    public GameObject rookPiece;

    void Awake() {
        playerScript = player.GetComponent<PlayerScript>();
    }

    public void RedrawNumbers() {
        for (int i = 0; i < playerScript.movesLeft.Length; ++i) {
            GameObject updateObj;

            switch ((Piece.Type)i) {
            case Piece.Type.Bishop:
                updateObj = bishopPiece;
                break;
            case Piece.Type.King:
                updateObj = kingPiece;
                break;
            case Piece.Type.Knight:
                updateObj = knightPiece;
                break;
            case Piece.Type.Pawn:
                updateObj = pawnPiece;
                break;
            case Piece.Type.Queen:
                updateObj = queenPiece;
                break;
            case Piece.Type.Rook:
                updateObj = rookPiece;
                break;
            default:
                // unreachable
                return;
            }

            int moves = playerScript.movesLeft[i];
            updateObj.GetComponent<Image>().color = moves == 0 ? Color.black : Color.white;
            updateObj.transform.GetComponentInChildren<Text>().text = moves.ToString();
        }
    }

}
