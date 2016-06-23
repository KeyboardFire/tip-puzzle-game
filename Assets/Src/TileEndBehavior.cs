using UnityEngine;

public class TileEndBehavior : TileBehavior {

    protected override void OnMoveSuccess() {
        Debug.Log("WIN");
    }

}
