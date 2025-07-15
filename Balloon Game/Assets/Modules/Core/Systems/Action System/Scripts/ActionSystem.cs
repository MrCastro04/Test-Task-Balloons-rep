using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Core.Utility.Singleton;
using Modules.New;
using UnityEngine;

namespace Modules.Core.Systems.Action_System.Scripts
{
    public class ActionSystem : Singleton<ActionSystem>
    {
        private List<GameAction> _reactions = new();

        private static Dictionary<Type, List<Action<GameAction>>> _preSubs = new(); 
        static Dictionary<Type, List<Action<GameAction>>> _postSubs = new(); 

        private static Dictionary<Type, Func<GameAction, IEnumerator>> _performers = new(); 

        public bool IsPerforming { get; private set; } = false;

        public void Perform(GameAction action, Action callback = null)
        {
            if (IsPerforming) return;

            IsPerforming = true;

            StartCoroutine(Flow(action, () =>
            {
                IsPerforming = false;
                callback?.Invoke();
            }));
        }

        public void AddReaction(GameAction newReaction)
        {
            _reactions?.Add(newReaction);
        }

        public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
        {
            Type type = typeof(T);

            IEnumerator wrappedPerformer(GameAction action) => performer((T) action); 

            if (_performers.ContainsKey(type))
            {
                _performers[type] = wrappedPerformer; 
            }

            else
            {
                _performers.Add(type, wrappedPerformer);
            }
        }

        public static void DetachPerformer<T>() where T : GameAction
        {
            Type type = typeof(T);

            if (_performers.ContainsKey(type))
            {
                _performers.Remove(type);
            }
        }

        public static void SubscribeReaction <T> (Action<T> reaction, ReactionTiming timing) where T : GameAction
        {
            Dictionary<Type, List<Action<GameAction>>> sub = timing == ReactionTiming.PRE ? _preSubs : _postSubs;

            Type type = typeof(T);

            void wrappedReaction(GameAction action)
            {
                reaction((T) action);
            }

            if (sub.ContainsKey(type))
            {
                sub[type].Add(wrappedReaction);
            }

            else
            {
                sub.Add(type, new());

                sub[type].Add(wrappedReaction);
            }
        }

        public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
        {
            Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? _preSubs : _postSubs;

            Type type = typeof(T);

            if (subs.ContainsKey(type))
            {
                void wrappedReaction(GameAction action)
                {
                    reaction((T) action);
                }

                subs[type].Remove(wrappedReaction);
            }
        }

        private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
        {
            _reactions = action.PreReactions;
            PerformSubscribers(action, _preSubs); 
            yield return PerformReactions();

            _reactions = action.PerformReactions;
            yield return PerformPerformer(action); 
            yield return PerformReactions();

            _reactions = action.PostReactions;
            PerformSubscribers(action, _postSubs);
            yield return PerformReactions();

            OnFlowFinished?.Invoke();
        }

        private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
        {
            Type type = action.GetType();

            if (subs.ContainsKey(type))
            {
                foreach (var sub in subs[type])
                {
                    sub(action);
                }
            }
        }

        private IEnumerator PerformReactions()
        {
            foreach (var reaction in _reactions)
            {
                yield return Flow(reaction);
            }
        }

        private IEnumerator PerformPerformer(GameAction action) 
        {
            Type type = action.GetType();

            if (_performers.ContainsKey(type))
            {
                yield return _performers[type](action);
            }
        }
    }
}

