using UnityEngine;

public class PlayerScript : MonoBehaviour {

    enum PieceType {
        Bishop, King, Knight, Pawn, Queen, Rook
    }

    PieceType pieceType;
    public GameObject pieceBishop;
    public GameObject pieceKing;
    public GameObject pieceKnight;
    public GameObject piecePawn;
    public GameObject pieceQueen;
    public GameObject pieceRook;

    // called on initialization
    void Start() {
        ChangePiece(PieceType.Rook);
    }

    void ChangePiece(PieceType piece) {
        foreach (Transform child in transform) {
            Destroy(child);
        }

        GameObject pieceObj;
        switch (piece) {
        case PieceType.Bishop: pieceObj = Instantiate(pieceBishop); break;
        case PieceType.King:   pieceObj = Instantiate(pieceKing);   break;
        case PieceType.Knight: pieceObj = Instantiate(pieceKnight); break;
        case PieceType.Pawn:   pieceObj = Instantiate(piecePawn);   break;
        case PieceType.Queen:  pieceObj = Instantiate(pieceQueen);  break;
        case PieceType.Rook:   pieceObj = Instantiate(pieceRook);   break;
        default: return; // unreachable
        }

        pieceObj.transform.parent = transform;
        pieceObj.transform.localScale = transform.localScale;
        pieceObj.transform.Translate(0, Util.ChildrenBounds(pieceObj.transform)
                .extents.y, 0);
    }

    // returns whether the move was successful
    public bool MoveTo(Vector2 movePos) {
        var pos = transform.position;
        pos.x = movePos.x;
        pos.z = movePos.y;
        transform.position = pos;
        return true; // TODO
    }

}
