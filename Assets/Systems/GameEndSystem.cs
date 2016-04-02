using UnityEngine;
using System.Collections;

public class GameEndSystem : EgoSystem<BrickCount>
{
    public override void Start()
    {
        EgoEvents<TriggerEnter2DEvent>.AddHandler( Handle );
        EgoEvents<BrickCreatedEvent>.AddHandler( Handle );
        EgoEvents<BrickHitEvent>.AddHandler( Handle );
        EgoEvents<ResetGameEvent>.AddHandler(Handle);
    }

    void Handle( TriggerEnter2DEvent e )
    {
        if( e.egoComponent1.HasComponents<Ball>() && e.egoComponent2.HasComponents<LoseArea>() )
        {
            EgoEvents<GameEndEvent>.AddEvent( new GameEndEvent( GameEnd.LOSE) );
        }
        else if( e.egoComponent1.HasComponents<LoseArea>() && e.egoComponent2.HasComponents<Ball>() ) 
        {
            EgoEvents<GameEndEvent>.AddEvent(new GameEndEvent(GameEnd.LOSE));
        }
    }

    void Handle( BrickCreatedEvent e )
    {
        foreach( var bundle in bundles )
        {
            var brickCount = bundle.component1;

            brickCount.initialNumBricks = e.totalNumBricks;
            brickCount.currentNumBricks++;

            break;
        }
    }

    void Handle( BrickHitEvent e )
    {
        foreach( var bundle in bundles )
        {
            var brickCount = bundle.component1;

            brickCount.currentNumBricks--;
            if( brickCount.initialNumBricks > 0 && brickCount.currentNumBricks <= 0 )
            {
                EgoEvents<GameEndEvent>.AddEvent( new GameEndEvent( GameEnd.WIN ) );
            }

            break;
        }
    }
     
    void Handle( ResetGameEvent e )
    {
        foreach( var bundle in bundles )
        {
            var brickCount = bundle.component1;

            brickCount.currentNumBricks = 0;
            brickCount.initialNumBricks = 0;
        }
    }
}
