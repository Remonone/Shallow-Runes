using System;
using System.Collections.Generic;
using System.Linq;
using Traits.Scales;
using UnityEngine;

namespace Traits {
    [CreateAssetMenu(menuName = "Shallow Runes/Data/Create Trait Data")]
    public class BaseTraits : ScriptableObject {

        [SerializeField] private List<TraitRow> _rows;

        public float GetValueByTrait<T>(Trait trait, int level) where T : Scale, new() {
            var traitRow = _rows.SingleOrDefault(row => row.Trait.Equals(trait));
            T scale = new T();
            if (traitRow is null) return 0;
            return scale.GetScaleValue(traitRow.Value, traitRow.LevelScale, level);
        }

        [Serializable]
        private class TraitRow {
            public Trait Trait;
            public float Value;
            public float LevelScale;
        }
    }
}