using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject TargetPrefab1;
    public GameObject TargetPrefab2;
    public Texture2D CursorTexture;
    private bool _Initialized = false;
    private bool _HardMode = false;
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
            Cursor.SetCursor(CursorTexture, new Vector2(64, 64), CursorMode.ForceSoftware);
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
                _HardMode = true;
                _Initialized = false;
                _Timer = 10;
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
        float flipModifier = 1;
        if (moveRight)
        {
            List<Vector2> leftPositions = new List<Vector2> { new Vector2(-11, 0.9f), new Vector2(-11, -1) };
            position = leftPositions[Random.Range(0, 2)];
            flipModifier = -1;
        }
        else
        {
            List<Vector2> rightPositions = new List<Vector2> { new Vector2(11, 0.9f), new Vector2(11, -1) };
            position = rightPositions[Random.Range(0, 2)];
            flipModifier = 1;
        }

        GameObject prefabToUse = Random.Range(0, 2) == 0 ? TargetPrefab1 : TargetPrefab2;

        GameObject newTarget = Instantiate(prefabToUse, new Vector3(position.x, position.y, -1), Quaternion.identity, gameObject.transform);
        newTarget.transform.localScale = new Vector3(newTarget.transform.localScale.x * flipModifier, newTarget.transform.localScale.y, newTarget.transform.localScale.z);
        TargetScript newTargetScript = newTarget.GetComponent<TargetScript>();
        newTargetScript.MoveRight = moveRight;
        newTargetScript.MoveSpeed = 4 + Random.Range(0f, 4f);
        if (_HardMode)
        {
            newTargetScript.MoveSpeed = 8 + Random.Range(0f, 6f);
        }
        TargetData newTargetData = new TargetData();
        newTargetData.GameObject = newTarget;
        newTargetData.Script = newTargetScript;
        _Targets.Add(newTargetData);
    }
}
