using UnityEngine;

public class TileBehavior : MonoBehaviour {

    GameObject player;
    PlayerScript playerScript;

    Vector2 pos;
    Vector3 oldScale;

    void Start() {
        player = GameObject.Find("/Player");
        playerScript = player.GetComponent<PlayerScript>();
        oldScale = transform.localScale;
    }

    void OnMouseEnter() {
        bool isCapture = BoardGenerator.Enemies.Exists(enemy =>
                enemy._pos == pos);
        bool isNotAttacked = BoardGenerator.Enemies.TrueForAll(enemy =>
                !playerScript.CanMove(enemy._pieceType, enemy._pos, pos, true));
        if (playerScript._movesLeft[(int)playerScript.PieceType] != 0 &&
                playerScript.CanMove(playerScript.PieceType, playerScript.Pos,
                    pos, isCapture) && isNotAttacked) {
            transform.localScale = transform.localScale + new Vector3(0, 0.5f, 0);
        }
    }

    void OnMouseExit() {
        transform.localScale = oldScale;
    }

    void OnMouseDown() {
        if (playerScript.GetComponent<PlayerScript>().MoveTo(pos)) {
            transform.localScale = oldScale;
            OnMoveSuccess();
        }
    }

    protected virtual void OnMoveSuccess() {
        // nop
    }

    public void SetPosition(int x, int y) {
        pos.x = x;
        pos.y = y;
    }

}
