namespace VendingMachineOOO.Domain;

public abstract class Coffee
{
    private List<Addon> _addons = new List<Addon>();
    public void AddCream(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _addons.Add(new Cream());
        }
    }

    public void AddSugar(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _addons.Add(new Sugar());
        }
    }


    public int NumberOfSugar
    {
        get { return _addons.Count(s => s is Sugar); }
    }

    public int NumberOfCream
    {
        get { return _addons.Count(s => s is Cream); }
    }
}

public class LargeCoffee : Coffee;
public class MediumCoffee : Coffee;
public class SmallCoffee : Coffee;
