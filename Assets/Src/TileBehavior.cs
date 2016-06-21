using UnityEngine;

public class TileBehavior : MonoBehaviour {

    Vector2 pos;

    void OnMouseOver() {
        // TODO outline or shade the square
    }

    void OnMouseDown() {
        GameObject.Find("/Player").GetComponent<PlayerScript>().MoveTo(pos);
    }

    public void SetPosition(int x, int y) {
        pos.x = x;
        pos.y = y;
    }

}
