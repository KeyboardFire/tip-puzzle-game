using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Piece.Type pieceType;
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

    public void ChangePiece(Piece.Type piece) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        pieceType = piece;

        GameObject pieceObj;
        switch (piece) {
        case Piece.Type.Bishop:
            pieceObj = (GameObject) Instantiate(pieceBishop, transform.position, transform.rotation);
            break;
        case Piece.Type.King:
            pieceObj = (GameObject) Instantiate(pieceKing, transform.position, transform.rotation);
            break;
        case Piece.Type.Knight:
            pieceObj = (GameObject) Instantiate(pieceKnight, transform.position, transform.rotation);
            break;
        case Piece.Type.Pawn:
            pieceObj = (GameObject) Instantiate(piecePawn, transform.position, transform.rotation);
            break;
        case Piece.Type.Queen:
            pieceObj = (GameObject) Instantiate(pieceQueen, transform.position, transform.rotation);
            break;
        case Piece.Type.Rook:
            pieceObj = (GameObject) Instantiate(pieceRook, transform.position, transform.rotation);
            break;
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
        if (CanMove(pieceType, pos, movePos,
                    BoardGenerator.enemies.Exists((enemy) => {
                        return enemy.pos == movePos;
                    })) &&
                    BoardGenerator.enemies.TrueForAll((enemy) => {
                        return !CanMove(enemy.pieceType, enemy.pos, movePos, true);
                    })) {
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
        BoardGenerator.enemies.RemoveAll((enemy) => {
            if (enemy.pos == pos) {
                Destroy(enemy.gameObject);
                return true;
            } else return false;
        });
    }

    bool CanMove(Piece.Type movePieceType, Vector2 fromPos, Vector2 toPos,
            bool isCapture) {
        if (movesLeft[(int)movePieceType] == 0) return false;

        int dx = Mathf.RoundToInt(toPos.x - fromPos.x),
            dy = Mathf.RoundToInt(toPos.y - fromPos.y);
        int adx = Mathf.Abs(dx), ady = Mathf.Abs(dy);

        switch (movePieceType) {

        case Piece.Type.Knight:
            return adx + ady == 3 && (adx == 1 || adx == 2);

        case Piece.Type.Pawn:
            if (isCapture) {
                return adx == 1 && dy == 1;
            } else {
                return dx == 0 && (dy == 1 || dy == 2 &&
                        BoardGenerator.IsPassable(new Vector2(fromPos.x,
                                fromPos.y + 1)));
            }

        case Piece.Type.Queen:
        case Piece.Type.Rook:
        case Piece.Type.Bishop:
        case Piece.Type.King:
            // make sure we're actually moving in a straight line
            if (dx != 0 && dy != 0 && adx != ady) return false;

            // now make sure there's nothing between the start and end squares
            Vector2 testSquare = fromPos;
            while (true) {
                testSquare.x += System.Math.Sign(dx);
                testSquare.y += System.Math.Sign(dy);

                // test is exclusive on both ends (this is why we inf. loop)
                if (testSquare == toPos) break;

                if (!BoardGenerator.IsPassable(testSquare)) return false;
            }

            // finally, check direction for bishop/rook and distance for king
            if (movePieceType == Piece.Type.Rook && (dx != 0 && dy != 0)) {
                return false;
            }
            if (movePieceType == Piece.Type.Bishop && (dx == 0 || dy == 0)) {
                return false;
            }
            if (movePieceType == Piece.Type.King && (adx > 1 || ady > 1)) {
                return false;
            }

            // all checks pass!
            return true;

        }

        return false; //unreachable
    }

}
