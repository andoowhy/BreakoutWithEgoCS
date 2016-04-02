using UnityEngine;
using System.Collections;

public class PaddleSystem : EgoSystem<Transform, Paddle, SpriteRenderer>
{
    public override void Start()
    {
        foreach (var bundle in bundles)
        {
            var transform = bundle.component1;
            var paddle = bundle.component2;

            paddle.initialPosition = transform.position;
        }

        EgoEvents<GameEndEvent>.AddHandler( Handle );
        EgoEvents<ResetGameEvent>.AddHandler(Handle);
    }

    public override void Update()
    {
        foreach( var bundle in bundles )
        {
            if (bundle.egoComponent.HasComponents<Pause>()) continue;

            var transform = bundle.component1;
            var paddle = bundle.component2;
            var sprite = bundle.component3;

            // Move Paddle Right
            if( Input.GetKey( KeyCode.RightArrow) )
            {
                MovePaddle( transform, paddle.speed );
            }

            // Move Paddle Left
            if( Input.GetKey( KeyCode.LeftArrow ) )
            {
                MovePaddle( transform, -paddle.speed );
            }

            // Keep Paddle within boundaries
            var right = paddle.right - sprite.bounds.extents.x;
            if( transform.position.x >= right )
            {
                transform.SetX( right );
            }
            var left = paddle.left + sprite.bounds.extents.x;
            if( transform.position.x <= left )
            {
                transform.SetX( left );
            }

        }
    }

    // Event Handler Methods

    void Handle( GameEndEvent e )
    {
        foreach (var bundle in bundles)
        {
            Ego.AddComponent<Pause>( bundle.egoComponent.gameObject );
        }
    }

    void Handle( ResetGameEvent e )
    {
        foreach (var bundle in bundles)
        {
            var transform = bundle.component1;
            var paddle = bundle.component2;

            transform.position = paddle.initialPosition;
            Ego.Destroy<Pause>( bundle.egoComponent.gameObject );
        }
    }

    // Helper Methods

    void MovePaddle(Transform transform, float speed)
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
