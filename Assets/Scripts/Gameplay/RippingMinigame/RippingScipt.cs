using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippingScipt : MonoBehaviour
{
    private bool _ShouldPressLeftNext = true;
    private MinigameScoreScript _ScoreScript;
    private MinigameEnablerScript _EnableScript;
    private Transform _LeftTransform;
    private Transform _RightTransform;
    private float _PercentRipped = 0;
    private float _Timer = 10;

    // Start is called before the first frame update
    void Start()
    {
        _ScoreScript = GetComponent<MinigameScoreScript>();
        _EnableScript = GetComponent<MinigameEnablerScript>();
        _LeftTransform = transform.GetChild(0).transform;
        _RightTransform = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_EnableScript.MinigameEnabled)
        {
            if (_PercentRipped < 1)
            {
                if (_ShouldPressLeftNext)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Vector3 newPosition = _LeftTransform.position;
                        newPosition.x -= 0.006f;
                        _LeftTransform.position = newPosition;
                        _LeftTransform.Rotate(0, 0, 0.3f);
                        _ShouldPressLeftNext = false;
                        _PercentRipped += 0.01f;
                        _ScoreScript.IncreaseScore(0.07f);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Vector3 newPosition = _RightTransform.position;
                        newPosition.x += 0.006f;
                        _RightTransform.position = newPosition;
                        _RightTransform.Rotate(0, 0, -0.3f);
                        _ShouldPressLeftNext = true;
                        _PercentRipped += 0.01f;
                        _ScoreScript.IncreaseScore(0.07f);
                    }
                }
            }

            _Timer -= Time.deltaTime;
            if (_Timer < 0)
            {
                _EnableScript.DisableMinigame();
            }
        }
    }
}
