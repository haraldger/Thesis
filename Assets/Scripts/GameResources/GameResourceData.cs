public class GameResourceData
{
    public string Code { get; private set; }

    private int _currentAmount;
    public int CurrentAmount
    {
        get => _currentAmount;
        private set
        {
            if (value > Cap)
            {
                _currentAmount = Cap;
            }
            else if (value < 0)
            {
                _currentAmount = 0;
            }
            else
            {
                _currentAmount = value;
            }
        }
    }

    public int Cap { get; set; }

    public GameResourceData(string code, int cap, int initialAmount)
    {
        Code = code;
        Cap = cap;
        CurrentAmount = initialAmount;
    }

    public bool CanConsumeResource( int amount)
    {
        if (CurrentAmount - amount >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ConsumeResource(int amount)
    {
        CurrentAmount -= amount;
    }

    public void AddResource(int amount)
    {
        CurrentAmount += amount;
    }

}
