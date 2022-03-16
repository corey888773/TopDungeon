using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text
    public Text hitpointText, levelText, xpText, weaponUpgradeText, pesosText;
    
    //Logic
    public Image characterSelectionSprite, weaponSprite;
    public RectTransform xpBar;

    //Character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            GameManager.instance.player.currentCharacterSelection++;

            if (GameManager.instance.player.currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                GameManager.instance.player.currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            GameManager.instance.player.currentCharacterSelection--;

            if (GameManager.instance.player.currentCharacterSelection < 0)
            {
                GameManager.instance.player.currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[GameManager.instance.player.currentCharacterSelection];
        GameManager.instance.player.SwapSprite(GameManager.instance.player.currentCharacterSelection);
        // UpdateMenu();
    }

    //Weapon upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }
    
    //Character information upgrade
    public void UpdateMenu()
    {
        //CharacterSprite
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[GameManager.instance.player.currentCharacterSelection];
        
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weaponPrices.Count == GameManager.instance.weapon.weaponLevel)
        {
            weaponUpgradeText.text = "MAX";
        }
        else
        {
            weaponUpgradeText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel + 1].ToString();
        }
        
        //Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        
        //Xp bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpBar.localScale = Vector3.one;
            xpText.text = GameManager.instance.experience.ToString();
        }
        else
        {
            int previousExp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentExp = GameManager.instance.GetXpToLevel(currentLevel);
            int diff = currentExp - previousExp;
            int currentExpToLevel = GameManager.instance.experience - previousExp;
            float progressBar = (float) currentExpToLevel / (float) diff;
            xpText.text = currentExpToLevel.ToString() + " / " + diff.ToString();
            xpBar.localScale = new Vector3(progressBar, 1, 1);

        }
        
        
        // xpBar.localScale = new Vector3(0.5f, 0, 0);
        // xpText.text = GameManager.instance.experience.ToString() + "/" + GameManager.instance.GetXpToLevel(GameManager.instance.GetCurrentLevel()).ToString();




    }
    
    
}   
