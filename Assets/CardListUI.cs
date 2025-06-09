using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardListUI : MonoBehaviour
{
    [SerializeField] private Transform cardListContainer;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<Sprite> cardSprites;
    
    public void AddCard(Sprite sprite)
    {
        GameObject newCard = Instantiate(cardPrefab, cardListContainer);
        newCard.GetComponent<Image>().sprite = sprite;
    }
    
    public void AddAllCards()
    {
        foreach (var sprite in cardSprites)
        {
            AddCard(sprite);
        }
    }
}