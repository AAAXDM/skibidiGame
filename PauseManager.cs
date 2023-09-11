using System.Collections.Generic;

public class PauseManager : IPauseHandler
{
    List<IPauseHandler> handlers = new List<IPauseHandler>();
    bool isPaused;

    public bool IsPaused => isPaused;

    public void Register(IPauseHandler handler) => handlers.Add(handler);

    public void UnRegister(IPauseHandler handler) => handlers.Remove(handler);

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
        foreach (IPauseHandler handler in handlers)
        {
            handler.SetPaused(isPaused);
        }
    }
}
