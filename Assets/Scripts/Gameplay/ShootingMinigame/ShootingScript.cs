using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject TargetPrefab;
    public Texture2D CursorTexture;
    private bool _Initialized = false;
    private List<TargetData> _Targets = new List<TargetData>();
    private MinigameEnablerScript _EnablerScript;
    public GameObject CrossbowObject;
    private CrossbowScript _CrossbowScript;
    private MinigameScoreScript _MinigameScoreScript;
    private float _Timer = 10f;
    private float _TimeElapsedSinceLastTargetSpawn = 0;
    
    private class TargetData
    {
        public TargetScript Script;
        public GameObject GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        _EnablerScript = GetComponent<MinigameEnablerScript>();
        _EnablerScript.EnableMinigame();
        _CrossbowScript = CrossbowObject.GetComponent<CrossbowScript>();
        _MinigameScoreScript = GetComponent<MinigameScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_Initialized && _EnablerScript.MinigameEnabled)
        {
            Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            _Initialized = true;
        }

        if (_EnablerScript.MinigameEnabled)
        {
            _Timer -= Time.deltaTime;
            _TimeElapsedSinceLastTargetSpawn += Time.deltaTime;
            if (_TimeElapsedSinceLastTargetSpawn > 1)
            {
                CreateTarget();
                _TimeElapsedSinceLastTargetSpawn = 0;
            }

            if (_CrossbowScript.ShotThisFrame)
            {
                foreach (TargetData targetData in _Targets)
                {
                    if (targetData.Script.ClickedThisFrame && !targetData.Script.TargetHit)
                    {
                        targetData.Script.HitTarget();
                        _MinigameScoreScript.IncreaseScore(0.25f);
                    }
                }
            }

            if (_Timer <= 0)
            {
                _EnablerScript.DisableMinigame();
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void CreateTarget()
    {
        bool moveRight = Random.Range(0, 2) == 0;
        Vector2 position;
        if (moveRight)
        {
            List<Vector2> leftPositions = new List<Vector2> { new Vector2(-11, 1), new Vector2(-11, 3) };
            position = leftPositions[Random.Range(0, 2)];
        }
        else
        {
            List<Vector2> rightPositions = new List<Vector2> { new Vector2(11, 1), new Vector2(11, 3) };
            position = rightPositions[Random.Range(0, 2)];
        }

        GameObject newTarget = Instantiate(TargetPrefab, new Vector3(position.x, position.y), Quaternion.identity, gameObject.transform);
        TargetScript newTargetScript = newTarget.GetComponent<TargetScript>();
        newTargetScript.MoveRight = moveRight;
        newTargetScript.MoveSpeed = 4 + Random.Range(0f, 4f);
        TargetData newTargetData = new TargetData();
        newTargetData.GameObject = newTarget;
        newTargetData.Script = newTargetScript;
        _Targets.Add(newTargetData);
    }
}
