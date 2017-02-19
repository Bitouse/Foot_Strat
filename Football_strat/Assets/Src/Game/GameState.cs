using System;

public enum GameState
{
	FIRST_GAME_ENTRANCE,
	MAIN_MENU,
	SUB_MENU,
	IG_PLAYER_PLACING_ITEMS,
	IG_TRY_LEVEL,
	LEVEL_RESULTS
}

public static class GameStateManager{
	public static GameState gameState;
}


