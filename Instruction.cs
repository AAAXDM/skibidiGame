using UnityEngine;
using System;

using IEnumerator = System.Collections.IEnumerator;

public abstract class Instruction : IEnumerator
{
    private Instruction current;
    object IEnumerator.Current => current;

    private object routine;
    public MonoBehaviour Parent { get; private set; }

    public bool IsExecuting { get; private set; }
    public bool IsPaused { get; private set; }

    private bool IsStopped { get; set; }

    public event Action<Instruction> Started;
    public event Action<Instruction> Paused;
    public event Action<Instruction> Terminated;
    public event Action<Instruction> Done;

    void IEnumerator.Reset()
    {
        IsPaused = false;
        IsStopped = false;

        routine = null;
    }

    bool IEnumerator.MoveNext()
    {
        if (IsStopped)
        {
            (this as IEnumerator).Reset();
            return false;
        }

        if (!IsExecuting)
        {
            IsExecuting = true;
            routine = new object();

            OnStarted();
            Started?.Invoke(this);
        }

        if (current != null)
            return true;

        if (IsPaused)
            return true;

        if (!Update())
        {
            OnDone();
            Done?.Invoke(this);

            IsStopped = true;
            return false;
        }

        return true;
    }

    protected Instruction(MonoBehaviour parent) => Parent = parent;

    public void Pause()
    {
        if (IsExecuting && !IsPaused)
        {
            IsPaused = true;

            OnPaused();
            Paused?.Invoke(this);
        }
    }

    public void Resume()
    {
        IsPaused = false;
        OnResumed();
    }

    public void Terminate()
    {
        if (Stop())
        {
            OnTerminated();
            Terminated?.Invoke(this);
        }
    }

    private bool Stop()
    {
        if (IsExecuting)
        {
            if (routine is Coroutine)
                Parent.StopCoroutine(routine as Coroutine);

            (this as IEnumerator).Reset();

            return IsStopped = true;
        }

        return false;
    }

    public Instruction Execute()
    {
        if (current != null)
        {
            Debug.LogWarning($"Instruction {GetType().Name} is currently waiting for another one and can't be stared right now.");
            return this;
        }

        if (!IsExecuting)
        {
            IsExecuting = true;
            routine = Parent.StartCoroutine(this);

            return this;
        }

        Debug.LogWarning($"Instruction {GetType().Name} is already executing.");
        return this;
    }

    public Instruction Execute(MonoBehaviour parent)
    {
        if (current != null)
        {
            Debug.LogWarning($"Instruction {GetType().Name} is currently waiting for another one and can't be stared right now.");
            return this;
        }

        if (!IsExecuting)
        {
            IsExecuting = true;
            routine = (Parent = parent).StartCoroutine(this);

            return this;
        }

        Debug.LogWarning($"Instruction {GetType().Name} is already executing.");
        return this;
    }

    public void Reset()
    {
        Terminate();

        Started = null;
        Paused = null;
        Terminated = null;
        Done = null;
    }

    protected virtual void OnStarted() { }
    protected virtual void OnPaused() { }
    protected virtual void OnResumed() { }
    protected virtual void OnTerminated() { }
    protected virtual void OnDone() { }

    protected abstract bool Update();
}
