using System.Collections.Generic;

public class Inventory
{
    private readonly List<Item> _items = new();
    private readonly int _capacity;

    public Inventory(int capacity = 10)
    {
        _capacity = capacity;
    }

    public bool AddItem(Item item)
    {
        if (_items.Count >= _capacity)
            return false;

        _items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item)
    {
        return _items.Remove(item);
    }

    public IReadOnlyList<Item> GetItems()
    {
        return _items.AsReadOnly();
    }

    public bool IsFull => _items.Count >= _capacity;
    public int Count => _items.Count;
}