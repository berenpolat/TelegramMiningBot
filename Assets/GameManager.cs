using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // Add DOTween namespace
using TMPro;  // Add TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    public float minedBudget;
    public TextMeshProUGUI minedBudgetText;  // Use TextMeshProUGUI for UI text
    public Image plusOneSign;
    public Button coinButton;
    public Canvas uiCanvas;  // Add a reference to the UI Canvas

    private const string MinedBudgetKey = "MinedBudget";  // Key for PlayerPrefs

    private void Start()
    {
        LoadMinedBudget();
        UpdateMinedBudgetText();  // Ensure text is updated on start
    }

    public void ClickOnCoin()
    {
        minedBudget += 1f;
        SaveMinedBudget();
        
        Image newPlusOneSign = Instantiate(plusOneSign, coinButton.transform.position, Quaternion.identity);
        newPlusOneSign.transform.SetParent(uiCanvas.transform, false); // Set the parent to the UI Canvas
        newPlusOneSign.transform.position = coinButton.transform.position; // Set position to coin button's position

        // Move the image 2 units up using DOTween and destroy it when done
        newPlusOneSign.transform.DOMoveY(newPlusOneSign.transform.position.y + 250f, .5f).OnComplete(() => 
        {
            Destroy(newPlusOneSign.gameObject);
        });

        UpdateMinedBudgetText();  // Update text after incrementing budget
    }

    private void Update()
    {
        // No need to update the text every frame; update it only when the budget changes
    }

    private void SaveMinedBudget()
    {
        PlayerPrefs.SetFloat(MinedBudgetKey, minedBudget);
        PlayerPrefs.Save();
    }

    private void LoadMinedBudget()
    {
        if (PlayerPrefs.HasKey(MinedBudgetKey))
        {
            minedBudget = PlayerPrefs.GetFloat(MinedBudgetKey);
        }
        else
        {
            minedBudget = 0f;
        }
    }

    private void UpdateMinedBudgetText()
    {
        minedBudgetText.text = $"Mined Budget: {minedBudget}";
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
