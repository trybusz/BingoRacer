using UnityEngine;

    public abstract class InputController : ScriptableObject {
        public abstract bool RetrieveJumpInputDown();
        public abstract bool RetrieveJumpInput();
        public abstract float RetrieveMoveInput();
        public abstract bool RetrieveDashInput();
        public abstract bool RetrieveDashInputDown();
        public abstract bool RetrieveCheckpointInputDown();
        public abstract bool RetrieveSpawnInputDown();
    public abstract bool ReloadLevelInputDown();
}