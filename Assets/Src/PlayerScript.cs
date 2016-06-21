using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public GameObject pieceRook;

    // called on initialization
    void Start() {
        ChangePiece(pieceRook);
    }

    void ChangePiece(GameObject piece) {
        foreach (Transform child in transform) {
            Destroy(child);
        }

        GameObject pieceObj = (GameObject) Instantiate(piece);
        pieceObj.transform.parent = transform;
        pieceObj.transform.localScale = transform.localScale;
        pieceObj.transform.Translate(0, Util.ChildrenBounds(pieceObj.transform).extents.y, 0);
    }

}
