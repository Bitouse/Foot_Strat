using UnityEngine;
using System.Collections;

public static class LevelManager {
	public static Round currentRound;
	public static LevelBuilderState builderState;

	public static void getRound(int matchIndex, int roundIndex){
		if (matchIndex < 0 || roundIndex < 0) {
			Debug.LogError ("Match index and round index must be positive integer\n");
			return;
		}

		// First load level from resources
		currentRound = LevelLoader.getLevelLoaderInstance ().loadField (matchIndex.ToString(), roundIndex.ToString());

		// Then display it
		builderState = LevelBuilderState.DISPLAY_GRID;
	}
}
