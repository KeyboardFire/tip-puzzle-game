using UnityEngine;

public class TileBehavior : MonoBehaviour {

    Vector2 pos;

    void OnMouseEnter() {
        transform.localScale = transform.localScale + new Vector3(0, 1, 0);
    }

    void OnMouseExit() {
        transform.localScale = transform.localScale - new Vector3(0, 1, 0);
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
