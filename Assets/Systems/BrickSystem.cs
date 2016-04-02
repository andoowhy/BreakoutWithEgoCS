using UnityEngine;
using System.Collections;

public class BrickSystem : EgoSystem
{
    public override void Start()
    {
        // Add Event Handlers
        EgoEvents<CollisionExit2DEvent>.AddHandler( Handle );
    }

    void Handle( CollisionExit2DEvent e )
    {
        // Destroy the brick if a ball hits it
        Brick brick;
        if( e.egoComponent1.TryGetComponents( out brick ) && e.egoComponent2.HasComponents<Ball>() )
        {
            BrickHit( brick );            
        }
        if( e.egoComponent1.HasComponents<Ball>() && e.egoComponent2.TryGetComponents( out brick ) )
        {
            BrickHit( brick );
        }
    }

    void BrickHit( Brick brick )
    {
        EgoEvents<IncreaseScoreEvent>.AddEvent( new IncreaseScoreEvent( brick.score ) );
        EgoEvents<BrickHitEvent>.AddEvent( new BrickHitEvent() );
        Ego.Destroy( brick.gameObject );
    }
}
