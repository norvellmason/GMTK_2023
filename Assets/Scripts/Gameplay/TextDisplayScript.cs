using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplayScript : MonoBehaviour
{
    private MinigameEnablerScript _EnableScript;
    private float _Timer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _EnableScript = GetComponent<MinigameEnablerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_EnableScript.MinigameEnabled)
        {
            _Timer -= Time.deltaTime;
            if (_Timer < 0)
            {
                _EnableScript.DisableMinigame();
            }
        }
    }
}
