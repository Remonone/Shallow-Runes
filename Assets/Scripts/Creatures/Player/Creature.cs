using Components;
using Traits;
using UnityEngine;

namespace Creatures.Player {
    public abstract class Creature : MonoBehaviour {
        [SerializeField] private LevelComponent _levelComponent;
        [SerializeField] private BaseTraits _baseTraits;

        public TraitsContainer Traits { get; private set; }
        public LevelComponent LevelComponent => _levelComponent;
        protected virtual void Awake() {
            Traits = new TraitsContainer(new TraitsMediator(), _baseTraits);
        }
    }
}