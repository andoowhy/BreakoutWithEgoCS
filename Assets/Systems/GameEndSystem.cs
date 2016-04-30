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
        ForEachGameObject( ( EgoComponent ego, BrickCount brickCount ) =>
        {
            brickCount.initialNumBricks = e.totalNumBricks;
            brickCount.currentNumBricks++;
        } );
    }

    void Handle( BrickHitEvent e )
    {
        ForEachGameObject( ( EgoComponent ego, BrickCount brickCount ) =>
        {
            brickCount.currentNumBricks--;
            if( brickCount.initialNumBricks > 0 && brickCount.currentNumBricks <= 0 )
            {
                EgoEvents<GameEndEvent>.AddEvent( new GameEndEvent( GameEnd.WIN ) );
            }
        } );
    }
     
    void Handle( ResetGameEvent e )
    {
        ForEachGameObject( ( EgoComponent ego, BrickCount brickCount ) =>
        {
            brickCount.currentNumBricks = 0;
            brickCount.initialNumBricks = 0;
        } );
    }
}
