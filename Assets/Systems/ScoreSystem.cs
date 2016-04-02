using UnityEngine;
using System.Collections;

public class ScoreSystem : EgoSystem<Score>
{
    public override void Start()
    {
        EgoEvents<IncreaseScoreEvent>.AddHandler(Handle);
        EgoEvents<ResetGameEvent>.AddHandler(Handle);
    }

    // Event Handler Methods

    void Handle( IncreaseScoreEvent e )
    {
        foreach( var bundle in bundles )
        {
            var score = bundle.component1;
            SetScore( score, score.score + e.increment );
            break; // Only update one Score Component, avoids singletons
        }
    }

    void Handle( ResetGameEvent e )
    {
        foreach (var bundle in bundles)
        {
            var score = bundle.component1;
            SetScore( score, 0 );
            break; // Only update one Score Component, avoids singletons
        }
    }

    // Helper Methods

    void SetScore( Score score, int newScore )
    {
        score.score = newScore;
        EgoEvents<UpdateScoreUIEvent>.AddEvent( new UpdateScoreUIEvent( score.score ) );
    }
}
