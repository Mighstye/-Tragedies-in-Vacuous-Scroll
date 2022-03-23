using CardSystem;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCardHover : MonoBehaviour
{
    private SpriteRenderer cardImage;

    private void Awake()
    {
        cardImage = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("GrazeBox")) return;
        var color = cardImage.color;
        color = new Color(color.r, color.g, color.b, 0.1f);
        cardImage.color = color;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("GrazeBox")) return;
        var color = cardImage.color;
        color = new Color(color.r, color.g, color.b, 1.0f);
        cardImage.color = color;
    }

    public void UpdateSelectedCard(ActiveCard activeCard)
    {
        cardImage.sprite = activeCard.gameObject.GetComponent<Image>().sprite;
    }
}