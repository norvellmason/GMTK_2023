using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TapingScript : MonoBehaviour
{
    private MinigameScoreScript _ScoreScript;
    private MinigameEnablerScript _EnableScript;

    public GameObject TapePrefab;
    private Transform _TapeFollowingMouse;
    private Vector2 _TapeStartPosition;
    private bool _IsDragging = false;
    private float _Timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _ScoreScript = GetComponent<MinigameScoreScript>();
        _EnableScript = GetComponent <MinigameEnablerScript>();

        transform.GetChild(1).GetComponent<PictureClickedScript>().AddOnClickListener(OnPictureClick);
        transform.GetChild(2).GetComponent<PictureClickedScript>().AddOnClickListener(OnPictureClick);
        transform.GetChild(1).GetComponent<PictureClickedScript>().AddOnReleaseListener(OnPictureRelease);
        transform.GetChild(2).GetComponent<PictureClickedScript>().AddOnReleaseListener(OnPictureRelease);
    }

    public void OnPictureClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        //mousePos.x = Mathf.Min(mousePos.x, 8);
        //mousePos.x = Mathf.Max(mousePos.x, -8);
        //mousePos.y = Mathf.Min(mousePos.y, 4);
        //mousePos.y = Mathf.Max(mousePos.y, -4);

        _IsDragging = true;

        _TapeFollowingMouse = Instantiate(TapePrefab, new Vector3(mousePos.x, mousePos.y), Quaternion.identity, gameObject.transform).transform;
        _TapeStartPosition = _TapeFollowingMouse.transform.position;
    }

    public void OnPictureRelease()
    {
        _IsDragging = false;
        _TapeFollowingMouse = null;
        _ScoreScript.IncreaseScore(0.1f);

        //if (_TapeStartPosition.x < 0 && mousePos.x > 0 ||
        //    _TapeStartPosition.x > 0 && mousePos.x < 0)
        //{
            
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (_EnableScript.MinigameEnabled)
        {
            //if (Input.GetMouseButtonUp(0))
            //{

            //}

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos.x = Mathf.Min(mousePos.x, 8);
            mousePos.x = Mathf.Max(mousePos.x, -8);
            mousePos.y = Mathf.Min(mousePos.y, 4);
            mousePos.y = Mathf.Max(mousePos.y, -4);

            if (_IsDragging)
            {
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                if (mousePos2D != _TapeStartPosition)
                {
                    Vector2 directionToMouse = mousePos2D - _TapeStartPosition;

                    _TapeFollowingMouse.position = _TapeStartPosition + (directionToMouse * 0.5f);

                    _TapeFollowingMouse.localScale = new Vector2(directionToMouse.magnitude * 1.75f, _TapeFollowingMouse.localScale.y);
                    _TapeFollowingMouse.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(new Vector2(1, 0), directionToMouse));
                }
            }

            _Timer -= Time.deltaTime;
            if (_Timer < 0)
            {
                _EnableScript.DisableMinigame();
            }
        }
    }
}
