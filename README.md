# Akka.FSM.Examples
Examples on how to use Akka FSM actor. FSM stands for Finite State Machine.
For more information use:

Akka [tutorial](http://doc.akka.io/docs/akka/current/scala/fsm.html)

Akka.net API: [docs](http://api.getakka.net/docs/stable/html/27A99810.htm) 


## Akka FSM basics

Akka provides a specialised FSM actor. It's a generic actor with signature ```FSM<TState, TStateData>```. 
The TState type represents the finite states the actor can be in and TStateData is the data that is stored when in a particular state.
To set up and use an FSM actor, code needs to be written to set up:

* the initial state of the FSM 
* the behaviours of the FSM in each state 
* and optionally what happens when a transition occurs

Also timers (i.e. how long to stay in one state before transitioning to another) can be associated with states, but it will not be discussed at this point.


### StartWith

The initial state is set with StartWith. StartWith is a function that takes 2 parameters, the initial state and the associated state data.

### When

Then we need to provide function that represent the response function that the actor will use when in a particular state.
The _When_ methods accepts 3 parameters, The state, a state transition function and a timer.
The state function is a function from an FsmEvent (i.e. a message that the actor receives) to a tuple TState, TStateData.
The framework will use the state function relevant for the current state the FSM actor is in, to perform the state transition when receiving a message.
**The only way for an FSM actor to perform a transition to a different state is by receiving a message**
There are helper methods to create the required return types: 
* _GoTo_ 
* _Stay_

Moreover _Using_ can be added in a fluent fashion to _GoTo_ and _Stay_ to transition to the next state with changed state data.


### OnTransition

Optionally we can separate some logic to tell the FSM what to do when a transition happens.

### Initialize

This function is used to start the FSM and should be the last invoked function at actor construction (or in PreStart) and PostRestart. 
From Akka docs: ... performs the transition into the initial state and setup timers...


### Describe FSM responses whilst in a state

Define state function (i.e. a state that returns a TState, TStateData tuple).
Discuss the *Goto*, *Stay*, *Using* methods.

### Other methods

*WhenUnhandled*,


## What's implemented in this repository

The turnstile FSM described on wikipedia is implemented in this repository.

### Example: coin-operated turnstile


from [wikipedia finite state machine](https://en.wikipedia.org/wiki/Finite-state_machine)

An example of a very simple mechanism that can be modeled by a state machine is a turnstile.
A turnstile, used to control access to subways and amusement park rides, is a gate with three rotating arms at waist height, one across the entryway.  Initially the arms are locked, blocking the entry, preventing patrons from passing through.  Depositing a coin  in a slot on the turnstile unlocks the arms, allowing a single customer to push through.  After the customer passes through, the arms are locked again until another coin is inserted.

Considered as a state machine, the turnstile has two states: **Locked** and **Unlocked**.
There are two inputs that affect its state: putting a coin in the slot (**coin**) and pushing the arm (**push**).  
In the locked state, pushing on the arm has no effect; no matter how many times the input **push** is given, it stays in the locked state.  Putting a coin in – that is, giving the machine a **coin** input – shifts the state from **Locked** to **Unlocked**.  In the unlocked state, putting additional coins in has no effect; that is, giving additional **coin** inputs does not change the state.  However, a customer pushing through the arms, giving a **push** input, shifts the state back to **Locked**.

The turnstile state machine can be represented by a [[state transition table]], showing for each state the new state and the output (action) resulting from each input

| Current State| Input| Next State| Output|
|-----|-----|-----|-----|
|Locked | coin | Unlocked | Unlock turnstile so customer can push through|
|Locked | push | Locked | None|
|Unlocked | coin | Unlocked | None|
|Unlocked | push | Locked | When customer has pushed through, lock turnstile|

### Learning tasks

* Complete the implementation of the state transition table.
* Where should the Initialize call be?
* How do we test the FSM logic? 
* Add transition logic to log to file the events and state data.
* Implement timers so that the turnstile becomes locked again after 30 secs.
