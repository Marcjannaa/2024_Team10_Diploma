

public class Card
{
    private string color;
    private string name;
    private int value;

    public Card(string color, string name, int value)
    {
        this.color = color;
        this.name = name;
        this.value = value;
    }
    
    
    public string GetColor() => color;
    public string GetName() => name;
    public int GetValue() => value;
}
