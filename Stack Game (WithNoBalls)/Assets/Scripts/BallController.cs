using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
   // [SerializeField] private GameObject _ballPrefab;

    [SerializeField] private TMP_Text _ballCountText = null;

   // [SerializeField] private List<GameObject> _balls = new List<GameObject>();

    [SerializeField] private float _verticalSpeed;

    [SerializeField] private float _horizontalSpeed;

    [SerializeField] private float _horizontalLimit;


    public SpawnManager spawnManager;

    public QuestionGateController qgt;

    public GameObject questionText;

    private string _gateText;

    private int _gateNumber;

    private float _horizontal;

    private int _targetCount;

    public int playerNumber;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        ForwardMovement();
        UpdateBallCountText();

    }

    private void HorizontalMovement()
    {
        float _newX;

        if (Input.GetMouseButton(0))
        {
            _horizontal = Input.GetAxisRaw("Mouse X");
        }
        else
        {
            _horizontal = 0;
        }

        _newX = transform.position.x + _horizontal * _horizontalSpeed * Time.deltaTime;
        _newX = Mathf.Clamp(_newX, -_horizontalLimit, _horizontalLimit);

        transform.position = new Vector3(
            _newX,
            transform.position.y,
            transform.position.z);
    }


    private void ForwardMovement()
    {
        transform.Translate(Vector3.forward * _verticalSpeed * Time.deltaTime);
    }

    private void UpdateBallCountText()
    {
        //playerNumber = _balls.Count; ;
        //_ballCountText.text = _balls.Count.ToString();
        _ballCountText.text = counter.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BallStack"))
        {   
            //other.gameObject.transform.SetParent(transform);
           // other.gameObject.GetComponent<SphereCollider>().enabled = false;
           // other.gameObject.transform.localPosition = new Vector3(0f, 0f, _balls[_balls.Count - 1].transform.localPosition.z - 1f);

            other.gameObject.SetActive(false);
            counter++;
            //_balls.Add(other.gameObject);
        }

        if (other.gameObject.CompareTag("Gate") || other.gameObject.CompareTag("LastGate"))
        {
            _gateNumber = other.gameObject.GetComponent<GateController>().GetGateNumber();
            //_targetCount = _balls.Count + _gateNumber;
            _targetCount = counter + _gateNumber;

            if (_gateNumber > 0)
            {
                IncreaseBallCount();
            }
            else if(_gateNumber <0)
            {
                DecreaseBallCount();
            }

            if(other.gameObject.CompareTag("LastGate"))
            {
                questionText.GetComponent<Text>().text = randomQuestionMaker();
                Debug.Log(questionText.GetComponent<Text>().text);
                questionText.SetActive(true);
            }
            
        }

        if (other.gameObject.CompareTag("SpawnTrigger"))
        {

            spawnManager.SpawnTriggerEntered();
        }


    }

    private void IncreaseBallCount()
    {
        for(int i=0; i < _gateNumber; i++)
        {
            
        //    GameObject _newBall = Instantiate(_ballPrefab);
        //    _newBall.transform.SetParent(transform);
        //    _newBall.GetComponent<SphereCollider>().enabled = false;
        //    _newBall.transform.localPosition = new Vector3(0f, 0f, _balls[_balls.Count - 1].transform.localPosition.z - 1f);
        //    _balls.Add(_newBall);
            counter++;
        } 
       
        Debug.Log("Total balls:"+counter);
    }

    private void DecreaseBallCount()
    {
        for(int i = counter -1 ; i >= _targetCount ; i--)
        {
            // _balls[i].SetActive(false);
            //_balls.RemoveAt(i);
            counter--;
        }
    }

    private string randomQuestionMaker()
    {
        int randomNumber = Random.Range(2, 4);

        string textt = null;

        if (randomNumber == 2)
        {
            textt = "Is it dividable by 2?";
           // questionText.GetComponent<Text>().text = textt;
          // Debug.Log(questionText.GetComponent<Text>().text);
        }
        else if (randomNumber == 3)
        {
            textt = "Is it dividable by 3?";
           // questionText.GetComponent<Text>().text = textt;
           // Debug.Log(questionText.GetComponent<Text>().text);
        }
        else if (randomNumber == 4)
        {
            textt = "Is it dividable by 4?";
            //questionText.GetComponent<Text>().text = textt;
           // Debug.Log(questionText.GetComponent<Text>().text);
        }

        return textt;

    }
}
