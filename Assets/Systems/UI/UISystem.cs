using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : EgoSystem<
	EgoConstraint<Canvas, UI>
>{
	public override void Start()
	{
		Reset();

		// Add Event Handler Methods
		EgoEvents<UpdateScoreUIEvent>.AddHandler( Handle );
		EgoEvents<GameEndEvent>.AddHandler( Handle );
		EgoEvents<ResetGameEvent>.AddHandler( Handle );
	}

	// Event Handler Methods

	void Handle( UpdateScoreUIEvent e )
	{
		constraint.ForEachGameObject( ( ego, canvas, ui ) =>
		{
			ui.ScoreText.text = "Score: " + e.newScore.ToString();
		} );
	}

	void Handle( GameEndEvent e )
	{
		constraint.ForEachGameObject( ( ego, canvas, ui ) =>
		 {
			 switch( e.gameEnd )
			 {
			 case GameEnd.LOSE:
				 // Show the Lose Panel
				 ui.LosePanelEgo.gameObject.SetActive( true );
				 break;
			 case GameEnd.WIN:
				 // Show the Win Panel
				 ui.WinPanelEgo.gameObject.SetActive( true );
				 break;
			 }
		 } );
	}

	void Handle( ResetGameEvent e )
	{
		Reset();
	}

	// Helper Methods

	void Reset()
	{
		constraint.ForEachGameObject( ( ego, canvas, ui ) =>
		{
			ui.LosePanelEgo.gameObject.SetActive( false );
			ui.WinPanelEgo.gameObject.SetActive( false );
		} );
	}
}
