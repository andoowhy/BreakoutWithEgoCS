using UnityEngine;
using System.Collections;

public class PaddleSystem : EgoSystem<Transform, Paddle, SpriteRenderer>
{
    public override void Start()
    {
        ForEachGameObject( ( EgoComponent ego, Transform transform, Paddle paddle, SpriteRenderer spriteRenderer ) =>
        {
            paddle.initialPosition = transform.position;
        } );

        EgoEvents<GameEndEvent>.AddHandler( Handle );
        EgoEvents<ResetGameEvent>.AddHandler(Handle);
    }

    public override void Update()
    {
        ForEachGameObject( ( EgoComponent ego, Transform transform, Paddle paddle, SpriteRenderer spriteRenderer ) =>
        {
            if( ego.HasComponents<Pause>() ) return;

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
        ForEachGameObject((EgoComponent ego, Transform transform, Paddle paddle, SpriteRenderer spriteRenderer) =>
        {
            Ego.AddComponent<Pause>( ego.gameObject );
        } );
    }

    void Handle( ResetGameEvent e )
    {
        ForEachGameObject((EgoComponent ego, Transform transform, Paddle paddle, SpriteRenderer spriteRenderer) =>
        {
            transform.position = paddle.initialPosition;
            Ego.Destroy<Pause>( ego.gameObject );
        } );
    }

    // Helper Methods

    void MovePaddle(Transform transform, float speed)
    {
        transform.Translate( Vector3.right * speed * Time.deltaTime );
    }
}
