using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveInformation : MonoBehaviour {

	public Sprite[] minions;

	private GameObject nextMinion;

	public List<Game.Spawners.WaveObject> waveObjList = new List<Game.Spawners.WaveObject>();

	private int lastNumber;

	Image currentImage;
	Text armorTypeText;
	Text attackTypeText;
	Text healthText;
	Text recommendedValueText;
	Text minionCountText;
	Text minionNameText;

	void Start () {

		nextMinion = waveObjList [Game.Spawners.MinionSpawnerBehaviour.waveNumber].minion;
		SetText ();
		SetSprite ();
	}

	void Update(){
		if (Game.Spawners.MinionSpawnerBehaviour.waveNumber != lastNumber) {
			nextMinion = waveObjList [Game.Spawners.MinionSpawnerBehaviour.waveNumber].minion;
			lastNumber = Game.Spawners.MinionSpawnerBehaviour.waveNumber;
			SetSprite ();
			SetText ();
		}
	}

	public void SetSprite(){
		currentImage = GameObject.Find ("MinionSprite").GetComponent<Image> ();	
		currentImage.sprite = minions [lastNumber];
	}

	public void SetText(){
		armorTypeText = GameObject.Find("ArmorType").GetComponent<Text>();
		armorTypeText.text = System.Enum.GetName (typeof(Game.Units.ArmorType), nextMinion.GetComponent<Game.Units.UnitBehaviour>().ArmorType);
	
		attackTypeText = GameObject.Find("AttackType").GetComponent<Text>();
		attackTypeText.text = System.Enum.GetName (typeof(Game.Units.AttackType), nextMinion.GetComponent<Game.Units.UnitBehaviour>().AttackType);
	
		healthText = GameObject.Find ("HealthValue").GetComponent<Text> ();
		healthText.text = "" + (int)nextMinion.GetComponent<Game.Units.UnitBehaviour> ().Hp;

		minionCountText = GameObject.Find ("CountValue").GetComponent<Text> ();
		minionCountText.text = "" + waveObjList [Game.Spawners.MinionSpawnerBehaviour.waveNumber].amountOfMinions;
	
		minionNameText = GameObject.Find ("MinionName").GetComponent<Text> ();
		minionNameText.text = nextMinion.name;

	}
}
