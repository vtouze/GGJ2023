using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _uiToDisplay = null;
    [SerializeField] private float _timer = 5f;
    [SerializeField] private Animation _animation= null;

    void Start()
    {
        _uiToDisplay.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _uiToDisplay.SetActive(true);
            Debug.Log("Enter");

        }
    }

    public void OnTiggerExit2D(Collider2D collision)
    {
        /*if (collision.tag == "Player")
        {
            _uiToDisplay.SetActive(false);
        }*/
        Debug.Log("Exit");
        _uiToDisplay.SetActive(false);


    }
}
