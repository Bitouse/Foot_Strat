using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool createLevel = false;
	public int matchNumber = 0;
	public int roundNumber = 0;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (createLevel) {
			LevelManager.getRound (matchNumber,roundNumber);
			createLevel = false;
		}
	}
}
