using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{
    private TextMeshProUGUI textmesh;
    private int timevalue = 0;

    // Start is called before the first frame update
    void Start()
    {
        textmesh = gameObject.GetComponent<TextMeshProUGUI>();
        textmesh.text = string.Format("{0:D10}", timevalue);
    }

    // Update is called once per frame
    void Update()
    {
        timevalue++;
        textmesh.text = string.Format("{0:D10}", timevalue);
    }
}
