// gere o UserCounter () 
// e StartIncrementing, incrementa o counter mum loop

using System;
using System.Threading.Tasks;

public class UserCounter
{
    public int Counter { get; private set; } = 0;


    // avisa mudanças
    public event Action? OnCounterChanged;

    public async Task StartIncrementing(int maxValue, int step, int delay)
    {
        for (int i = 0; i < maxValue; i += step)
        {
            Counter += step;
            OnCounterChanged?.Invoke();
            await Task.Delay(delay);
        }
    }
}
