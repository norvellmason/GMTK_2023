using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameEnablerScript : MonoBehaviour
{
    public bool MinigameEnabled { get; private set; } = false;
    private bool _IsFadingIn = false;
    private bool _IsFadingOut = false;
    private float _ValueToSetAlpha = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_IsFadingIn)
        {
            _ValueToSetAlpha += Time.deltaTime;
            _ValueToSetAlpha = Mathf.Min(_ValueToSetAlpha, 1);
            SetAlpha(_ValueToSetAlpha);
            if (_ValueToSetAlpha == 1)
            {
                _IsFadingIn = false;
            }
        }
        else if (_IsFadingOut)
        {
            _ValueToSetAlpha -= Time.deltaTime;
            _ValueToSetAlpha = Mathf.Max(_ValueToSetAlpha, 0);
            SetAlpha(_ValueToSetAlpha);
            if (_ValueToSetAlpha == 0)
            {
                _IsFadingOut = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void EnableMinigame()
    {
        MinigameEnabled = true;
        _IsFadingIn = true;
        _IsFadingOut = false;
        _ValueToSetAlpha = 0;
        gameObject.SetActive(true);
    }

    public void DisableMinigame()
    {
        MinigameEnabled = false;
        _IsFadingOut = true;
        _IsFadingIn = false;
        _ValueToSetAlpha = 1;
    }

    public void SetAlpha(float alpha)
    {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        Color newColor;
        foreach (SpriteRenderer child in children)
        {
            newColor = child.color;
            newColor.a = alpha;
            child.color = newColor;
        }
    }
}
