using UnityEngine;

public class Wait : Instruction
{
    float startTime;
    public float Delay { get; set; }

    protected override void OnPaused() => Delay -= Time.time - startTime;

    protected override void OnResumed() => startTime = Time.time;

    protected override void OnStarted() => startTime = Time.time;

    protected override bool Update() => Time.time - startTime<Delay;


    public Wait(MonoBehaviour parent) : base(parent) { }

    public Wait(float delay, MonoBehaviour parent) : base(parent) => Delay = delay;

    public Instruction Execute(float delay)
    {
        Delay = delay;

        return Execute();
    }

}
