using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private AmibeCharacter _character = null;
    [SerializeField] private TMP_Text _scoreDNAText = null;
    [SerializeField] private TMP_Text _chrono = null;
    private float _time = 0f;
    private int _seconds;
    private int _minutes;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        _scoreDNAText.text = _character.ScoreDNA.ToString() + " / " + _character.RequireDNA.ToString();
        
        _time += Time.deltaTime;

        _seconds = Mathf.RoundToInt(_time);
        if(_seconds > 60)
        {
            _minutes += 1;
            _time = 1;

        }
        _minutes = Mathf.RoundToInt(_minutes);
        
        
        _chrono.text = _minutes.ToString() + ":" + _seconds.ToString();
    }

 

}