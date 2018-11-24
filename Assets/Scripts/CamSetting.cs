using UnityEngine;

public class CamSetting : MonoBehaviour
{
	void Awake ()
    {
        GetComponent<Camera>().aspect = 9f / 16f;
    }
}
