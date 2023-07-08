using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameScoreScript : MonoBehaviour
{
    public delegate void IncreaseScoreCallback(float amount);
    private List<IncreaseScoreCallback> _Callbacks = new List<IncreaseScoreCallback>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScoreListener(IncreaseScoreCallback callback)
    {
        _Callbacks.Add(callback);
    }

    public void IncreaseScore(float amount)
    {
        foreach (IncreaseScoreCallback callback in _Callbacks)
        {
            callback(amount);
        }
    }
}
