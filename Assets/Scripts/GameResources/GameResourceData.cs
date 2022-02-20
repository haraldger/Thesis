public class GameResourceData
{
    public string Code { get; private set; }

    private int _currentAmount;
    public int CurrentAmount
    {
        get => _currentAmount;
        set
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

}
