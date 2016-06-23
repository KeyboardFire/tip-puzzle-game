using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public enum PieceType {
        Bishop, King, Knight, Pawn, Queen, Rook
    }
    public static string pieceChars = "BKNPQR";

    PieceType pieceType;
    public GameObject pieceBishop;
    public GameObject pieceKing;
    public GameObject pieceKnight;
    public GameObject piecePawn;
    public GameObject pieceQueen;
    public GameObject pieceRook;

    public int[] movesLeft = new int[7];

    Vector2 pos = new Vector2(0, 0);

    public GameObject canvas;
    CanvasScript canvasScript;

    void Awake() {
        canvasScript = canvas.GetComponent<CanvasScript>();
    }

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
            --movesLeft[(int)pieceType];
            canvasScript.RedrawNumbers();
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
        if (movesLeft[(int)pieceType] == 0) return false;

        int dx = Mathf.RoundToInt(movePos.x - pos.x),
            dy = Mathf.RoundToInt(movePos.y - pos.y);
        int adx = Mathf.Abs(dx), ady = Mathf.Abs(dy);

        switch (pieceType) {

        case PieceType.Knight:
            return adx + ady == 3 && (adx == 1 || adx == 2);

        case PieceType.Pawn:
            return dx == 0 && (dy == 1 || dy == 2 &&
                    BoardGenerator.IsPassable(new Vector2(pos.x, pos.y + 1)));

        case PieceType.Queen:
        case PieceType.Rook:
        case PieceType.Bishop:
        case PieceType.King:
            // make sure we're actually moving in a straight line
            if (dx != 0 && dy != 0 && adx != ady) return false;

            // now make sure there's nothing between the start and end squares
            Vector2 testSquare = pos;
            while (true) {
                testSquare.x += System.Math.Sign(dx);
                testSquare.y += System.Math.Sign(dy);

                // test is exclusive on both ends (this is why we inf. loop)
                if (testSquare == movePos) break;

                if (!BoardGenerator.IsPassable(testSquare)) return false;
            }

            // finally, check direction for bishop/rook and distance for king
            if (pieceType == PieceType.Rook   && (dx != 0 && dy != 0)) return false;
            if (pieceType == PieceType.Bishop && (dx == 0 || dy == 0)) return false;
            if (pieceType == PieceType.King   && (adx > 1 || ady > 1)) return false;

            // all checks pass!
            return true;

        }

        return false; //unreachable
    }

}
