using UnityEngine;

public class Util {

    public static Bounds ChildrenBounds(Transform obj) {
        Bounds? bounds = null;
        foreach (Transform child in obj) {
            if (bounds.HasValue) {
                bounds.Value.Encapsulate(child.GetComponent<Renderer>().bounds);
            } else {
                bounds = child.GetComponent<Renderer>().bounds;
            }
        }
        return bounds.Value;
    }

}
