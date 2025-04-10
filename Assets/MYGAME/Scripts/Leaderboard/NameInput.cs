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
            Debug.LogWarning("��� ������ �� �������!");
            return;
        }

        scoreScript.AddPlayer(playerName, coins);
        //Debug.Log($"������� �����: {playerName} � {coins} ��������");

      
    }
}
