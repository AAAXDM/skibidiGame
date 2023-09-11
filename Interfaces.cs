using System;

public interface IPauseHandler
{
    void SetPaused(bool isPaused);
}

public interface IBuyHandler
{
    public event Action<IBuyHandler> WantToBuy;
    void TryToBuy();
    void Buy();
}
public interface IInstruction
{
    bool IsExecuting { get; }
    bool IsPaused { get; }

    Instruction Execute();
    void Pause();
    void Resume();
    void Terminate();

    event Action<Instruction> Started;
    event Action<Instruction> Paused;
    event Action<Instruction> Cancelled;
    event Action<Instruction> Done;
}

public interface IItem
{
    public int Id { get; }
    public int Price { get; }
    public bool IsByuing { get; }
    void Buy();
}

public delegate void CollisionDelegate(Obstacle obstacle);

public delegate void TriggerDelegate();

public enum ObstacleType {Box, Floor, Coin, Power}

public enum PowerType {coins,immortal, speed, magnet }

public enum DeviceType {desktop, mobile }


