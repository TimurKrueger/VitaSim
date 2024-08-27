using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    private int numOfHearts=(int)PlayerStats.health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update()
    {
        numOfHearts=(int)PlayerStats.getHealth();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].sprite=fullHeart;
            }
            else
            {
                hearts[i].sprite=emptyHeart;
            }
        }
    }
}
