using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text = message;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        //text in Unity have different positioning system to the objects so we need
        //to adjust it's position to the world size
        floatingText.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;
        
        floatingText.Show();

    }
    
    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find( text => !text.active);
        // Synonymous for this line ^^^^
        //
        // foreach (var text in floatingTexts)
        //     if (!text.active)
        //         return text;
        //
        // for (int i = 0; i < floatingTexts.Count; i++)
        // {
        //     if (!floatingTexts[i].active)
        //         return floatingTexts[i];
        // }

        if (txt == null)
        {
            txt = new FloatingText();
            
            //creates an instance of text prefab 
            txt.gameObject = Instantiate(textPrefab);
            txt.gameObject.transform.SetParent(textContainer.transform);
            txt.txt = txt.gameObject.GetComponent<Text>();
            
            // add to a list
            floatingTexts.Add(txt);
        }
        return txt;
        
    }

    private void Update()
    {
        foreach (var floatingText in floatingTexts)
        {
            floatingText.UpdateFloatingText();     
        }
    }
}
