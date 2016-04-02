using UnityEngine;

public class UpdateScoreUIEvent : EgoEvent
{
    public readonly int newScore;

    public UpdateScoreUIEvent( int newScore )
    {
        this.newScore = newScore;
    }
}
