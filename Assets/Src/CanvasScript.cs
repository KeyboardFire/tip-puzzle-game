using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    public GameObject _player;
    PlayerScript playerScript;

    public GameObject _bishopPiece;
    public GameObject _kingPiece;
    public GameObject _knightPiece;
    public GameObject _pawnPiece;
    public GameObject _queenPiece;
    public GameObject _rookPiece;
    public GameObject _switchesIcon;

    void Awake() {
        playerScript = _player.GetComponent<PlayerScript>();
    }

    public void RedrawNumbers() {
        for (int i = 0; i < playerScript._movesLeft.Length; ++i) {
            GameObject updateObj;

            switch ((Piece.Type)i) {
            case Piece.Type.Bishop:
                updateObj = _bishopPiece;
                break;
            case Piece.Type.King:
                updateObj = _kingPiece;
                break;
            case Piece.Type.Knight:
                updateObj = _knightPiece;
                break;
            case Piece.Type.Pawn:
                updateObj = _pawnPiece;
                break;
            case Piece.Type.Queen:
                updateObj = _queenPiece;
                break;
            case Piece.Type.Rook:
                updateObj = _rookPiece;
                break;
            default:
                // unreachable
                return;
            }

            int moves = playerScript._movesLeft[i];
            updateObj.GetComponent<Image>().color =
                moves == 0 ? Color.black : Color.white;
            updateObj.transform.GetComponentInChildren<Text>().text =
                moves.ToString();
        }

        int switches = playerScript._switchesLeft;
        Debug.Log(switches);
        _switchesIcon.GetComponent<Image>().color =
            switches == 0 ? Color.black : Color.white;
        _switchesIcon.transform.GetComponentInChildren<Text>().text =
            switches.ToString();
    }

}
