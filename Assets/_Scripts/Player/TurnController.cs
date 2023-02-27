using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    private Player currentPlayer;

    private void TurnControl()
    {
        int turn = 0;

        currentPlayer = players[turn % 4];
        
    }
}
