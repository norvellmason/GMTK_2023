using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrossbowScript : MonoBehaviour
{
    public bool ShotThisFrame { get; private set; }
    private bool _IsOnCooldown = false;
    private float _CooldownTimer;

    private SpriteRenderer _CrossbowDrawnSprite;
    private SpriteRenderer _CrossbowFiredSprite;

    // Start is called before the first frame update
    void Start()
    {
        _CrossbowDrawnSprite = this.transform.GetChild(0).transform.gameObject.GetComponent<SpriteRenderer>();
        _CrossbowFiredSprite = this.transform.GetChild(1).transform.gameObject.GetComponent<SpriteRenderer>();
        _CrossbowDrawnSprite.enabled = true;
        _CrossbowFiredSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool shot = false;
        if (Input.GetMouseButtonDown(0))
        {
            if (!_IsOnCooldown)
            {
                ShotThisFrame = true;
                _IsOnCooldown = true;
                _CooldownTimer = 0.25f;
                _CrossbowDrawnSprite.enabled = false;
                _CrossbowFiredSprite.enabled = true;
                shot = true;
            }
        }
        
        if (!shot)
        {
            ShotThisFrame = false;
        }

        if (_IsOnCooldown)
        {
            _CooldownTimer -= Time.deltaTime;
            if (_CooldownTimer <= 0)
            {
                _CooldownTimer = 0;
                _IsOnCooldown = false;
                _CrossbowDrawnSprite.enabled = true;
                _CrossbowFiredSprite.enabled = false;
            }
        }
    }
}
