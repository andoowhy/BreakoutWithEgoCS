using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayAgainButtonSystem : EgoSystem<
	EgoConstraint<Button, UIPlayAgain>
>{
	public override void Start()
	{
		constraint.ForEachGameObject( ( egoComponent, button, uiPlayAgain ) =>
		{
			button.onClick.AddListener( () =>
			{
				EgoEvents<ResetGameEvent>.AddEvent( new ResetGameEvent() );
			} );
		} );
	}
}
