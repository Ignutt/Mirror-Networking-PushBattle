using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkRoomController : NetworkRoomManager
{
    [SerializeField] private Transform playersInRoomLayout;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        clientIndex++;

        if (IsSceneActive(RoomScene))
        {
            if (roomSlots.Count == maxConnections)
                return;

            allPlayersReady = false;

            GameObject newRoomGameObject = OnRoomServerCreateRoomPlayer(conn);
            if (newRoomGameObject == null)
            {
                newRoomGameObject = Instantiate(roomPlayerPrefab.gameObject, playersInRoomLayout);
            }

            NetworkServer.AddPlayerForConnection(conn, newRoomGameObject);
        }
        else
            OnRoomServerAddPlayer(conn);
    }
}
