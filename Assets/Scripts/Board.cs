using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform BoardParent;

    public GameObject CardPrefab;

    private void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject card = Instantiate(CardPrefab);
            card.name = "" + i;
            card.transform.SetParent(BoardParent, false);
        }
    }
}
