using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class CameraColor : MonoBehaviour, IPauseHandler
{
    [SerializeField] Color[] colors;
    [SerializeField] int time;
    UIController uIController;
    Camera camera;
    Sequence tween;
    Color color;
    int counter = 0;

    void Awake()
    {
        camera = GetComponent<Camera>();
        uIController = FindObjectOfType<UIController>();
        tween = DOTween.Sequence();
    }

    void Start()
    {
        uIController.PauseManager.Register(this);
        ChangeColor();
    }

    void OnDisable() => uIController.PauseManager.UnRegister(this);

    Color GetColor()
    {
        if(counter < colors.Length)
        {
           return UpdateColor();
        }
        else
        {
            counter = 0;
            return UpdateColor();
        }       
    }

    Color UpdateColor()
    {
        color = colors[counter];
        counter++;
        return color;
    }

    public void ChangeColor()
    {
        camera.DOColor(GetColor(), time).OnComplete(ChangeColor);
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused) camera.DOPause();
        else camera.DOPlay();
    }
}
