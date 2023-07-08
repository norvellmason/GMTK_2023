using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PumpingIronScript : MonoBehaviour
{
    private GameObject _ForearmObject;
    private Vector3 _PointToRotateAround = new Vector3(0, -1.5f, 0);
    private float _ArmAngle = 0;
    private MinigameScoreScript _MinigameScoreScript;
    private float _Timer = 10;
    private MinigameEnablerScript _EnablerScript;

    // Start is called before the first frame update
    void Start()
    {
        _ForearmObject = transform.GetChild(2).gameObject;
        _MinigameScoreScript = GetComponent<MinigameScoreScript>();
        _EnablerScript = GetComponent<MinigameEnablerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_EnablerScript.MinigameEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _ForearmObject.transform.RotateAround(_PointToRotateAround, Vector3.back, 5);
                _ArmAngle += 5;
            }

            _ForearmObject.transform.RotateAround(_PointToRotateAround, Vector3.back, -5 * Time.deltaTime);
            _ArmAngle += -5 * Time.deltaTime;

            if (_ArmAngle > 90)
            {
                _ForearmObject.transform.RotateAround(_PointToRotateAround, Vector3.back, -_ArmAngle);
                _ArmAngle = 0;
                _MinigameScoreScript.IncreaseScore(1);
            }
            if (_ArmAngle < 0)
            {
                _ForearmObject.transform.RotateAround(_PointToRotateAround, Vector3.back, -_ArmAngle);
                _ArmAngle = 0;
            }

            _Timer -= Time.deltaTime;
            if (_Timer < 0)
            {
                _EnablerScript.DisableMinigame();
            }
        }
    }
}
