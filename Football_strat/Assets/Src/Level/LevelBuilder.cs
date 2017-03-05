using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class LevelBuilder:MonoBehaviour {
	// Field informations
	private float x_max = 1505f, x_min = -1f, y_max = 858f, y_min = 10f;
	private float cell_size, x_offset = 0f, y_offset = 0f;

	// TODO : do not use instantiate to create field components
	public GameObject cellsContainer, edContainer, emContainer, eaContainer, adContainer, amContainer, atContainer, ballContainer, goalContainer, goalkeeperContainer, bonusContainer;

	private GameObject cellPrefab, ballPrefab, enemyDefenderPrefab, enemyGoalKeeperPrefab, goalPrefab, allyPrefab;
	public GameObject grassField;
	public GameObject defender, midfield, attacker;

	// Texts fields
	public Text commentaries, roundInfos, team1, team2, score1, score2, defendersNumber, midfieldsNumber, attackersNumber;

	void Start () {
		// Load prefab 
		// TODO : Use a gameObject table to recycle gameobjects
		/*ballPrefab = Resources.Load ("prefabs/Ball") as GameObject;
		enemyDefenderPrefab = Resources.Load ("prefabs/Enemy_Player") as GameObject;
		enemyGoalKeeperPrefab = Resources.Load ("prefabs/Enemy_Goalkeeper") as GameObject;
		goalPrefab = Resources.Load ("prefabs/Goal") as GameObject;
		allyPrefab = Resources.Load ("prefabs/Ally_Defender") as GameObject;
		cellPrefab = Resources.Load ("prefabs/Cell") as GameObject;*/
	}

	void Update(){
		switch (LevelManager.builderState) {
		case global::LevelBuilderState.DISPLAY_GRID:
			// Display grid
			displayGrid();
			LevelManager.builderState = global::LevelBuilderState.DISPLAY_BACKGROUND;
			break;
		case global::LevelBuilderState.DISPLAY_BACKGROUND:
			// Display Background
			displayFieldBackground ();
			LevelManager.builderState = global::LevelBuilderState.DISPLAY_COMPONENTS;
			break;
		case global::LevelBuilderState.DISPLAY_COMPONENTS:
			// Display grid components
			displayGridComponents();
			LevelManager.builderState = global::LevelBuilderState.DISPLAY_TEXT;
			break;
		case global::LevelBuilderState.DISPLAY_TEXT:
			// Display text
			displayText();
			LevelManager.builderState = global::LevelBuilderState.LEVEL_IS_DISPLAYED;
			break;
		}
	}

	private void displayText(){
		commentaries.text = LevelManager.currentRound.commentary;
		roundInfos.text = "Round "+LevelManager.currentRound.round_id;
		team1.text = LevelManager.currentRound.team_0;
		team2.text = LevelManager.currentRound.team_1;
		score1.text = "1   -";
		score2.text = "0";
		defendersNumber.text = "" + LevelManager.currentRound.defenders;
		midfieldsNumber.text = "" + LevelManager.currentRound.midfields;
		attackersNumber.text = "" + LevelManager.currentRound.attackers;
	}

	private void displayGridComponents(){
		// Display field components
		for (int i = 0; i < LevelManager.currentRound.width; i++) {
			for (int j = 0; j < LevelManager.currentRound.length; j++) {
				Vector3 pos = new Vector3(i, LevelManager.currentRound.length - j, 0f) * cell_size;
				pos.x += x_offset;
				pos.y += y_offset;

				switch (LevelManager.currentRound.field [i, j]) {
				case CellEnum.BALL_START_CELL:
					GameObject ballGO = Instantiate (ballPrefab, pos - new Vector3 (0f, 0.35f, 0f) * cell_size, Quaternion.identity) as GameObject;
					GameObject allyGO = Instantiate (allyPrefab, pos, Quaternion.identity) as GameObject;

					ballGO.transform.localScale = new Vector3 (ballGO.transform.localScale.x, ballGO.transform.localScale.y, ballGO.transform.localScale.z) * cell_size; 
					allyGO.transform.localScale = new Vector3 (allyGO.transform.localScale.x, allyGO.transform.localScale.y, allyGO.transform.localScale.z) * cell_size;

					ballGO.transform.parent = transform.Find(i+"_"+j).transform;
					allyGO.transform.parent = transform.Find(i+"_"+j).transform;

					break;
				case CellEnum.GOAL_LEFT_CELL:
					GameObject goalLGO = Instantiate(goalPrefab, pos, Quaternion.Euler(0, 0, 180)) as GameObject;
					goalLGO.transform.localScale = new Vector3(goalLGO.transform.localScale.x, goalLGO.transform.localScale.y, goalLGO.transform.localScale.z) * cell_size; 
					goalLGO.transform.parent = transform.Find(i+"_"+j).transform;
					break;
				case CellEnum.GOAL_RIGHT_CELL:
					GameObject goalRGO = Instantiate(goalPrefab, pos, Quaternion.identity) as GameObject;
					goalRGO.transform.localScale = new Vector3(goalRGO.transform.localScale.x, goalRGO.transform.localScale.y, goalRGO.transform.localScale.z) * cell_size; 
					goalRGO.transform.parent = transform.Find(i+"_"+j).transform;
					break;
				case CellEnum.GOAL_TOP_CELL:
					GameObject goalTGO = Instantiate(goalPrefab, pos, Quaternion.Euler(0, 0, 90)) as GameObject;
					goalTGO.transform.localScale = new Vector3(goalTGO.transform.localScale.x, goalTGO.transform.localScale.y, goalTGO.transform.localScale.z) * cell_size; 
					goalTGO.transform.parent = transform.Find(i+"_"+j).transform;
					break;
				case CellEnum.GOAL_BOTTOM_CELL:
					GameObject goalBGO = Instantiate(goalPrefab, pos, Quaternion.Euler(0, 0, 270)) as GameObject;
					goalBGO.transform.localScale = new Vector3(goalBGO.transform.localScale.x, goalBGO.transform.localScale.y, goalBGO.transform.localScale.z) * cell_size; 
					goalBGO.transform.parent = transform.Find(i+"_"+j).transform;
					break;
				case CellEnum.BONUS_CELL:

					break;
				case CellEnum.ENEMY_DEFENDER_CELL:
					GameObject enDeGO = Instantiate(enemyDefenderPrefab, pos, Quaternion.identity) as GameObject;
					enDeGO.transform.localScale = new Vector3(enDeGO.transform.localScale.x, enDeGO.transform.localScale.y, enDeGO.transform.localScale.z) * cell_size; 
					enDeGO.transform.parent = transform.Find(i+"_"+j).transform;
					break;
				case CellEnum.ENEMY_GOALKEEPER_CELL:
					GameObject enGoGO = Instantiate(enemyGoalKeeperPrefab, pos, Quaternion.identity) as GameObject;
					enGoGO.transform.localScale = new Vector3(enGoGO.transform.localScale.x, enGoGO.transform.localScale.y, enGoGO.transform.localScale.z) * cell_size;
					enGoGO.transform.parent = transform.Find(i+"_"+j).transform; 
					break;
				}
			}
		}
	}
		
	private void displayFieldBackground(){
		string t = LevelManager.currentRound.background;
		Debug.Log (t);
		Sprite backgroundImg = Resources.Load <Sprite>(LevelManager.currentRound.background);
		if (backgroundImg == null) {
			Debug.LogError ("Background cannot be found\n");
			return;
		}

		grassField.GetComponent<SpriteRenderer> ().sprite = backgroundImg;
	}

	private void displayGrid(){
		// Space between grid lines
		setCellHighAndWidth ();

		for (int i = 0; i < LevelManager.currentRound.width; i++) {
			for (int j = 0; j < LevelManager.currentRound.length; j++) {
				Vector3 pos = new Vector3(i + 0.5f, LevelManager.currentRound.length - j - 0.5f, -0.01f) * cell_size;
				pos.x += x_offset;
				pos.y += y_offset;
				GameObject cellGO = cellsContainer.transform.GetChild(0).gameObject;



				cellGO.transform.localScale = new Vector3(cellGO.transform.localScale.x, cellGO.transform.localScale.y, cellGO.transform.localScale.z) * cell_size / 100f; 
				cellGO.name = i + "_" + j;
				cellGO.transform.parent = transform;
			}
		}
	}

	private void setCellHighAndWidth(){
		float x_length = x_max - x_min;
		float y_length = y_max - y_min;

		float x_cell_size, y_cell_size;

		x_cell_size = x_length / LevelManager.currentRound.width;
		y_cell_size = y_length / LevelManager.currentRound.length;

		// Get cell size
		if(y_cell_size > x_cell_size) {
			cell_size = x_cell_size;
		} else {
			cell_size = y_cell_size;
		}
			
		// Get offset
		float gridRealSize;
		if (LevelManager.currentRound.width < LevelManager.currentRound.length) {
			gridRealSize = LevelManager.currentRound.width * cell_size;
			x_offset = (x_length - gridRealSize) / 2f;
			//y_offset = y_min * cell_size;
		} else {
			gridRealSize = LevelManager.currentRound.length * cell_size;
			y_offset = (y_length - gridRealSize) / 2f;
			//x_offset = x_min * cell_size;
		}
	}
}
