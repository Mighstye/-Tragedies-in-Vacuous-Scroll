using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreScript : MonoBehaviour
{
    public TextMeshProUGUI textmesh;

    // Start is called before the first frame update
    void Start()
    {
        textmesh = gameObject.GetComponent<TextMeshProUGUI>();
        textmesh.text = "1234567890";
    }

    // Update is called once per frame
    void Update()
    {
        textmesh.text = "1234567890";
    }
}
