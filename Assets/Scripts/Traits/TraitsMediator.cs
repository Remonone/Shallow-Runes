using System;
using System.Collections.Generic;
using Traits.Modifiers;

namespace Traits {
    public class TraitsMediator {
        private readonly LinkedList<TraitModifier> _modifiers = new();

        public event EventHandler<Query> Queries;
        
        internal void PerformQuery(object sender, Query query) => Queries?.Invoke(sender, query);

        public void AddModifier(TraitModifier modifier) {
            _modifiers.AddLast(modifier);
            modifier.MarkedForRemoval = false;
            Queries += modifier.Handle;

            modifier.OnDispose += _ => {
                _modifiers.Remove(modifier);
                Queries -= modifier.Handle;
            };
        }

        public void Update(float delta) {
            var node = _modifiers.First;
            while (node != null) {
                var modifier = node.Value;
                modifier.Update(delta);
                node = node.Next;
            }

            node = _modifiers.First;
            while (node != null) {
                var nextNode = node.Next;

                if (node.Value.MarkedForRemoval) {
                    node.Value.Dispose();
                }
                
                node = nextNode;
            }
        }
    }
    
    public class Query {
        public readonly Trait Type;
        public float Value;

        public Query(Trait type, float value) {
            Type = type;
            Value = value;
        }
    }
}