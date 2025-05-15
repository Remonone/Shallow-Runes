namespace Traits.Scales {
    public class PlainScale : Scale {
        public override float GetScaleValue(float initialValue, float scale, int step) {
            return initialValue + scale * step;
        }
    }
}