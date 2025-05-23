using UnityEngine;

namespace Components {
    public class PositionState {
        private LayerMask _groundMask;
        
        private RaycastHit _hit;

        public bool Grounded { get; private set; }
        public Vector3 SlopeNormal { get; private set; }
        public bool OnSlope { get; private set; }

        public PositionState(LayerMask mask) {
            _groundMask = mask;
        }
        
        public bool Running { get; set; }


        public float PlayerHeight { get; private set; }
        

        public void UpdateState(Vector3 position) {
            Grounded = Physics.Raycast(position, Vector3.down, PlayerHeight + .2F, _groundMask);
            OnSlope = CheckOnSlope(position);
            SlopeNormal = OnSlope ? _hit.normal : Vector3.zero;
        }

        private bool CheckOnSlope(Vector3 position) {
            if (Physics.Raycast(position, Vector3.down, out _hit, PlayerHeight + .3f)) {
                float maxSlopeAngle = 53F;
                float angle = Vector3.Angle(Vector3.up, _hit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        public void UpdatePlayerHeight(Vector3 position) {
            Physics.Raycast(position, Vector3.down, out var hit, 10F, _groundMask);
            PlayerHeight = hit.distance;
        }
    }
}