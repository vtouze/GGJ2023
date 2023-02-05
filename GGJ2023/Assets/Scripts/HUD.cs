using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private AmibeCharacter _character = null;
    [SerializeField] private TMP_Text _scoreDNAText = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _scoreDNAText.text = _character.ScoreDNA.ToString() + " / " + _character.RequireDNA.ToString();
    }
}
