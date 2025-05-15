

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

        public float Health(int level) {
            var baseHealth = _traits.GetValueByTrait<MultiplicationScale>(Trait.HEALTH, level);
            Query query = new Query(Trait.HEALTH, baseHealth);
            Mediator.PerformQuery(this, query);
            return query.Value;
        }

        public float HealthEfficiency(int level) {
            var baseEfficiency = _traits.GetValueByTrait<PlainScale>(Trait.HEALTH_EFFICIENCY, level);
            Query query = new Query(Trait.HEALTH_EFFICIENCY, baseEfficiency);
            Mediator.PerformQuery(this, query);
            return query.Value;
        }

        public float Speed(int level) {
            var baseEfficiency = _traits.GetValueByTrait<PlainScale>(Trait.MOVE_SPEED, level);
            Query query = new Query(Trait.MOVE_SPEED, baseEfficiency);
            Mediator.PerformQuery(this, query);
            return query.Value;
        }

        public float JumpForce(int level) {
            var baseEfficiency = _traits.GetValueByTrait<PlainScale>(Trait.JUMP_FORCE, level);
            Query query = new Query(Trait.JUMP_FORCE, baseEfficiency);
            Mediator.PerformQuery(this, query);
            return query.Value;
        }

        public float RunningSpeed(int level) {
            var baseEfficiency = _traits.GetValueByTrait<PlainScale>(Trait.RUNNING_SPEED, level);
            Query query = new Query(Trait.RUNNING_SPEED, baseEfficiency);
            Mediator.PerformQuery(this, query);
            return query.Value;
        }

        public float CrouchingSpeed(int level) {
            var baseEfficiency = _traits.GetValueByTrait<PlainScale>(Trait.CROUCHING_SPEED, level);
            Query query = new Query(Trait.CROUCHING_SPEED, baseEfficiency);
            Mediator.PerformQuery(this, query);
            return query.Value;
        }
    }
}