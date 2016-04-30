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
        ForEachGameObject( (EgoComponent ego, Score score ) =>
        {
            SetScore( score, score.score + e.increment );
        } );
    }

    void Handle( ResetGameEvent e )
    {
        ForEachGameObject( (EgoComponent ego, Score score ) =>
        {
            SetScore( score, 0 );
        } );
    }

    // Helper Methods

    void SetScore( Score score, int newScore )
    {
        score.score = newScore;
        EgoEvents<UpdateScoreUIEvent>.AddEvent( new UpdateScoreUIEvent( score.score ) );
    }
}
