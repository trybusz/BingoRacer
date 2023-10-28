using UnityEngine;

public class GetCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
