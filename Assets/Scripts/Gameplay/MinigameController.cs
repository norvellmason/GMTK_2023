using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    private PowerLevelScript _PowerLevelScript;

    private List<MinigameData> _Minigames = new List<MinigameData>();
    private int _MinigameIndex = 0;

    private class MinigameData
    {
        public GameObject ParentObject { get; private set; }
        public MinigameEnablerScript Enabler { get; private set; }


        public MinigameData(GameObject parentObject, MinigameScoreScript.IncreaseScoreCallback scoreCallback)
        {
            ParentObject = parentObject;
            Enabler = ParentObject.transform.GetComponent<MinigameEnablerScript>();
            ParentObject.transform.GetComponent<MinigameScoreScript>().AddScoreListener(scoreCallback);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _PowerLevelScript = transform.GetChild(0).GetComponent<PowerLevelScript>();

        _Minigames.Add(new MinigameData(transform.GetChild(1).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(2).gameObject, ReportSuccess));

        _Minigames.First().Enabler.EnableMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_Minigames[_MinigameIndex].Enabler.MinigameEnabled)
        {
            _MinigameIndex++;
            
            if (_MinigameIndex < _Minigames.Count)
            {
                _Minigames[_MinigameIndex].Enabler.EnableMinigame();
            }
        }
    }

    public void ReportSuccess(float successAmount)
    {
        _PowerLevelScript.IncreasePowerLevel(successAmount);
    }
}