using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WritingScript : MonoBehaviour
{
    private MinigameEnablerScript _EnableScript;
    private MinigameScoreScript _ScoreScript;

    private TMP_Text _GrayText;
    private TMP_Text _BlackText;
    private string _TextToType =
        "Dear Diary. Today a puny human attacked\n" +
        "my home.This guy has a bad case of main\n" +
        "character syndrome. Seriously. He's been\n" +
        "killing all my friends and family without\n" +
        "remorse. All for the few coppers and trinkets\n" +
        "we've managed to save over the years.\n" +
        "Gotta go. He's almost here. I really need to\n" +
        "finish this guy off.";
    private int _CurrentCharIndex = 0;

    private float _Timer = 20;


    // Start is called before the first frame update
    void Start()
    {
        _EnableScript = GetComponent<MinigameEnablerScript>();
        _ScoreScript = GetComponent<MinigameScoreScript>();

        _GrayText = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
        _BlackText = transform.GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>();

        _GrayText.text = _TextToType;
        _BlackText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (_EnableScript.MinigameEnabled)
        {
            if (_CurrentCharIndex < _TextToType.Length)
            {
                char nextChar = _TextToType[_CurrentCharIndex];
                if (nextChar == '\n')
                {
                    _CurrentCharIndex++;
                    nextChar = _TextToType[_CurrentCharIndex];
                    _BlackText.text += '\n';
                }

                if (Input.anyKeyDown)
                {
                    foreach (char c in Input.inputString)
                    {
                        if (c == nextChar)
                        {
                            _BlackText.text += c;
                            _CurrentCharIndex++;
                            _ScoreScript.IncreaseScore(0.05f);
                        }
                    }
                }
            }

            _Timer -= Time.deltaTime;
            if (_Timer < 0 )
            {
                _EnableScript.DisableMinigame();
            }
        }
    }
}
