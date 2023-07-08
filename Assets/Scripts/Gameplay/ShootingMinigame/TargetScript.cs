using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public bool ClickedThisFrame { get; private set; }
    public bool TargetHit { get; private set; } = false;
    public bool MoveRight = true;
    public float MoveSpeed = 1f;
    private SpriteRenderer _TargetSpriteRenderer;
    private SpriteRenderer _TargetHitSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _TargetSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _TargetSpriteRenderer.enabled = true;
        _TargetHitSpriteRenderer = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        _TargetHitSpriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xDelta = Time.deltaTime * MoveSpeed;
        if (!MoveRight)
        {
            xDelta *= -1;
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x + xDelta, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickedThisFrame = true;
        }
    }

    public void HitTarget()
    {
        _TargetSpriteRenderer.enabled = false;
        _TargetHitSpriteRenderer.enabled = true;
        TargetHit = true;
    }
}
