

using Traits.Scales;

namespace Traits {
    public class TraitsContainer {
        private BaseTraits _traits;
        private TraitsMediator _mediator;

        public TraitsMediator Mediator => _mediator;
        
        public TraitsContainer(TraitsMediator mediator, BaseTraits traits) {
            _mediator = mediator;
            _traits = traits;
        }

        public float Health(int level) => GetEndValue<MultiplicationScale>(Trait.HEALTH, level);

        public float HealEfficiency(int level) => GetEndValue<PlainScale>(Trait.HEAL_EFFICIENCY, level);

        public float Speed(int level) => GetEndValue<PlainScale>(Trait.MOVE_SPEED, level);

        public float JumpForce(int level) => GetEndValue<PlainScale>(Trait.JUMP_FORCE, level);

        public float RunningSpeed(int level) => GetEndValue<PlainScale>(Trait.RUNNING_SPEED, level);

        public float AttackDamage(int level) => GetEndValue<PlainScale>(Trait.ATTACK_DAMAGE, level);

        public float AttackShotSpeed(int level) => GetEndValue<PlainScale>(Trait.ATTACK_SHOT_SPEED, level);

        public float AttackSpeed(int level) => GetEndValue<MultiplicationScale>(Trait.ATTACK_SPEED, level);

        public float AttackRange(int level) => GetEndValue<PlainScale>(Trait.ATTACK_RANGE, level);

        private float GetEndValue<T>(Trait trait, int level) where T : Scale, new() {
            var baseEfficiency = _traits.GetValueByTrait<T>(trait, level);
            Query query = new Query(trait, baseEfficiency);
            Mediator.PerformQuery(this, query);
            return query.Value;
        }
    }
}