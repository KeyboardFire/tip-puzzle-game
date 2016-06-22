using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public enum PieceType {
        Bishop, King, Knight, Pawn, Queen, Rook
    }

    PieceType pieceType;
    public GameObject pieceBishop;
    public GameObject pieceKing;
    public GameObject pieceKnight;
    public GameObject piecePawn;
    public GameObject pieceQueen;
    public GameObject pieceRook;

    Vector2 pos = new Vector2(0, 0);

    // called on initialization
    void Start() {
        ChangePiece(PieceType.Knight);
    }

    public void ChangePiece(PieceType piece) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        pieceType = piece;

        GameObject pieceObj;
        switch (piece) {
        case PieceType.Bishop: pieceObj = (GameObject) Instantiate(pieceBishop, transform.position, transform.rotation); break;
        case PieceType.King:   pieceObj = (GameObject) Instantiate(pieceKing, transform.position, transform.rotation);   break;
        case PieceType.Knight: pieceObj = (GameObject) Instantiate(pieceKnight, transform.position, transform.rotation); break;
        case PieceType.Pawn:   pieceObj = (GameObject) Instantiate(piecePawn, transform.position, transform.rotation);   break;
        case PieceType.Queen:  pieceObj = (GameObject) Instantiate(pieceQueen, transform.position, transform.rotation);  break;
        case PieceType.Rook:   pieceObj = (GameObject) Instantiate(pieceRook, transform.position, transform.rotation);   break;
        default: return; // unreachable
        }

        // add the piece as a child and scale it accordingly
        pieceObj.transform.parent = transform;
        pieceObj.transform.localScale = transform.localScale;

        // place the bottom of the piece on the board
        Bounds bounds = Util.ChildrenBounds(pieceObj.transform);
        pieceObj.transform.Translate(0, bounds.extents.y - bounds.center.y, 0);
    }

    // returns whether the move was successful
    public bool MoveTo(Vector2 movePos) {
        if (CanMove(movePos)) {
            ForceMove(movePos);
            return true;
        } else return false;
    }

    public void ForceMove(Vector2 movePos) {
        Vector3 oldPos = transform.position;
        oldPos.x = movePos.x;
        oldPos.z = movePos.y;
        transform.position = oldPos;
        pos = movePos;
    }

    bool CanMove(Vector2 movePos) {
        int dx = Mathf.RoundToInt(movePos.x - pos.x),
            dy = Mathf.RoundToInt(movePos.y - pos.y);
        int adx = Mathf.Abs(dx), ady = Mathf.Abs(dy);

        switch (pieceType) {
        case PieceType.Bishop:
            // TODO
            return false;
        case PieceType.King:
            // TODO
            return false;
        case PieceType.Knight:
            return adx + ady == 3 && (adx == 1 || adx == 2);
        case PieceType.Pawn:
            return dx == 0 && (dy == 1 || dy == 2 &&
                    BoardGenerator.IsPassable(new Vector2(pos.x, pos.y + 1)));
        case PieceType.Queen:
            // TODO
            return false;
        case PieceType.Rook:
            // TODO
            return false;
        }

        return false; //unreachable
    }

}
