using UnityEngine;
using System.Collections;

public class GameObjectRecycler : MonoBehaviour {
	// Recycler containers
	public GameObject cellsContainer, rpDContainer, rpMContainer, rpAContainer, edContainer, emContainer, eaContainer, adContainer, amContainer, atContainer, ballContainer, goalContainer, goalkeeperContainer, bonusContainer;

	// Prefabs
	public GameObject cellPrefab;
	public GameObject enemyDefenderPrefab;
	public GameObject enemyMidfieldPrefab;
	public GameObject enemyAttackerPrefab;
	public GameObject allyDefenderPrefab;
	public GameObject allyMidfieldPrefab;
	public GameObject allyAttackerPrefab;
	public GameObject ballPrefab;
	public GameObject goalPrefab;
	public GameObject goalkeeperPrefab;
	public GameObject bonusPrefab;

	// Max values
	public static int maxCellsNumber;
	public static int maxDefendersNumber;
	public static int maxMidfieldsNumber;
	public static int maxAttackersNumber;
	public static int maxEnemyDefendersNumber;
	public static int maxEnemyMidfieldsNumber;
	public static int maxEnemyAttackersNumber;
	public static int maxAllyDefendersNumber;
	public static int maxAllyMidfieldsNumber;
	public static int maxAllyAttackersNumber;
	public static int maxBonusNumber;
	public static Round biggestRound;

	// Use this for initialization
	void Start () {	
		biggestRound = LevelLoader.getLevelLoaderInstance ().getBiggestRound ();

		maxCellsNumber = biggestRound.width * biggestRound.length;
		maxDefendersNumber = biggestRound.defenders;
		maxMidfieldsNumber = biggestRound.midfields;
		maxAttackersNumber = biggestRound.attackers;

		maxEnemyDefendersNumber = biggestRound.enemyDefenders;
		maxEnemyMidfieldsNumber = biggestRound.enemyMidfields;
		maxEnemyAttackersNumber = biggestRound.enemyAttackers;

		maxAllyDefendersNumber = biggestRound.allyDefenders + 1;
		maxAllyMidfieldsNumber = biggestRound.allyMidfields;
		maxAllyAttackersNumber = biggestRound.allyAttackers;

		maxBonusNumber = biggestRound.bonus;

		maxAttackersNumber = biggestRound.attackers + biggestRound.allyAttackers;

		Debug.Log ("Max cells number : "+maxCellsNumber+" / "+maxDefendersNumber+" / "+maxMidfieldsNumber+" / "+maxAttackersNumber+" / "+maxEnemyDefendersNumber+" / "+maxEnemyMidfieldsNumber+" / "+maxEnemyAttackersNumber);
	
		// Add cells, ball, enemies, allies and goal to grid
		createGridGameObjects();

		// Add allies to right pannel
		createRightPannelGameObjects();
	}	

	private void createGridGameObjects(){
		// Cells
		createCells();

		// Characters alreay on field
		createFieldCharacters();

		// Goal, Goalkeeper, ball, bonus
		createFieldItems();
	}

	private void createRightPannelGameObjects(){
		// def
		for (int j = 0; j < maxDefendersNumber; j++) {
			GameObject go = Instantiate(allyDefenderPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = rpDContainer.transform;
			go.SetActive (false);
		}
		// mid
		for (int j = 0; j < maxMidfieldsNumber; j++) {
			GameObject go = Instantiate(allyMidfieldPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = rpMContainer.transform;
			go.SetActive (false);
		}
		// att
		for (int j = 0; j < maxAttackersNumber; j++) {
			GameObject go = Instantiate(allyAttackerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = rpAContainer.transform;
			go.SetActive (false);
		}
	}

	private void createCells(){
		for (int j = 0; j < maxCellsNumber; j++) {
			GameObject cellGO = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			cellGO.transform.parent = cellsContainer.transform;
			cellGO.SetActive (false);
		}
	}

	private void createFieldCharacters(){
		// Enemy def
		for (int j = 0; j < maxEnemyDefendersNumber; j++) {
			GameObject go = Instantiate(enemyDefenderPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = edContainer.transform;
			go.SetActive (false);
		}
		// Enemy mid
		for (int j = 0; j < maxEnemyMidfieldsNumber; j++) {
			GameObject go = Instantiate(enemyMidfieldPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = emContainer.transform;
			go.SetActive (false);
		}
		// Enemy att
		for (int j = 0; j < maxEnemyAttackersNumber; j++) {
			GameObject go = Instantiate(enemyAttackerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = eaContainer.transform;
			go.SetActive (false);
		}


		// Ally def
		for (int j = 0; j < maxAllyDefendersNumber; j++) {
			GameObject go = Instantiate(allyAttackerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = adContainer.transform;
			go.SetActive (false);
		}
		// Ally mid
		for (int j = 0; j < maxAllyMidfieldsNumber; j++) {
			GameObject go = Instantiate(allyMidfieldPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = amContainer.transform;
			go.SetActive (false);
		}
		// Ally att
		for (int j = 0; j < maxAllyAttackersNumber; j++) {
			GameObject go = Instantiate(allyAttackerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			go.transform.parent = atContainer.transform;
			go.SetActive (false);
		}
	}

	void createFieldItems(){
		// Goal
		GameObject goalGo = Instantiate(goalPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		goalGo.transform.parent = goalContainer.transform;
		goalGo.SetActive (false);

		// Goalkeeper
		GameObject goalkeeperGo = Instantiate(goalkeeperPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		goalkeeperGo.transform.parent = goalkeeperContainer.transform;
		goalkeeperGo.SetActive (false);

		// Ball
		GameObject ballGo = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		ballGo.transform.parent = ballContainer.transform;
		ballGo.SetActive (false);

		// Bonus
		for (int j = 0; j < maxBonusNumber; j++) {
			GameObject bonusGo = Instantiate(bonusPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			bonusGo.transform.parent = bonusContainer.transform;
			bonusGo.SetActive (false);
		}
	}


	// Update is called once per frame
	void Update () {
	}
}
