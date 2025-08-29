using System;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    private static event Action OnMaxClientsWereConnected;

    public static void SubOnMaxClientsWereConnected(Action callback)
    {
        OnMaxClientsWereConnected += callback;
    }

    public static void UnsubOnMaxClientsWereConnected(Action callback)
    {
        OnMaxClientsWereConnected -= callback;
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if (numPlayers == maxConnections)
        {
            OnMaxClientsWereConnected?.Invoke();
        }

        base.OnServerConnect(conn);
    }
}
