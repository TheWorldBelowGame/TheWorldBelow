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
	
	// End the current state and transition to a new state.
	// newState = The state to transition to.
	public void ChangeState(State newState)
	{
		if (currentState != null)
		{
			currentState.OnFinish();
		}
		
		currentState = newState;
		currentState.ConcludeState = this.Reset;
		currentState.OnStart();
	}
	
	// Reset the state machine to the default state.
	public void Reset()
	{
		if (currentState != null)
			currentState.OnFinish();
		currentState = defaultState;
	}
	
	// This should be called every Update tick.
	public void Update()
	{
		if (currentState != null)
		{
			currentState.OnUpdate();
		}
	}
	
	// Is the state machine doing anything?
	// returns true if the state machine is currently in the default state.
	public bool IsDefault()
	{
		return currentState == defaultState;
	}
}

// A State is merely a bundle of behavior listening to specific events, such as...
// OnUpdate -- Fired every frame of the game.
// OnStart -- Fired once when the state is transitioned to.
// OnFinish -- Fired as the state concludes.
// State Constructors often store data that will be used during the execution of the State.
public abstract class State
{
	// States may call ConcludeState() to end their processing.
	public Action ConcludeState;

	public abstract void OnStart();
	public abstract void OnUpdate();
	public abstract void OnFinish();
}