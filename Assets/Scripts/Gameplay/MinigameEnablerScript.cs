using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameEnablerScript : MonoBehaviour
{
    public bool MinigameEnabled { get; private set; } = false;
    private bool _IsFadingIn = false;
    public bool IsFadingOut { get; private set; } = false;
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
            _ValueToSetAlpha += Time.deltaTime * 0.5f;
            _ValueToSetAlpha = Mathf.Min(_ValueToSetAlpha, 1);
            SetAlpha(_ValueToSetAlpha);
            if (_ValueToSetAlpha == 1)
            {
                _IsFadingIn = false;
                MinigameEnabled = true;
            }
        }
        else if (IsFadingOut)
        {
            _ValueToSetAlpha -= Time.deltaTime * 0.5f;
            _ValueToSetAlpha = Mathf.Max(_ValueToSetAlpha, 0);
            SetAlpha(_ValueToSetAlpha);
            if (_ValueToSetAlpha == 0)
            {
                IsFadingOut = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void EnableMinigame()
    {
        _IsFadingIn = true;
        IsFadingOut = false;
        _ValueToSetAlpha = 0;
        gameObject.SetActive(true);
    }

    public void DisableMinigame()
    {
        MinigameEnabled = false;
        IsFadingOut = true;
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
        TMP_Text[] textChildren = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in textChildren)
        {
            newColor = text.color;
            newColor.a = alpha;
            text.color = newColor;
        }
    }
}
