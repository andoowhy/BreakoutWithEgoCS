using UnityEngine;
using System.Collections;

public class BallSystem : EgoSystem<
	EgoConstraint<Transform, Ball, Rigidbody2D, SpriteRenderer>
>{
	public override void Start()
	{
		// Set the ball's initial position from the editor
		constraint.ForEachGameObject( ( egoComponent, transform, ball, rigidbody2D, spriteRenderer ) =>
		{
			ball.initialPosition = transform.position;
		} );

		Reset();

		// Add Event Handler
		EgoEvents<CollisionEnter2DEvent>.AddHandler( Handle );
		EgoEvents<GameEndEvent>.AddHandler( Handle );
		EgoEvents<ResetGameEvent>.AddHandler( Handle );
	}

	public override void FixedUpdate()
	{
		constraint.ForEachGameObject( ( egoComponent, transform, ball, rigidbody2D, spriteRenderer ) =>
		{
			if( egoComponent.HasComponents<Pause>() )
			{
				if( rigidbody2D.IsAwake() ) rigidbody2D.Sleep();
			}
			else
			{
				rigidbody2D.velocity = rigidbody2D.velocity.normalized * ball.initialSpeed;
			}
		} );
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
		constraint.ForEachGameObject( ( egoComponent, transform, ball, rigidbody2D, spriteRenderer ) =>
		{
			Ego.AddComponent<Pause>( egoComponent );
		} );
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
		var dir = ( collision.contacts[ 0 ].point - ( Vector2 )paddleTransform.position );

		// Adjust the new direction's severity
		dir /= 3f;

		// Change the ball's direction
		dir.Normalize();
		ballRb2d.velocity = ballRb2d.velocity.magnitude * dir;
	}

	void Reset()
	{
		constraint.ForEachGameObject( ( egoComponent, transform, ball, rigidbody2D, spriteRenderer ) =>
		{
			Ego.DestroyComponent<Pause>( egoComponent );

			// Move the ball back to its initial position
			transform.position = ball.initialPosition;

			// Give the ball a random velocity
			var theta = Random.Range( 45f, 180f - 45f );
			var velocity = Quaternion.Euler( 0f, 0f, theta ) * Vector2.right;
			rigidbody2D.velocity = ball.initialSpeed * velocity;
		} );
	}
}
