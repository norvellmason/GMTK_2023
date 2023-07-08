using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLevelScript : MonoBehaviour
{
    private Transform _BarTransform;
    private SpriteRenderer _BarSpriteRenderer;
    public float PowerLevel { get; private set; }
    private Vector2 _BarStartingPos;
    private float _PowerToAdd = 0;

    // Start is called before the first frame update
    void Start()
    {
        _BarTransform = transform.GetChild(1);
        _BarSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _BarStartingPos = _BarTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_PowerToAdd > 0)
        {
            float powerToAddThisFrame = Time.deltaTime * 1.5f;
            powerToAddThisFrame = Mathf.Min(powerToAddThisFrame, _PowerToAdd);
            _PowerToAdd -= powerToAddThisFrame;
            PowerLevel += powerToAddThisFrame;

            float newXPos = _BarStartingPos.x + (_BarSpriteRenderer.bounds.size.x * 0.5f);
            _BarTransform.position = new Vector2(newXPos, _BarStartingPos.y);
            _BarTransform.localScale = new Vector2(PowerLevel, _BarTransform.localScale.y);
        }
    }

    public void IncreasePowerLevel(float amount)
    {
        _PowerToAdd += amount;
    }
}
