using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController {
        public override bool RetrieveJumpInputDown() {
            return Input.GetButtonDown("Jump");
        }

        public override bool RetrieveJumpInput() {
            return Input.GetButton("Jump");
        }

        public override float RetrieveMoveInput() {
            return Input.GetAxisRaw("Horizontal");
        }

        public override bool RetrieveDashInput() {
            return Input.GetKey(KeyCode.S);
        }

        public override bool RetrieveDashInputDown() {
            return Input.GetKeyDown(KeyCode.S);
        }

        public override bool RetrieveCheckpointInputDown() {
            return Input.GetKeyDown(KeyCode.T);
        }
        public override bool RetrieveSpawnInputDown() {
            return Input.GetKeyDown(KeyCode.Y);
        }
    public override bool ReloadLevelInputDown() {
        return Input.GetKeyDown(KeyCode.P);
    }
}