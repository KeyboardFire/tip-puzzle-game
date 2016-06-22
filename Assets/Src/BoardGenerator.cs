using UnityEngine;
using System.IO;
using System.Linq;

public class BoardGenerator : MonoBehaviour {

    enum Tile {
        Empty, Square, Start, End, Unknown
    }
    public GameObject tileSquare;
    public GameObject tileStart;
    public GameObject tileEnd;

    public Material lightSquareMat;

    public GameObject player;

    // called on initialization
    void Start() {

        // read the level file and turn it into Tile objects
        var tiles =
            from line in File.ReadAllLines("Assets/Levels/level0.txt")
            select (
                    from ch in line
                    select
                        ch == ' ' ? Tile.Empty :
                        ch == '#' ? Tile.Square :
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
                Vector3 pos = new Vector3(x.idx, 0, y.idx);

                switch (y.tile) {
                case Tile.Square:
                    tile = (GameObject) Instantiate(tileSquare, pos, Quaternion.identity);
                    if ((x.idx + y.idx) % 2 == 0) {
                        tile.GetComponent<Renderer>().material = lightSquareMat;
                    }
                    break;
                case Tile.Start:
                    tile = (GameObject) Instantiate(tileStart, pos, Quaternion.identity);
                    player.GetComponent<PlayerScript>().ForceMove(new Vector2(x.idx, y.idx));
                    break;
                case Tile.End:
                    tile = (GameObject) Instantiate(tileEnd, pos, Quaternion.identity);
                    break;
                default:
                    continue;
                }

                tile.GetComponent<TileBehavior>().SetPosition(x.idx, y.idx);

            }
        }
    }

}
