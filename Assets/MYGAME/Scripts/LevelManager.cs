using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int fishCount;
    public float currentBonus = 1.0f;
    public float bonusTime = 5.0f;
    public bool bonusActive;
    public GameObject looseCanvas;
    public Text looseFishCountText;

    public AudioSource audio;
    public AudioClip bonusSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void AddFish()
    {
        fishCount += 1 * (int)currentBonus;
    }

    public void EndGame()
    {
        looseCanvas.SetActive(true);
        looseFishCountText.text = fishCount.ToString();
    }
    
    public void ActivateBonus()
    {
        audio.PlayOneShot(bonusSound);
        StartCoroutine(StartBonus());
    }

    private IEnumerator StartBonus()
    {
        currentBonus = 2.0f;
        bonusActive = true;
        yield return new WaitForSeconds(bonusTime);
        currentBonus = 1.0f;
        bonusActive = false;
    }
    
}
