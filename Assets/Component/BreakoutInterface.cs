using UnityEngine;
using System.Collections;

public class BreakoutInterface : MonoBehaviour
{
    static BreakoutInterface()
    {
        EgoSystems.Add(
            new BallSystem(),
            new BrickInstantiationSystem(),
            new BrickSystem(),
            new PaddleSystem(),
            new BallSystem(),
            new ScoreSystem(),
            new GameEndSystem(),

            new PlayAgainButtonSystem(),
            new UISystem()
        );
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
