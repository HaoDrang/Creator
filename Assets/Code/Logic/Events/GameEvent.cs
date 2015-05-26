
public enum GameEvent
{
	GameStart 		= 0,
	GameEnd 		= 1,
	GamePause		= 2,

	StartBeginner	= 3,
	StartEasy		= 4,
	StartNormal		= 5,
	StartHard		= 6,
	StartCrazy		= 7,
	StartOverlord	= 8,
	StartDemigod	= 9,

	EndBeginner		= 10,
	EndEasy			= 11,
	EndNormal		= 12,
	EndHard			= 13,
	EndCrazy		= 14,
	EndOverlord		= 15,
	EndDemigod		= 16,

	BallShoot		= 17,
	BallCollide		= 18,
	BallConsume		= 19,
	BallDrop		= 20,

	RoundBegin		= 21,
	RoundEnd		= 22,
}