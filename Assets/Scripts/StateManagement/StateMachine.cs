﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace StateManagement
{
    public class StateMachine<T> where T : class
    {
        private readonly IState _anyState;
        private readonly T _context;

        private readonly Dictionary<Type, IState> _stateMap = new();
        private readonly Dictionary<IState, List<Transition<T>>> _transitionMap = new();

        private IState _currentState;

        public StateMachine(T context)
        {
            _context = context;
            _anyState = new AnyState();
            _stateMap.Add(typeof(AnyState), _anyState);
        }

        public void AddState<TState>() where TState : State<T>, new()
        {
            var state = new TState();
            AddState(state);
        }

        public void AddState(State<T> state)
        {
            state.Context = _context;
            _stateMap.Add(state.GetType(), state);
        }

        public void AddTransition<TFromState, TToState>(Condition<T> condition)
        {
            Type fromStateType = typeof(TFromState);
            Type toStateType = typeof(TToState);

            if (!_stateMap.TryGetValue(fromStateType, out IState fromState))
                return;

            if (!_stateMap.TryGetValue(toStateType, out IState toState))
                return;

            AddTransition(fromState, toState, condition);
        }

        public void AddTransition(IState from, IState to, Condition<T> condition)
        {
            AddTransition(new Transition<T>(from, to, condition));
        }

        public void AddTransition(Transition<T> transition)
        {
            if (_transitionMap.ContainsKey(transition.From))
            {
                _transitionMap[transition.From].Add(transition);
            }
            else
            {
                var transitions = new List<Transition<T>> {transition};
                _transitionMap.Add(transition.From, transitions);
            }
        }

        public void Update(float deltaTime)
        {
            _currentState?.Update(deltaTime);
            if (_currentState != null) 
                Debug.Log($"Update {_currentState.GetType().Name}");
        }

        public void CheckTransitions()
        {
            Transition<T> result = null;
            result = FirstCanExecuteTransition(_anyState);

            if (result == null)
                FirstCanExecuteTransition(_currentState);

            if (result == null)
                return;

            Enter(result.To);
        }

        private Transition<T> FirstCanExecuteTransition(IState state)
        {
            if (state == null)
                return null;

            if (!_transitionMap.TryGetValue(state, out List<Transition<T>> transitions))
                return null;

            return transitions.FirstOrDefault(transition => transition.Statement(_context));
        }

        private void Enter(IState state)
        {
            _currentState?.Exit();
            _currentState = state;
            state.Enter();
        }
    }
}