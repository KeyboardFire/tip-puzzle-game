using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class BoardGenerator : MonoBehaviour {

    enum Tile {
        Empty, Square, EnemyPawn, EnemyKnight, EnemyBishop, EnemyRook,
        EnemyQueen, EnemyKing, Start, End, Unknown
    }
    public GameObject tileSquare;
    public GameObject tileStart;
    public GameObject tileEnd;

    public Material lightSquareMat;

    public GameObject player;
    PlayerScript playerScript;

    public GameObject canvas;
    CanvasScript canvasScript;

    public GameObject pieceBishop;
    public GameObject pieceKing;
    public GameObject pieceKnight;
    public GameObject piecePawn;
    public GameObject pieceQueen;
    public GameObject pieceRook;

    public Material enemyMat;

    readonly static List<Vector2> passable = new List<Vector2>();

    void Awake() {
        playerScript = player.GetComponent<PlayerScript>();
        canvasScript = canvas.GetComponent<CanvasScript>();
    }

    void Start() {
        LoadLevel();
    }

    void LoadLevel() {
        List<string> lines = GlobalData.currentLevel.text
            .Split("\n".ToCharArray()).ToList();

        playerScript.ChangePiece((Piece.Type)
                Piece.pieceChars.IndexOf(lines[0][0]));
        playerScript.movesLeft = new int[7];
        foreach (char ch in lines[0]) {
            ++playerScript.movesLeft[Piece.pieceChars.IndexOf(ch)];
        }
        canvasScript.RedrawNumbers();
        lines.RemoveAt(0);

        // remove existing tiles
        List<Transform> children = transform.Cast<Transform>().ToList();
        children.ForEach(child => Destroy(child.gameObject));

        // read the level file and turn it into Tile objects
        var tiles =
            from line in lines
            select (
                    from ch in line
                    select
                        ch == ' ' ? Tile.Empty :
                        ch == '#' ? Tile.Square :
                        ch == 'P' ? Tile.EnemyPawn :
                        ch == 'N' ? Tile.EnemyKnight :
                        ch == 'B' ? Tile.EnemyBishop :
                        ch == 'R' ? Tile.EnemyRook :
                        ch == 'Q' ? Tile.EnemyQueen :
                        ch == 'K' ? Tile.EnemyKing :
                        ch == 'S' ? Tile.Start :
                        ch == 'E' ? Tile.End :
                        Tile.Unknown // ???
                   );

        // loop through the grid of Tiles and place them in the world
        foreach (var x in tiles.Select((row, idx) => new { row, idx })) {
            foreach (var y in x.row.Select((tile, idx) => new { tile, idx })) {

                // the tile being placed
                GameObject tile;
                // the position of the tile
                var pos = new Vector3(x.idx, 0, y.idx);
                // this is really really bad and I'm sorry
                bool isEnemy = false;

                switch (y.tile) {
                case Tile.Square:
                case Tile.EnemyPawn:
                case Tile.EnemyKnight:
                case Tile.EnemyBishop:
                case Tile.EnemyRook:
                case Tile.EnemyQueen:
                case Tile.EnemyKing:
                    tile = (GameObject) Instantiate(tileSquare, pos, Quaternion.identity);
                    if ((x.idx + y.idx) % 2 == 0) {
                        tile.GetComponent<Renderer>().material = lightSquareMat;
                    }
                    passable.Add(new Vector2(x.idx, y.idx));
                    isEnemy = y.tile != Tile.Square;
                    break;
                case Tile.Start:
                    tile = (GameObject) Instantiate(tileStart, pos, Quaternion.identity);
                    playerScript.ForceMove(new Vector2(x.idx, y.idx));
                    passable.Add(new Vector2(x.idx, y.idx));
                    break;
                case Tile.End:
                    tile = (GameObject) Instantiate(tileEnd, pos, Quaternion.identity);
                    passable.Add(new Vector2(x.idx, y.idx));
                    break;
                default:
                    continue;
                }

                tile.GetComponent<TileBehavior>().SetPosition(x.idx, y.idx);
                tile.transform.parent = transform;

                if (isEnemy) {
                    GameObject pieceObj;
                    switch (y.tile) {
                    case Tile.EnemyBishop:
                        pieceObj = (GameObject) Instantiate(pieceBishop, transform.position, transform.rotation);
                        break;
                    case Tile.EnemyKing:
                        pieceObj = (GameObject) Instantiate(pieceKing, transform.position, transform.rotation);
                        break;
                    case Tile.EnemyKnight:
                        pieceObj = (GameObject) Instantiate(pieceKnight, transform.position, transform.rotation);
                        break;
                    case Tile.EnemyPawn:
                        pieceObj = (GameObject) Instantiate(piecePawn, transform.position, transform.rotation);
                        break;
                    case Tile.EnemyQueen:
                        pieceObj = (GameObject) Instantiate(pieceQueen, transform.position, transform.rotation);
                        break;
                    case Tile.EnemyRook:
                        pieceObj = (GameObject) Instantiate(pieceRook, transform.position, transform.rotation);
                        break;
                    default: return; // unreachable
                    }

                    pieceObj.transform.parent = transform;
                    pieceObj.transform.localScale = player.transform.localScale;
                    pieceObj.transform.position = tile.transform.position;
                    Bounds bounds = Util.ChildrenBounds(pieceObj.transform);
                    pieceObj.transform.Translate(0, bounds.extents.y - bounds.center.y, 0);

                    foreach (Transform child in pieceObj.transform) {
                        child.gameObject.GetComponent<Renderer>().material = enemyMat;
                    }
                }

            }
        }
    }

    public static bool IsPassable(Vector2 v) {
        return passable.Contains(v);
    }

}
