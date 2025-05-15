using System;

namespace Traits.Scales {
    [Serializable]
    public abstract class Scale {
        public Scale() {
            
        }
        public abstract float GetScaleValue(float initialValue, float scale, int step);
    }
}