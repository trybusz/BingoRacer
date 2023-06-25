using UnityEngine;

    public abstract class InputController : ScriptableObject {
        public abstract float RetrieveMoveInput();
        public abstract bool RetrieveJumpInputDown();
        public abstract bool RetrieveJumpInput();
    }