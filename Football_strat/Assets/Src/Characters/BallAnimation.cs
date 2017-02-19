using UnityEngine;
using System.Collections;

public class BallAnimation : MonoBehaviour {
	public float speed = 10f;
	public Orientations initialOrientation;
	public Orientations currentOrientation;

	// Use this for initialization
	void Start () {
		initialOrientation = Orientations.RIGHT;
		currentOrientation = Orientations.RIGHT;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStateManager.gameState == GameState.IG_TRY_LEVEL) {
			Debug.Log ("Ball should go");
		}
	}
}
