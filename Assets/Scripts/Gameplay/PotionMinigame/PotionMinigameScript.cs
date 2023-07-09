using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMinigameScript : MonoBehaviour
{
    private bool _Initialized = false;
    private MinigameEnablerScript _EnablerScript;
    private MinigameScoreScript _ScoreScript;
    private Transform _PotionTransform;
    private Vector3 _PositionLastFrame = Vector3.zero;
    private float _Timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _EnablerScript = GetComponent<MinigameEnablerScript>();
        _ScoreScript = GetComponent<MinigameScoreScript>();
        _PotionTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_Initialized && _EnablerScript.MinigameEnabled)
        {
            Cursor.visible = false;
            _Initialized = true;
        }

        if (_EnablerScript.MinigameEnabled)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            newPosition.x = Mathf.Min(newPosition.x, -1);
            newPosition.x = Mathf.Max(newPosition.x, -8);
            newPosition.y = Mathf.Min(newPosition.y, 4);
            newPosition.y = Mathf.Max(newPosition.y, -4);
            newPosition.x -= 3;
            _PotionTransform.position = newPosition;

            float pointsToScore = Vector3.Distance(_PotionTransform.position, _PositionLastFrame) * 0.005f;
            _ScoreScript.IncreaseScore(pointsToScore);
            _PositionLastFrame = _PotionTransform.position;

            _Timer -= Time.deltaTime;
            if (_Timer < 0)
            {
                _EnablerScript.DisableMinigame();
            }
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
