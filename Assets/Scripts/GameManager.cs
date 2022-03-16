using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	private void Awake(){
		if(GameManager.instance != null)
		{
			Destroy(gameObject);
			Destroy(player.gameObject);
			Destroy(floatingTextManager.gameObject);
			Destroy(menu);
			Destroy(hud);
			Destroy(eventSystem);
			return;
		}

		instance = this;
		SceneManager.sceneLoaded += LoadState;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	//resources
	public List<Sprite> playerSprites;
	public List<Sprite> weaponSprites;
	public List<int> weaponPrices;
	public List<int> xpTable;

	// references
	public Player player;
	public FloatingTextManager floatingTextManager;
	public Weapon weapon;
	public RectTransform hitPointBar;
	public GameObject menu;
	public GameObject hud;
	public GameObject eventSystem;

	// nawiązania do skryptów itd

	// logic
	public int pesos;
	public int experience;

	private void Update()
	{
		// Debug.Log(GetCurrentLevel());
	}

	//floating text
	public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
	{
		floatingTextManager.Show(message, fontSize, color, position, motion, duration);
	}
	
	//Upgrade Weapon
	public bool TryUpgradeWeapon()
	{
		if (weaponPrices.Count <= weapon.weaponLevel)
		{
			return false;
		}

		if (pesos >= weaponPrices[weapon.weaponLevel])
		{
			pesos -= weaponPrices[weapon.weaponLevel];
			weapon.UpgradeWeapon();
			return true;
		}

		return false;
	}
	
	//Experience System
	public int GetCurrentLevel()
	{
		int level = 0;
		int expGap = 0;

		while (experience >= expGap)
		{
			expGap += xpTable[level];
			level++;

			if (level == xpTable.Count)
			{
				return level;
			}
		}
		return level;
	}
	public int GetXpToLevel(int level)
	{
		int r = 0;
		int expGap = 0;

		while (r < level)
		{
			expGap += xpTable[r];
			r++;
		}

		return expGap;
	}
	public void GrantExp(int exp)
	{
		int currentLevel = GetCurrentLevel();
		experience += exp;
		if (currentLevel < GetCurrentLevel())
		{
			for (int i = 0; i < GetCurrentLevel() - currentLevel; i++)
			{
				player.OnLevelUp(false);
				Debug.Log("lvlup");
			}
		}
	}
	
	//Health Bar
	public void OnHitpointChange()
	{
		float ratio = (float) player.hitpoint / (float) player.maxHitpoint;
		hitPointBar.localScale = new Vector3(ratio, 1.15f, 1);
	}
	
	
	// save state	
	public void OnSceneLoaded(Scene s, LoadSceneMode mode)
	{
		player.transform.position = GameObject.Find("SpawnPoint").transform.position;
     	OnHitpointChange();
	}
	
	public void SaveState(){
		string s ="";
		s += pesos.ToString() + "-";
		s += experience.ToString() + "-";
		s += weapon.weaponLevel.ToString() + "-";
		s += player.currentCharacterSelection.ToString() + "-";

		PlayerPrefs.SetString("SaveState", s);
		Debug.Log("SaveState");

	}
	public void LoadState(Scene s, LoadSceneMode mode)
	{
		SceneManager.sceneLoaded -= LoadState;
		
		//if player has not the key the code wont go further
		if (!PlayerPrefs.HasKey("SaveState"))
			return;
		
		string[] data = PlayerPrefs.GetString("SaveState").Split('-');
		
		pesos = int.Parse(data[0]);
		experience = int.Parse(data[1]);
		weapon.SetWeaponLevel(int.Parse(data[2]));
		player.SwapSprite(int.Parse(data[3]));
		player.SetLevel(GetCurrentLevel());
		
		
		Debug.Log("LoadState"); 
	}
}
