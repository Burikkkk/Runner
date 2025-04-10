using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text scoreText;
    public Image bonusImage;

    private void Update()
    {
        scoreText.text = LevelManager.instance.fishCount.ToString();
        bonusImage.enabled = LevelManager.instance.bonusActive;
    }
}
