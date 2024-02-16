using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    public GameObject[] cardPrefab = new GameObject[16];
    public GameObject Player;
    public List<int> usedNumbers = new List<int>();
    public int randomNumber;
    public bool isGamePaused = false;

    public void CardInstantiate() {

        usedNumbers.Clear();

        for (int i = -1; i < 2; i++) {
            randomNumber = GetUniqueRandomNumber();
            GameObject card = Instantiate(cardPrefab[randomNumber], new Vector2(Player.transform.position.x + (i * 6), Player.transform.position.y), Quaternion.identity);
            card.transform.parent = this.gameObject.transform;
            Time.timeScale = 0f;
        }
    }

    int GetUniqueRandomNumber()
    {
        int newNumber;

        do
        {
            newNumber = Random.Range(0, 16);
        } while (usedNumbers.Contains(newNumber));

        usedNumbers.Add(newNumber);

        return newNumber;
    }

    public void RemoveAllChildren() {
        Time.timeScale = 1f;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        transform.DetachChildren();
    }
}
