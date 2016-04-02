using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : EgoSystem<Canvas, UI>
{
    public override void Start()
    {
        Reset();

        // Add Event Handler Methods
        EgoEvents<UpdateScoreUIEvent>.AddHandler(Handle);
        EgoEvents<GameEndEvent>.AddHandler(Handle);
        EgoEvents<ResetGameEvent>.AddHandler(Handle);
    }

    // Event Handler Methods

    void Handle( UpdateScoreUIEvent e )
    {
        foreach (var bundle in bundles)
        {
            var scoreText = bundle.component2.ScoreText;
            scoreText.text = "Score: " + e.newScore.ToString();
        }
    }

    void Handle( GameEndEvent e )
    {
        foreach (var bundle in bundles)
        {
            switch( e.gameEnd )
            {
            case GameEnd.LOSE:
                // Show the Lose Panel
                var losePanelEgo = bundle.component2.LosePanelEgo;
                losePanelEgo.gameObject.SetActive( true );
                break;
            case GameEnd.WIN:
                // Show the Win Panel
                var winPanelEgo = bundle.component2.WinPanelEgo;
                winPanelEgo.gameObject.SetActive( true );
                break;
            }

            break;
        }
    }

    void Handle( ResetGameEvent e )
    {
        Reset();
    }

    // Helper Methods

    void Reset()
    {
        foreach (var bundle in bundles)
        {
            var losePanelEgo = bundle.component2.LosePanelEgo;
            var winPanelEgo = bundle.component2.WinPanelEgo;

            // Disable Everything
            losePanelEgo.gameObject.SetActive( false );
            winPanelEgo.gameObject.SetActive( false );

            break;
        }
    }
}
