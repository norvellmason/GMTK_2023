using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    private PowerLevelScript _PowerLevelScript;

    private List<MinigameData> _Minigames = new List<MinigameData>();
    private int _MinigameIndex = 0;

    public GameObject _CoachPrefab;
    private Transform _CoachTransform;
    private Vector3 _CoachDestination = new Vector3(15, -4, -5);

    public GameObject _GoblinaPrefab;
    private Transform _GoblinaTransform;
    private Vector3 _GoblinaDestination = new Vector3(15, 6, -5);

    public GameObject _GoblinoPrefab;
    private Transform _GoblinoTransform;
    private Vector3 _GoblinoDestination = new Vector3(2, 8, -5);

    public GameObject _HeroPrefab1;
    private Transform _HeroTransform1;
    private Vector3 _HeroDestination1 = new Vector3(14, 0, -5);

    public GameObject _HeroPrefab2;
    private Transform _HeroTransform2;
    private Vector3 _HeroDestination2 = new Vector3(-14, 0, -5);

    public GameObject _HeroPrefab3;
    private Transform _HeroTransform3;
    private Vector3 _HeroDestination3 = new Vector3(14, 0, -5);


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
        _CoachTransform = Instantiate(_CoachPrefab, new Vector3(-14f, 5, -5), Quaternion.identity).transform;
        _GoblinaTransform = Instantiate(_GoblinaPrefab, new Vector3(-11f, -5, -5), Quaternion.identity).transform;
        _GoblinoTransform = Instantiate(_GoblinoPrefab, new Vector3(-3f, -8, -5), Quaternion.identity).transform;
        _HeroTransform1 = Instantiate(_HeroPrefab1, new Vector3(-14f, 0, -5), Quaternion.identity).transform;
        _HeroTransform2 = Instantiate(_HeroPrefab2, new Vector3(14f, 0, -5), Quaternion.identity).transform;
        _HeroTransform3 = Instantiate(_HeroPrefab3, new Vector3(-14f, 0, -5), Quaternion.identity).transform;

        _Minigames.Add(new MinigameData(transform.GetChild(1).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(2).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(3).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(4).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(5).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(6).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(7).gameObject, ReportSuccess));
        _Minigames.Add(new MinigameData(transform.GetChild(8).gameObject, ReportSuccess));


        _Minigames.First().Enabler.EnableMinigame();

        //_Minigames[2].Enabler.EnableMinigame();
        //_MinigameIndex = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (_MinigameIndex > 1)
        {
            MoveHero1();
        }
       
        if (_MinigameIndex > 2)
        {
            MoveGoblina();
        }

        if (_MinigameIndex > 3)
        {
            MoveHero2();
        }

        if (_MinigameIndex > 4)
        {
            MoveGoblino();
            MoveCoach();
        }

        if (_MinigameIndex > 5)
        {
            MoveHero3();
        }

        if (_MinigameIndex < _Minigames.Count && _Minigames[_MinigameIndex].Enabler.IsFadingOut)
        {
            _MinigameIndex++;

            if (_MinigameIndex < _Minigames.Count)
            {
                _Minigames[_MinigameIndex].Enabler.EnableMinigame();
            }
        }

        if (_MinigameIndex == 8)
        {
            if (_PowerLevelScript.PowerLevel > 17f)
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

    private void MoveCoach()
    {
        Vector3 directionToMove = _CoachDestination - _CoachTransform.position;
        if (directionToMove != Vector3.zero)
        {
            directionToMove.Normalize();
            _CoachTransform.position += directionToMove * Time.deltaTime * 3;
        }
    }

    private void MoveGoblina()
    {
        Vector3 directionToMove = _GoblinoDestination - _GoblinoTransform.position;
        if (directionToMove != Vector3.zero)
        {
            directionToMove.Normalize();
            _GoblinoTransform.position += directionToMove * Time.deltaTime * 3;
        }
    }
    
    private void MoveGoblino()
    {
        Vector3 directionToMove = _GoblinaDestination - _GoblinaTransform.position;
        if (directionToMove != Vector3.zero)
        {
            directionToMove.Normalize();
            _GoblinaTransform.position += directionToMove * Time.deltaTime * 3;
        }
    }

    private void MoveHero1()
    {
        Vector3 directionToMove = _HeroDestination1 - _HeroTransform1.position;
        if (directionToMove != Vector3.zero)
        {
            directionToMove.Normalize();
            _HeroTransform1.position += directionToMove * Time.deltaTime * 3;
        }
    }

    private void MoveHero2()
    {
        Vector3 directionToMove = _HeroDestination2 - _HeroTransform2.position;
        if (directionToMove != Vector3.zero)
        {
            directionToMove.Normalize();
            _HeroTransform2.position += directionToMove * Time.deltaTime * 3;
        }
    }

    private void MoveHero3()
    {
        Vector3 directionToMove = _HeroDestination3 - _HeroTransform3.position;
        if (directionToMove != Vector3.zero)
        {
            directionToMove.Normalize();
            _HeroTransform3.position += directionToMove * Time.deltaTime * 3;
        }
    }

    public void ReportSuccess(float successAmount)
    {
        _PowerLevelScript.IncreasePowerLevel(successAmount);
    }
}