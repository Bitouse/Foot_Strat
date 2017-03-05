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


	public Round getBiggestRound(){
		int maxFieldSize = 0, maxWidth = 0, maxLength = 0, maxDefenders = 0, maxMidfields = 0, maxAttackers = 0, maxBonus = 0, maxEDefenders = 0, maxEMidfields = 0, maxEAttackers = 0, maxADefenders = 0, maxAMidielfd = 0, maxAAttackers = 0;
		int matchId = 0, roundId = 0;

		string matchPath = "GameResources/match_" + matchId+"/";

		while(Directory.Exists(LEVEL_PATH+matchPath)){
			while (File.Exists (LEVEL_PATH+matchPath+ "round_" + roundId +"_info.json")) {
				TextAsset fieldFile = Resources.Load<TextAsset>(matchPath+ "round_" + roundId +"_info") as TextAsset;
				Round round = JsonUtility.FromJson<Round>(fieldFile.text);

				int roundSize = round.width * round.length;
				if(roundSize > maxFieldSize){
					maxFieldSize = roundSize;
					maxWidth = round.width;
					maxLength = round.length;
				}
				if(round.defenders > maxDefenders){
					maxDefenders = round.defenders;
				}
				if(round.midfields > maxMidfields){
					maxMidfields = round.midfields;
				}
				if(round.attackers > maxAttackers){
					maxAttackers = round.attackers;
				}
				if(round.bonus > maxBonus){
					maxBonus = round.bonus;
				}
				if(round.enemyDefenders > maxEDefenders){
					maxEDefenders = round.enemyDefenders;
				}
				if(round.enemyMidfields > maxEMidfields){
					maxEMidfields = round.enemyMidfields;
				}
				if(round.enemyAttackers > maxEAttackers){
					maxEAttackers = round.enemyAttackers;
				}
				if(round.allyDefenders > maxADefenders){
					maxADefenders = round.allyDefenders;
				}
				if(round.allyMidfields > maxAMidielfd){
					maxAMidielfd = round.allyMidfields;
				}
				if(round.allyAttackers > maxAttackers){
					maxAttackers = round.allyAttackers;
				}

				roundId++;
			}			
			matchId++;
			matchPath = "GameResources/match_" + matchId + "/";
			roundId = 0;
		}
			
		Round biggestRound = new Round(maxWidth, maxLength, maxDefenders, maxMidfields, maxAttackers, maxBonus, maxEDefenders, maxEMidfields, maxEAttackers, maxADefenders, maxAMidielfd, maxAAttackers);

		return biggestRound;
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
