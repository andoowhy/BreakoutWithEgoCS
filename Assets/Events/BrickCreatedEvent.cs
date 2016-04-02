using UnityEngine;

public class BrickCreatedEvent : EgoEvent
{
    public int totalNumBricks;

    public BrickCreatedEvent( int totalNumBricks )
    {
        this.totalNumBricks = totalNumBricks;
    }
}
