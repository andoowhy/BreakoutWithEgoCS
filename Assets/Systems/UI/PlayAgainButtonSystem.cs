using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayAgainButtonSystem : EgoSystem<Button, UIPlayAgain>
{
    public override void Start()
    {
        foreach (var bundle in bundles)
        {
            var button = bundle.component1;
            button.onClick.AddListener( () =>
            {
                EgoEvents<ResetGameEvent>.AddEvent( new ResetGameEvent() );
            });
        }
    }
}
