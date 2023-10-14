using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupDisplay : MonoBehaviour
{
    private TMP_Text display;
    public Image[] bulletImages;
    public Image slowTimeImage;
    public float slowTime;
    public int numOfBullets;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TMP_Text>();
        slowTime = 0;
        numOfBullets = 0;
        updateDisplay();
        updateSlowtimeDisplay();
        updateBulletDisplay();
    }

    private void updateDisplay()
    {
        display.text =
            "Powerups\n" +
            "\n" +
            "SlowTime\n" +
            (int)((slowTime > 0 ? slowTime + 1 : slowTime)) + "s\n" +
            "\n" +
            "Bullets\n" +
            numOfBullets;
    }

    private void updateBulletDisplay()
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            if (i < numOfBullets)
            {
                bulletImages[i].color = Color.white;
            }
            else
            {
                bulletImages[i].color = Color.black;
            }
        }
    }

    private void updateSlowtimeDisplay()
    {
        if (slowTime > 0)
        {
            slowTimeImage.color = Color.white;
        }
        else
        {
            slowTimeImage.color = Color.black;
        }
    }

    public void setSlowTime(float t)
    {
        slowTime = t;
        updateDisplay();
        updateSlowtimeDisplay();
    }

    public void setBullets(int b)
    {
        numOfBullets = b;
        updateDisplay();
        updateBulletDisplay();
    }
}
