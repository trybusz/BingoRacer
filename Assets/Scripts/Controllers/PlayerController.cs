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
    }