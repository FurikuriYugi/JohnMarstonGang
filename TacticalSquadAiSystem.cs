using System.Collections.Generic;
using Newtonsoft.Json;

public class TacticalSquadAiSystem
{
    private readonly List<TacticalOrderData> _orders = new List<TacticalOrderData>();

    public List<TacticalOrderData> GetOrders()
    {
        return new List<TacticalOrderData>(_orders);
    }

    public TacticalOrderData GetOrder(string orderId)
    {
        if (string.IsNullOrWhiteSpace(orderId))
            return null;

        return _orders.Find(x => x != null && x.OrderId == orderId);
    }

    public void RegisterOrder(TacticalOrderData order)
    {
        if (order == null || string.IsNullOrWhiteSpace(order.OrderId))
            return;

        _orders.RemoveAll(x => x != null && x.OrderId == order.OrderId);
        _orders.Add(order);
    }

    public void RemoveOrder(string orderId)
    {
        _orders.RemoveAll(x => x != null && x.OrderId == orderId);
    }

    public void MarkCompleted(string orderId)
    {
        TacticalOrderData order = GetOrder(orderId);
        if (order == null)
            return;

        order.IsCompleted = true;
    }

    public string SaveState()
    {
        return JsonConvert.SerializeObject(_orders, Formatting.Indented);
    }

    public void LoadState(string json)
    {
        _orders.Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        List<TacticalOrderData> orders = JsonConvert.DeserializeObject<List<TacticalOrderData>>(json);
        if (orders != null)
            _orders.AddRange(orders);
    }
}