using UnityEngine;
using System.Collections;

public class ScoreSystem : EgoSystem<
	EgoConstraint<Score>
>{
	public override void Start()
	{
		EgoEvents<IncreaseScoreEvent>.AddHandler( Handle );
		EgoEvents<ResetGameEvent>.AddHandler( Handle );
	}

	// Event Handler Methods

	void Handle( IncreaseScoreEvent e )
	{
		constraint.ForEachGameObject( ( ego, score ) =>
		{
			SetScore( score, score.score + e.increment );
		} );
	}

	void Handle( ResetGameEvent e )
	{
		constraint.ForEachGameObject( ( ego, score ) =>
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
