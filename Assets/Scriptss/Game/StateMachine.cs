using System;

// A State Machine class to control state transitions.
public class StateMachine<T> where T : State
{
	private State currentState;
	private State defaultState;
	
	// defaultState = The machine will switch to this state whenever another state ends.
	public StateMachine(State defaultState)
	{
		this.defaultState = defaultState;
	}
	
	public Type GetCurrentState()
	{
		return currentState.GetType();
	}
	
	// End the current state and transition to a new state.
	// newState = The state to transition to.
	public void ChangeState(State newState)
	{
		if (currentState != null)
		{
			currentState.Finish();
		}
		
		if (newState == null) {
			newState = defaultState;
		}

		currentState = newState;
		currentState.Transition = this.ChangeState;
		currentState.Start();
	}
	
	// Reset the state machine to the default state.
	public void Reset()
	{
		ChangeState(defaultState);
	}
	
	// This should be called every Update tick.
	public void Update()
	{
		if (currentState != null)
		{
			currentState.CheckState();
			currentState.Update();
		}
	}
}

// A State is merely a bundle of behavior listening to specific events, such as...
// OnUpdate -- Fired every frame of the game.
// OnStart -- Fired once when the state is transitioned to.
// OnFinish -- Fired as the state concludes.
// State Constructors often store data that will be used during the execution of the State.
public abstract class State
{
	// States may call Transition() to transition to a new state. If the argument is null, this will transition to the default state
	public Action<State> Transition;

	public abstract void Start();
	// CheckState is used to transition to different states
	public abstract void CheckState();
	public abstract void Update();
	public abstract void Finish();
}