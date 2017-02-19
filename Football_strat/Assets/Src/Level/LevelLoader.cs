using UnityEngine;
using System.Collections;
using System.IO;

public class LevelLoader {
	private static LevelLoader instance;
	private Round currentRound;
	private string LEVEL_PATH = Application.dataPath+"/Resources/";

	private LevelLoader(){}

	public static LevelLoader getLevelLoaderInstance(){
		if (instance == null) 
			instance = new LevelLoader();
		return instance;
	}
		
	public Round loadField(string matchIndex, string roundIndex){
		this.currentRound = new Round ();

		if (!getRoundFile (matchIndex, roundIndex))
			return null;

		return this.currentRound;
	}

	private bool getRoundFile(string matchIndex, string roundIndex){
		string currentRoundInfosPath = "GameResources/match_" + matchIndex + "/round_" + roundIndex+"_info";
		string currentRoundFieldPath = "GameResources/match_" + matchIndex + "/round_" + roundIndex+"_field";

		if (!getRoundFileInfos (currentRoundInfosPath))
			return false;
		
		if (!getRoundFileField (currentRoundFieldPath))
			return false;

		return true;
	}

	private bool getRoundFileInfos(string currentRoundPath){
		string fieldFilePath = LEVEL_PATH + currentRoundPath + ".json"; 
		if (!File.Exists (fieldFilePath)) {
			Debug.LogError ("File "+fieldFilePath+" doesn't exist");
			return false;
		}
		TextAsset fieldFile = Resources.Load<TextAsset>(currentRoundPath) as TextAsset;
		this.currentRound = JsonUtility.FromJson<Round>(fieldFile.text);

		// Set current round field dimensions
		this.currentRound.setFieldDimensions ();

		return true;
	}

	private bool getRoundFileField(string currentRoundPath){
		string fieldFilePath = LEVEL_PATH + currentRoundPath + ".json"; 
		if (!File.Exists (fieldFilePath)) {
			Debug.LogError ("File "+fieldFilePath+" doesn't exist");
			return false;
		}

		TextAsset fieldFile = Resources.Load<TextAsset>(currentRoundPath) as TextAsset;
		FieldTable[] line = JsonHelper.FromJson<FieldTable>(fieldFile.text);

		// Add field to current round
		for (int i = 0; i < this.currentRound.width; i++) {
			for (int j = 0; j < this.currentRound.length; j++) {
				switch (line [j].content [i]) {
				case 0:
					this.currentRound.field [i, j] = CellEnum.EMPTY_CELL; 
					break;
				case 1: 
					this.currentRound.field [i, j] = CellEnum.BALL_START_CELL; 
					break;
				case 2: 
					this.currentRound.field [i, j] = CellEnum.BONUS_CELL; 
					break;
				case 3: 
					this.currentRound.field [i, j] = CellEnum.GOAL_LEFT_CELL; 
					break;
				case 4: 
					this.currentRound.field [i, j] = CellEnum.GOAL_RIGHT_CELL; 
					break;
				case 5: 
					this.currentRound.field [i, j] = CellEnum.GOAL_TOP_CELL; 
					break;
				case 6: 
					this.currentRound.field [i, j] = CellEnum.GOAL_BOTTOM_CELL; 
					break;
				case 10: 
					this.currentRound.field [i, j] = CellEnum.ENEMY_DEFENDER_CELL; 
					break;
				case 11: 
					this.currentRound.field [i, j] = CellEnum.ENEMY_GOALKEEPER_CELL; 
					break;
				}
			}
		}

		return true;
	}
}
