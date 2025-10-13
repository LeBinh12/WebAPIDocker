using Fleck;

namespace sepending.Application.Services;

public class WebSocketConnectionManager
{
    private readonly Dictionary<int, List<IWebSocketConnection>> _connections = new();

    
    public void AddConnection(int userId, IWebSocketConnection connection)
    {
        if (!_connections.ContainsKey(userId))
            _connections[userId] = new List<IWebSocketConnection>();

        _connections[userId].Add(connection);
    }



    public void RemoveConnection(int userId, IWebSocketConnection connection)
    {
        if (_connections.TryGetValue(userId, out var list))
        {
            list.Remove(connection);
            if (list.Count == 0)
                _connections.Remove(userId);
        }
    }
    
    public IEnumerable<IWebSocketConnection> GetConnections(int userId)
    {
        return _connections.TryGetValue(userId, out var list) ? list : Enumerable.Empty<IWebSocketConnection>();
    }
    
    public void ClearAllConnections()
    {
        foreach (var list in _connections.Values)
        {
            foreach (var conn in list)
            {
                if (conn.IsAvailable)
                {
                    conn.Close(); // đóng socket
                }
            }
        }
        _connections.Clear(); // xóa danh sách
    }

}