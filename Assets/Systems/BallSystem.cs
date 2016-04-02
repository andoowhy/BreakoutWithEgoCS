using UnityEngine;
using System.Collections;

public class BallSystem : EgoSystem<Transform, Ball, Rigidbody2D, SpriteRenderer>
{ 
    public override void Start()
    {
        Debug.Log( "BallSystem Start: " + bundles.Count.ToString() + " bundles" );

        // Set the ball's initial position from the editor
        foreach( var bundle in bundles )
        {
            var transform = bundle.component1;
            var ball = bundle.component2;

            ball.initialPosition = transform.position;
        }

        Reset();

        // Add Event Handler
        EgoEvents<CollisionEnter2DEvent>.AddHandler( Handle );
        EgoEvents<GameEndEvent>.AddHandler(Handle);
        EgoEvents<ResetGameEvent>.AddHandler( Handle );        
    }

    public override void FixedUpdate()
    {
        foreach (var bundle in bundles)
        {
            var ball = bundle.component2;
            var rb2D = bundle.component3;

            if (ball && rb2D)
            {
                if (bundle.egoComponent.HasComponents<Pause>())
                {
                    if (rb2D.IsAwake()) rb2D.Sleep();
                }
                else
                {
                    rb2D.velocity = rb2D.velocity.normalized * ball.initialSpeed;
                }
            }
        }
    }

    // Event Handler Methods

    void Handle( CollisionEnter2DEvent e )
    {
        // When a Ball hits a Paddle, change the Ball's direction
        if( e.egoComponent1.HasComponents<Ball>() && e.egoComponent2.HasComponents<Paddle>() )
        {
            ChangeBallDirection( e.egoComponent1, e.egoComponent2, e.collision );
        }
        else if( e.egoComponent1.HasComponents<Paddle>() && e.egoComponent2.HasComponents<Ball>() )
        {
            ChangeBallDirection( e.egoComponent2, e.egoComponent1, e.collision );
        }
    }

    void Handle( GameEndEvent e )
    {
        foreach (var bundle in bundles)
        {
            Ego.AddComponent<Pause>( bundle.egoComponent.gameObject );
        }
    }

    void Handle( ResetGameEvent e )
    {
        Reset();
    }

    // Helper Methods

    void ChangeBallDirection( EgoComponent ballEgoComponent, EgoComponent paddleEgoComponent, Collision2D collision )
    {
        // Paddle Components
        Transform paddleTransform = paddleEgoComponent.transform;
        SpriteRenderer paddleSprite;
        if( !paddleEgoComponent.TryGetComponents( out paddleSprite ) ) return;

        // Ball Components
        Rigidbody2D ballRb2d;
        if( !ballEgoComponent.TryGetComponents( out ballRb2d ) ) return;

        // Get the direction from the Paddle's center to the contact point
        var dir = ( collision.contacts[0].point - (Vector2)paddleTransform.position );

        // Adjust the new direction's severity
        dir /= 3f;

        // Change the ball's direction
        dir.Normalize();
        ballRb2d.velocity = ballRb2d.velocity.magnitude * dir;
    }

    void Reset()
    {
        foreach (var bundle in bundles)
        {
            var transform = bundle.component1;
            var ball = bundle.component2;
            var rb2D = bundle.component3;

            Ego.Destroy<Pause>( bundle.egoComponent.gameObject );

            // Move the ball back to its initial position
            transform.position = ball.initialPosition;

            // Give the ball a random velocity
            var theta = Random.Range(45f, 180f - 45f);
            var velocity = Quaternion.Euler(0f, 0f, theta) * Vector2.right;
            rb2D.velocity = ball.initialSpeed * velocity;
        }
       
    }
}
