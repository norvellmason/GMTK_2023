using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSceneScript : MonoBehaviour
{
    public ParticleSystem PlayButtonParticleSystem;
    private float _StartTimer = 1f;
    private bool _PressedPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_PressedPlay)
        {
            _StartTimer -= Time.deltaTime;
            if (_StartTimer < 0)
            {
                SceneManager.LoadScene("GameplayScene");
            }
        }
    }

    public void PressPlay()
    {
        _PressedPlay = true;
        PlayButtonParticleSystem.Play();
    }
}
