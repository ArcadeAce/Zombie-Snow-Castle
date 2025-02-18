using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScriptForTitles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroygameObject", 10f);
    }

    // Update is called once per frame
    private void DestroygameObject()
    {
        Destroy(this.gameObject);
    }
}
