using UnityEngine;

public class TileBehavior : MonoBehaviour {

    Vector2 pos;

    void OnMouseOver() {
        // TODO outline or shade the square
    }

    void OnMouseDown() {
        if (GameObject.Find("/Player").GetComponent<PlayerScript>().MoveTo(pos)) {
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
