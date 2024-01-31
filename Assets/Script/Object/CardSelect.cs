using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelect : MonoBehaviour, IPointerClickHandler {

    public Card card;

    public void OnPointerClick(PointerEventData eventData) {
        card = transform.parent.gameObject.GetComponent<Card>();
        card.RemoveAllChildren();
    }
}
    