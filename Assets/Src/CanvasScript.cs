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

    void Start() {
        playerScript = player.GetComponent<PlayerScript>();
    }

    public void RedrawNumbers() {
        for (int i = 0; i < playerScript.movesLeft.Length; ++i) {
            GameObject updateObj;

            switch ((PlayerScript.PieceType)i) {
            case PlayerScript.PieceType.Bishop:
                updateObj = bishopPiece;
                break;
            case PlayerScript.PieceType.King:
                updateObj = kingPiece;
                break;
            case PlayerScript.PieceType.Knight:
                updateObj = knightPiece;
                break;
            case PlayerScript.PieceType.Pawn:
                updateObj = pawnPiece;
                break;
            case PlayerScript.PieceType.Queen:
                updateObj = queenPiece;
                break;
            case PlayerScript.PieceType.Rook:
                updateObj = rookPiece;
                break;
            default:
                // unreachable
                return;
            }

            updateObj.transform.GetComponentInChildren<Text>().text =
                playerScript.movesLeft[i].ToString();
        }
    }

}
