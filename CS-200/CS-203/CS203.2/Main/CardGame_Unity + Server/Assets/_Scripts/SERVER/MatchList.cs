using Riptide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wfMultiplayer;

public class MatchList : MonoBehaviour
{
    public static List<Match> list = new List<Match>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*****************************Lobby Creation*****************************/

    public static Match CreateNewMatch(List<List<char>> gameBoard, List<Player> players)
    {
        Match match = new Match(gameBoard, players);
        Debug.Log($"Creating match {list.Count}, {match}");
        list.Add(match);
        return match;
    }

    private void LogNewLobby(List<Player> players)
    {
        string playerText = "";
        foreach (var player in players)
        {
            playerText += player.ToString() + ", ";
        }
        playerText = playerText.Substring(0, playerText.Length - 3);
        Debug.Log($"Creating match {list.Count} with players: " + playerText);

    }


    /*****************************Lobby Destruction**************************/
    public static void PlayerDisconnect(Match match)
    {
        Debug.Log("Destroying match!");
        if (match != null)
        {
            foreach (var player in match.GetPlayers())
            {

                Message message = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.sendOpponentDisconnect);
                NetworkManager.Singleton.Server.Send(message, player.Id);
                Debug.Log($"{player} is now set to idle");
                player.status = PlayerStatus.Idle;
                player.currentMatch = null;
            }

            DestroyLobby(match);
        }
        
    }

    private static void DestroyLobbyByPlayer(Player player)
    {
        Debug.Log("Destroying lobby!");
        for (int i = 0; i < list.Count; i++)
        {
            Match item = list[i];
            if (item.GetPlayers().Contains(player))
            {
                Debug.Log($"Ending match {i}, {item}");
                list.Remove(item);
                return;
            }
        }
    }
    private static void DestroyLobby(Match match)
    {

        if (list.Contains(match))
        {
            list.Remove(match);
        }
    }
}

