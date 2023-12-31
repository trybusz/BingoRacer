using UnityEngine;

    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController {
        public override bool RetrieveJumpInputDown() {
            return true;
        }

        public override bool RetrieveJumpInput() {
            return true;
        }

        public override float RetrieveMoveInput() {
            return 0f;
        }

        public override bool RetrieveDashInput() {
            return true;
        }

        public override bool RetrieveDashInputDown() {
            return true;
        }

        public override bool RetrieveCheckpointInputDown() {
            return true;
        }
        public override bool RetrieveSpawnInputDown() {
            return true;
        }
    public override bool ReloadLevelInputDown() {
        return true;
    }
}