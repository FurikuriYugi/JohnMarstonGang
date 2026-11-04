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

    public TacticalOrderData CreateOrder(
        string orderId,
        string issuerId,
        string targetId,
        string orderType,
        string combatStyle,
        string currentTask,
        int priority,
        bool requiresStealth,
        bool requiresCover,
        bool requiresNonLethal)
    {
        TacticalOrderData order = new TacticalOrderData
        {
            OrderId = orderId,
            IssuerId = issuerId,
            TargetId = targetId,
            OrderType = orderType,
            CombatStyle = combatStyle,
            CurrentTask = currentTask,
            Priority = priority,
            RequiresStealth = requiresStealth,
            RequiresCover = requiresCover,
            RequiresNonLethal = requiresNonLethal,
            IsActive = true,
            IssuedUtc = System.DateTime.UtcNow
        };

        RegisterOrder(order);
        return order;
    }

    public void AssignMember(string orderId, string memberId)
    {
        TacticalOrderData order = GetOrder(orderId);
        if (order == null || string.IsNullOrWhiteSpace(memberId))
            return;

        if (!order.AssignedMemberIds.Contains(memberId))
            order.AssignedMemberIds.Add(memberId);
    }

    public void AddNote(string orderId, string note)
    {
        TacticalOrderData order = GetOrder(orderId);
        if (order == null || string.IsNullOrWhiteSpace(note))
            return;

        order.Notes.Add(note);
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