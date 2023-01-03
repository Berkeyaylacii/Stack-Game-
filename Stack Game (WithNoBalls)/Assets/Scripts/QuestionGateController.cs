using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionGateController : MonoBehaviour
{
    public GameObject questionText;

    public BallController BallController;

    [SerializeField] private enum GateType
    {
        YesQuestionGate,
        NoQuestionGate
    }

    [SerializeField] private GateType _gateType;

    [SerializeField] private string _gateText;
    public string GetGateText()
    {
        return _gateText;
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            

            if (_gateText == "YES")
            {
                Debug.Log("Player said YES");
            }

            if (_gateText == "NO")
            {
                Debug.Log("Player said NO");
            }

            questionText.SetActive(false);
        }
    }


   

}
