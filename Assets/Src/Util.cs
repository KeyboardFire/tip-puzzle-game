using UnityEngine;

public class Util {

    public static Bounds ChildrenBounds(Transform obj) {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        foreach (Transform child in obj) {
            bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
        }
        return bounds;
    }

}
