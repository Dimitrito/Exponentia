using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Handles the Guide UI panel, showing instructions and allowing the player to close it.
/// </summary>
public class Menu : MonoBehaviour
{
    public GameObject guide;
    void Awake()
    {
        var menuUi = GetComponent<UIDocument>();
        var _root = menuUi.rootVisualElement;

        _root.Q<Button>("playButton").clicked += () =>
        {
            ClickSfx.Instance.PlaySound();
            SceneManager.LoadScene("MainScene");
        };

        _root.Q<Button>("guideButton").clicked += () =>
        {
            ClickSfx.Instance.PlaySound();
            guide.SetActive(true);
        };

        _root.Q<Button>("exitButton").clicked += () =>
        {
            ClickSfx.Instance.PlaySound();
            Application.Quit();
        };
    }
}