using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Handles the Game Over screen, showing final score and menu button.
/// </summary>
public class GameOverHandler : MonoBehaviour
{
    void OnEnable()
    {
        var gameOverUi = GetComponent<UIDocument>();
        var _root = gameOverUi.rootVisualElement;

        _root.Q<Label>("description").text = "The cube touched the red line, your score is " + Score.score.ToString();
        
        _root.Q<Button>("menuButton").clicked += () =>
        {
            ClickSfx.Instance.PlaySound();
            SceneManager.LoadScene("Menu");
        };
    }
}