using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayAgainButtonSystem : EgoSystem<Button, UIPlayAgain>
{
    public override void Start()
    {
        ForEachGameObject( ( EgoComponent egoComponent, Button button, UIPlayAgain uiPlayAgain ) =>
        {
            button.onClick.AddListener(() =>
            {
                EgoEvents<ResetGameEvent>.AddEvent(new ResetGameEvent());
            });
        } );
    }
}
