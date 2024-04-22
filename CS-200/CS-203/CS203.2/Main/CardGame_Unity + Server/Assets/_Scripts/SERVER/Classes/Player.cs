using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace wfMultiplayer
{


    public enum PlayerStatus
    {
        Idle = 1,
        Searching = 2,
        InMatch = 3,
    }
    public class Player
    {
        public static Player GetPlayerById(ushort id)
        {
            return list[id];
        }

        public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();
        public ushort Id { get; set; }
        public string Username { get; set; }

        public PlayerStatus status { get; set; }

        public Match currentMatch { get; set; }

        public Player(ushort Id, string Username)
        {
            this.Id = Id;
            this.Username = Username;
            status = PlayerStatus.Idle;
        }

        public override string ToString()
        {
            return "Player [" + Username + ":" + Id + "]";
        }
    }
}

