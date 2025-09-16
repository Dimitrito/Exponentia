using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Handles the Guide screen, showing game instructions and a back button.
/// </summary>
public class Guide : MonoBehaviour
{
    string description = "Welcome to Exponentia!\r\nYour goal is to score as many points as possible before the arena fills up and the cube crosses the red line.\r\nYou can move the cube left and right before pushing it forward.";

    void OnEnable()
    {
        var guideUi = GetComponent<UIDocument>();
        var _root = guideUi.rootVisualElement;

        _root.Q<Label>("description").text = description;

        _root.Q<Button>("backButton").clicked += () =>
        {
            ClickSfx.Instance.PlaySound();
            gameObject.SetActive(false);
        };
    }
}