using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureClickedScript : MonoBehaviour
{
    private List<Action> _OnClickListeners = new List<Action>();
    private List<Action> _OnReleaseListeners = new List<Action>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOnClickListener(Action action)
    {
        _OnClickListeners.Add(action);
    }

    public void AddOnReleaseListener(Action action)
    {
        _OnReleaseListeners.Add(action);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Action action in _OnClickListeners)
            {
                action();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            foreach (Action action in _OnReleaseListeners)
            {
                action();
            }
        }
    }
}
