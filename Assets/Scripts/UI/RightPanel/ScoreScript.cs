using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI textmesh;

    // Start is called before the first frame update
    private void Start()
    {
        textmesh = gameObject.GetComponent<TextMeshProUGUI>();
        textmesh.text = "1234567890";
    }

    // Update is called once per frame
    private void Update()
    {
        textmesh.text = "1234567890";
    }
}