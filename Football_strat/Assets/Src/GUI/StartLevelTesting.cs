using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartLevelTesting : MonoBehaviour {

	void OnMouseDown ()
	{
		Debug.Log ("Player start this level");
		GameStateManager.gameState = GameState.IG_TRY_LEVEL;
	}
}
