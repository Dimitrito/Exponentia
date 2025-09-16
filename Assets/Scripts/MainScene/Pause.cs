using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public InputActionAsset actions;
    public InputActionReference activatePause;
    public InputActionReference deactivatePause;

    void Awake()
    {
        activatePause.action.Enable();
        activatePause.action.performed += ctx => PauseEnable();
    }

    //Enables the pause state: stops time, shows UI, switches input actions.
    void PauseEnable() 
    {
        actions.FindActionMap("Player").Disable();
        actions.FindActionMap("UI").Enable();
        deactivatePause.action.Enable();
        deactivatePause.action.performed += ctx => PauseDisable();

        Time.timeScale = 0f;
        pause.SetActive(true);

        var pauseUi = pause.GetComponent<UIDocument>();
        var _root = pauseUi.rootVisualElement;

        _root.Q<Button>("resumeButton").clicked += () =>
        {
            ClickSfx.Instance.PlaySound();
            PauseDisable();
        };
        _root.Q<Button>("exitButton").clicked += () =>
        {
            SceneManager.LoadScene("Menu");
        };
    }

    // Disables the pause state: resumes time, hides UI, switches input actions.
    void PauseDisable()
    {
        actions.FindActionMap("UI").Disable();
        actions.FindActionMap("Player").Enable();
        activatePause.action.Enable();
        activatePause.action.performed += ctx => PauseEnable();

        Time.timeScale = 1f;
        pause.SetActive(false);
    }
}