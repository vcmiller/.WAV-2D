using UnityEngine;
using SBR;

public abstract class TutBossSM : GroundEnemySMBase {
    public enum StateID {
        Wait, Aggro, Combat
    }

    private class State {
        public StateID id;

        public Notify enter;
        public Notify during;
        public Notify exit;

        public Transition[] transitions;
    }

    private class Transition {
        public State from;
        public State to;

        public Notify notify;
        public Condition cond;
    }

    private State[] states;
    private State currentState;

    public TutBossSM() {
        states = new State[3];

        State stateWait = new State();
        stateWait.id = StateID.Wait;
        stateWait.during = State_Wait;
        stateWait.transitions = new Transition[1];
        states[0] = stateWait;

        State stateAggro = new State();
        stateAggro.id = StateID.Aggro;
        stateAggro.during = State_Aggro;
        stateAggro.transitions = new Transition[1];
        states[1] = stateAggro;

        State stateCombat = new State();
        stateCombat.id = StateID.Combat;
        stateCombat.during = State_Combat;
        stateCombat.transitions = new Transition[0];
        states[2] = stateCombat;

        currentState = stateWait;

        Transition transitionWaitAggro = new Transition();
        transitionWaitAggro.from = stateWait;
        transitionWaitAggro.to = stateAggro;
        transitionWaitAggro.cond = TransitionCond_Wait_Aggro;
        transitionWaitAggro.notify = TransitionNotify_Wait_Aggro;
        stateWait.transitions[0] = transitionWaitAggro;

        Transition transitionAggroCombat = new Transition();
        transitionAggroCombat.from = stateAggro;
        transitionAggroCombat.to = stateCombat;
        transitionAggroCombat.cond = TransitionCond_Aggro_Combat;
        stateAggro.transitions[0] = transitionAggroCombat;


    }

    public StateID state {
        get {
            return currentState.id;
        }

        set {
            foreach (var s in states) {
                if (s.id == value) {
                    TransitionTo(s);
                    return;
                }
            }
        }
    }

    public override string stateName {
        get {
            return state.ToString();
        }

        set {
            try {
                state = (StateID)System.Enum.Parse(typeof(StateID), value);
            } catch (System.ArgumentException) {
                throw new System.ArgumentException("Given string is not a valid state name!");
            }
        }
    }

    public override void Initialize(GameObject obj) {
        base.Initialize(obj);

        CallIfSet(currentState.enter);
    }

    public override void Update() {
        currentState.during();

        State cur = currentState;

        for (int i = 0; i < maxTransitionsPerUpdate; i++) {
            bool found = false;

            foreach (var t in cur.transitions) {
                if (t.cond()) {
                    CallIfSet(t.notify);
                    cur = t.to;
                    found = true;
                }
            }

            if (!found) {
                break;
            }
        }

        if (cur != currentState) {
            TransitionTo(cur);
        }
    }

    private void TransitionTo(State target) {
        CallIfSet(currentState.exit);
        currentState = target;
        CallIfSet(target.enter);
    }

    protected virtual void State_Wait() { }
    protected virtual void State_Aggro() { }
    protected virtual void State_Combat() { }

    protected virtual bool TransitionCond_Wait_Aggro() { return false; }
    protected virtual void TransitionNotify_Wait_Aggro() { }
    protected virtual bool TransitionCond_Aggro_Combat() { return false; }

}
