using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Manages the player's score and updates the UI.
/// </summary>
public class Score : MonoBehaviour
{
    public static int score;
    private VisualElement _root;

    void Awake ()
    {
        var scoreUi = GetComponent<UIDocument>();
        _root = scoreUi.rootVisualElement;

        score = 0;
        _root.Q<Label>("scoreLabel").text = score.ToString();
    }

    /// <summary>
    /// Adds the given value to the current score and updates the UI.
    /// </summary>
    /// <param name="value">Points to add to the score</param>
    public void AddScore(int score) 
    {
        Score.score += score;
        _root.Q<Label>("scoreLabel").text = Score.score.ToString();
    }
}