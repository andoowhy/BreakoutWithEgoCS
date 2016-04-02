using UnityEngine;
using System.Collections;

public class BrickInstantiationSystem : EgoSystem<Transform, BrickInstantiation>
{
    public override void Start()
    {
        InstantiateBricks();

        EgoEvents<ResetGameEvent>.AddHandler( Handle );
    }

    // Event Handlers

    void Handle( ResetGameEvent e )
    {
        InstantiateBricks();
    }

    // Helper Methods

    void InstantiateBricks()
    {
        foreach( var bundle in bundles )
        {
            var transform = bundle.component1;
            var brickInstantiation = bundle.component2;

            // Remove all previous Bricks
            for( int i = 0; i < transform.childCount; i++ )
            {
                Ego.Destroy( transform.GetChild( i ).gameObject );
            }

            // Create Bricks
            for( int row = 0; row < brickInstantiation.rows; row++ )
            {
                for( int col = 0; col < brickInstantiation.columns; col++ )
                {
                    // GameObject & EgoComponent
                    var brickIndex = Random.Range( 0, brickInstantiation.tiles.Count );
                    if( !brickInstantiation.tiles[brickIndex] ) return;
                    var brickEgo = Ego.AddGameObject( GameObject.Instantiate( brickInstantiation.tiles[ brickIndex ] ) );
                    brickEgo.gameObject.name = "brick";
                    brickEgo.gameObject.transform.parent = transform;

                    // Position
                    SpriteRenderer brickSprite;
                    if( !brickEgo.TryGetComponents( out brickSprite ) ) return;

                    var x = brickSprite.bounds.size.x * ( col + -brickInstantiation.columns / 2.0f + 0.5f );
                    var y = brickSprite.bounds.size.y * ( row + -brickInstantiation.rows / 2.0f + 0.5f );
                    brickEgo.transform.localPosition = new Vector2( x, y );

                    var e = new BrickCreatedEvent( brickInstantiation.rows * brickInstantiation.columns );
                    EgoEvents<BrickCreatedEvent>.AddEvent( e );
                }
            }

            break;
        }
    }

}
