using UnityEngine;
using System.Collections;

public class PaddleSystem : EgoSystem<
	EgoConstraint<Transform, Paddle, SpriteRenderer>
>{
	public override void Start()
	{
		constraint.ForEachGameObject( ( ego, transform, paddle, spriteRenderer ) =>
		{
			paddle.initialPosition = transform.position;
		} );

		EgoEvents<GameEndEvent>.AddHandler( Handle );
		EgoEvents<ResetGameEvent>.AddHandler( Handle );
	}

	public override void Update()
	{
		constraint.ForEachGameObject( ( ego, transform, paddle, spriteRenderer ) =>
		{
			if( ego.HasComponents<Pause>() ) return;

			// Move Paddle Right
			if( Input.GetKey( KeyCode.RightArrow ) )
			{
				MovePaddle( transform, paddle.speed );
			}

			// Move Paddle Left
			if( Input.GetKey( KeyCode.LeftArrow ) )
			{
				MovePaddle( transform, -paddle.speed );
			}

			// Keep Paddle within boundaries
			var right = paddle.right - spriteRenderer.bounds.extents.x;
			if( transform.position.x >= right )
			{
				transform.SetX( right );
			}
			var left = paddle.left + spriteRenderer.bounds.extents.x;
			if( transform.position.x <= left )
			{
				transform.SetX( left );
			}
		} );
	}

	// Event Handler Methods

	void Handle( GameEndEvent e )
	{
		constraint.ForEachGameObject( ( ego, transform, paddle, spriteRenderer ) =>
		{
			Ego.AddComponent<Pause>( ego );
		} );
	}

	void Handle( ResetGameEvent e )
	{
		constraint.ForEachGameObject( ( ego, transform, paddle, spriteRenderer ) =>
		{
			transform.position = paddle.initialPosition;
			Ego.DestroyComponent<Pause>( ego );
		} );
	}

	// Helper Methods

	void MovePaddle( Transform transform, float speed )
	{
		transform.Translate( Vector3.right * speed * Time.deltaTime );
	}
}
