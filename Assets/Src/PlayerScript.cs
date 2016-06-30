using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    Piece.Type pieceType;
    public Piece.Type PieceType { get { return pieceType; } }
    public GameObject _pieceBishop;
    public GameObject _pieceKing;
    public GameObject _pieceKnight;
    public GameObject _piecePawn;
    public GameObject _pieceQueen;
    public GameObject _pieceRook;

    public int[] _movesLeft = new int[6];
    public int _switchesLeft;

    Vector2 pos = new Vector2(0, 0);
    public Vector2 Pos { get { return pos; } }

    public GameObject _canvas;
    CanvasScript canvasScript;

    public GameObject _board;
    BoardGenerator boardScript;

    struct PlayerState {
        public Piece.Type pieceType;
        public int[] movesLeft;
        public int switchesLeft;
        public Vector2 pos;
        public List<BoardGenerator.Enemy> enemies;
        public PlayerState(Piece.Type pieceType, int[] movesLeft,
                int switchesLeft, Vector2 pos, List<BoardGenerator.Enemy> enemies) {
            this.pieceType = pieceType;
            this.movesLeft = movesLeft;
            this.switchesLeft = switchesLeft;
            this.pos = pos;
            this.enemies = enemies;
        }
    }
    readonly Stack<PlayerState> moveStack = new Stack<PlayerState>();
    void PushState() {
        moveStack.Push(new PlayerState(pieceType, (int[]) _movesLeft.Clone(),
            _switchesLeft, new Vector2(pos.x, pos.y),
            new List<BoardGenerator.Enemy>(BoardGenerator.Enemies)));
    }

    void Awake() {
        canvasScript = _canvas.GetComponent<CanvasScript>();
        boardScript = _board.GetComponent<BoardGenerator>();
        moveStack.Clear();
    }

    public void ChangePiece(Piece.Type piece) {
        PushState();

        if (_switchesLeft == 0) return;
        --_switchesLeft;
        canvasScript.RedrawNumbers();

        SetPiece(piece);
    }

    void SetPiece(Piece.Type piece) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        pieceType = piece;

        GameObject pieceObj;
        switch (piece) {
        case Piece.Type.Bishop:
            pieceObj = (GameObject) Instantiate(_pieceBishop,
                        transform.position, transform.rotation);
            break;
        case Piece.Type.King:
            pieceObj = (GameObject) Instantiate(_pieceKing,
                        transform.position, transform.rotation);
            break;
        case Piece.Type.Knight:
            pieceObj = (GameObject) Instantiate(_pieceKnight,
                        transform.position, transform.rotation);
            break;
        case Piece.Type.Pawn:
            pieceObj = (GameObject) Instantiate(_piecePawn,
                        transform.position, transform.rotation);
            break;
        case Piece.Type.Queen:
            pieceObj = (GameObject) Instantiate(_pieceQueen,
                        transform.position, transform.rotation);
            break;
        case Piece.Type.Rook:
            pieceObj = (GameObject) Instantiate(_pieceRook,
                        transform.position, transform.rotation);
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
        bool isCapture = BoardGenerator.Enemies.Exists(enemy =>
                enemy._pos == movePos);

        if (_movesLeft[(int)pieceType] != 0 &&
                CanMove(pieceType, pos, movePos, isCapture) &&
                BoardGenerator.Enemies.TrueForAll(enemy =>
                    !CanMove(enemy._pieceType, enemy._pos, movePos, true))) {
            // player is able to move to square
            PushState();
            Piece.Type? capturedPiece = ForceMove(movePos);
            if (capturedPiece.HasValue) {
                ++_movesLeft[(int)capturedPiece.Value];
            }
            --_movesLeft[(int)pieceType];
            canvasScript.RedrawNumbers();
            return true;
        }

        return false;
    }

    public Piece.Type? ForceMove(Vector2 movePos) {
        Piece.Type? capturedPiece = null;
        Vector3 oldPos = transform.position;
        oldPos.x = movePos.x;
        oldPos.z = movePos.y;
        transform.position = oldPos;
        pos = movePos;
        BoardGenerator.Enemies.RemoveAll(enemy => {
            if (enemy._pos == pos) {
                capturedPiece = enemy._pieceType;
                Destroy(enemy._gameObject);
                return true;
            }
            return false;
        });
        return capturedPiece;
    }

    public bool CanMove(Piece.Type movePieceType, Vector2 fromPos,
            Vector2 toPos, bool isCapture) {
        if (fromPos == toPos) return false;

        int dx = Mathf.RoundToInt(toPos.x - fromPos.x),
            dy = Mathf.RoundToInt(toPos.y - fromPos.y);
        int adx = Mathf.Abs(dx), ady = Mathf.Abs(dy);

        switch (movePieceType) {

        case Piece.Type.Knight:
            return adx + ady == 3 && (adx == 1 || adx == 2);

        case Piece.Type.Pawn:
            if (isCapture) return adx == 1 && dy == 1;
            return dx == 0 && (dy == 1 || dy == 2 &&
                   BoardGenerator.IsPassable(new Vector2(fromPos.x,
                                                         fromPos.y + 1)));

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

    public void UndoMove() {
        if (moveStack.Count > 1) {
            PlayerState ps = moveStack.Pop();
            SetPiece(ps.pieceType);
            _movesLeft = ps.movesLeft;
            _switchesLeft = ps.switchesLeft;
            pos = ps.pos;
            ForceMove(pos);
            boardScript.KillAllEnemies();
            BoardGenerator.Enemies.Clear();
            BoardGenerator.Enemies.AddRange(ps.enemies);
            boardScript.RedrawEnemies();
            canvasScript.RedrawNumbers();
        }
    }

}
