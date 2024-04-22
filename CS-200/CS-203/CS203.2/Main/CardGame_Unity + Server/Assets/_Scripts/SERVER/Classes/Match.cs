using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wfMultiplayer
{
    public class Match
    {
        private Board gameBoard;
        private List<Player> players;
        //int playerTurnIndex;

        public Match(List<List<char>> gameBoard, List<Player> players)
        {
            Debug.Log("Beginning match creation process...");
            this.gameBoard = new Board(gameBoard);
            this.players = players;
            //playerTurnIndex = 0;
            Debug.Log("Match initialized.");
            Debug.Log(this.gameBoard);
        }
        public Board GetGameBoard() { return gameBoard; }

        public char GetGameBoardItem(int x, int y) { return gameBoard.boardChars[x][y]; }

        public void SetGameBoardItem(int x, int y, char c)
        {
            gameBoard.boardChars[x][y] = c;
        }
        public List<Player> GetPlayers()
        {
            return players;
        }

        public override string ToString()
        {
            string playerText = "[";
            foreach (var player in players)
            {
                playerText += player.ToString() + ", ";
            }
            playerText = playerText.Substring(0, playerText.Length - 3) + "]";
            return playerText;
        }
    }
}