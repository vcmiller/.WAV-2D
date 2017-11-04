using UnityEngine;
using SBR;

public abstract class DishSM : GroundEnemySMBase {
    public enum StateID {
        Attack, Move
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

    public DishSM() {
        states = new State[2];

        State stateAttack = new State();
        stateAttack.id = StateID.Attack;
        stateAttack.during = State_Attack;
        stateAttack.exit = StateExit_Attack;
        stateAttack.transitions = new Transition[1];
        states[0] = stateAttack;

        State stateMove = new State();
        stateMove.id = StateID.Move;
        stateMove.during = State_Move;
        stateMove.transitions = new Transition[1];
        states[1] = stateMove;

        currentState = stateMove;

        Transition transitionAttackMove = new Transition();
        transitionAttackMove.from = stateAttack;
        transitionAttackMove.to = stateMove;
        transitionAttackMove.cond = TransitionCond_Attack_Move;
        stateAttack.transitions[0] = transitionAttackMove;

        Transition transitionMoveAttack = new Transition();
        transitionMoveAttack.from = stateMove;
        transitionMoveAttack.to = stateAttack;
        transitionMoveAttack.cond = TransitionCond_Move_Attack;
        stateMove.transitions[0] = transitionMoveAttack;


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

    protected virtual void State_Attack() { }
    protected virtual void StateExit_Attack() { }
    protected virtual void State_Move() { }

    protected virtual bool TransitionCond_Attack_Move() { return false; }
    protected virtual bool TransitionCond_Move_Attack() { return false; }

}
