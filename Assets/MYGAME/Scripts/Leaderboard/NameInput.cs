using UnityEngine;
using UnityEngine.UI; 
using TMPro;
public class NameInfo : MonoBehaviour
{
    public TMP_InputField nameInput;
    public Score scoreScript;



    public void OnSaveButtonClicked()
    {
        string playerName = nameInput.text;
        int coins=LevelManager.instance.fishCount;

        if (string.IsNullOrWhiteSpace(playerName))
        {
            Debug.LogWarning("Имя игрока не введено!");
            return;
        }

        scoreScript.AddPlayer(playerName, coins);
        //Debug.Log($"Сохранён игрок: {playerName} с {coins} монетами");

      
    }
}
