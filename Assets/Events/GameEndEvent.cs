using UnityEngine;
using System.Collections;

public enum GameEnd
{
    LOSE,
    WIN,
};

public class GameEndEvent : EgoEvent
{
    public GameEnd gameEnd;

    public GameEndEvent( GameEnd gameEnd )
    {
        this.gameEnd = gameEnd;
    }
}
