using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pannel;
    [SerializeField] GameObject bonusPannel;
    [SerializeField] GameObject mobileControl;
    PlayerAction action;
    bool isPaused;

    public PauseManager PauseManager { get; private set; }

    void Awake()
    {
        action = new PlayerAction();
        PauseManager = new PauseManager();
        isPaused = false;
        pannel.SetActive(isPaused);
        bonusPannel.SetActive(false);
        if (GameManager.Instance.DeviceType == DeviceType.mobile) 
            mobileControl.SetActive(true);
    }

    void OnEnable()
    {
        action.Player.Pause.performed += TogglePause;
        action.Player.Pause.Enable();
    }

    void OnDisable()
    {
        action.Player.Pause.performed -= TogglePause;
        action.Player.Pause.Disable();
    }

    void TogglePause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        pannel.SetActive(isPaused);
        PauseManager.SetPaused(isPaused);
    }

    public void ActiveBonusPannel() => bonusPannel.SetActive(true);
}
