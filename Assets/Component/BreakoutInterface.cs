using UnityEngine;
using System.Collections;

public class BreakoutInterface : MonoBehaviour
{
    static BreakoutInterface()
    {
        EgoSystems.systems.Add( new BallSystem() );
        EgoSystems.systems.Add( new BrickInstantiationSystem());
        EgoSystems.systems.Add( new BrickSystem() );
        EgoSystems.systems.Add( new PaddleSystem() );
        EgoSystems.systems.Add( new BallSystem() );
        EgoSystems.systems.Add( new ScoreSystem() );
        EgoSystems.systems.Add( new GameEndSystem() );

        // UI
        EgoSystems.systems.Add( new PlayAgainButtonSystem() );
        EgoSystems.systems.Add( new UISystem() );
    }

	void Start()
    {
        EgoSystems.Start();
	}

	void Update()
    {
        EgoSystems.Update();
	}

    void FixedUpdate()
    {
        EgoSystems.FixedUpdate();
    }
}
