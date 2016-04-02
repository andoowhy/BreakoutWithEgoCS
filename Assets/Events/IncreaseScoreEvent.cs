using UnityEngine;

public class IncreaseScoreEvent : EgoEvent
{
    public int increment = 0;

    public IncreaseScoreEvent( int newScore )
    {
        this.increment = newScore;
    }
}