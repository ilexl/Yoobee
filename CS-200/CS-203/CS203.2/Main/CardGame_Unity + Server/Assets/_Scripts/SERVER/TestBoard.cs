using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using wfMultiplayer;
public class TestBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<List<char>> boardList = new List<List<char>>();
        Board board = new Board(boardList);
        Debug.Log(board);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
