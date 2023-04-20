using Data.Entities;

namespace Data.Model;

public class Cart
{
    private List<CartLine> lineCollection = new();

    public IEnumerable<CartLine> Lines => lineCollection;

    public void AddItem(Food food, int quantity)
    {
        CartLine line = lineCollection.FirstOrDefault(p => p.Food.Id == food.Id);

        if (line == null)
        {
            lineCollection.Add(new CartLine { Food = food, Quantity = quantity });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public void RemoveLine(Food food)
    {
        lineCollection.RemoveAll(l => l.Food.Id == food.Id);
    }

    public decimal ComputeTotalValue()
    {
        return lineCollection.Sum(l => l.Food.Price * l.Quantity);
    }

    public void Clear()
    {
        lineCollection.Clear();
    }
}
public class CartLine
{
    public Food Food { get; set; }
    public int Quantity { get; set; }
}