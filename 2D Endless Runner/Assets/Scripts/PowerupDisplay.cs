using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupDisplay : MonoBehaviour
{
    private TMP_Text display;
    public Image[] bulletImages;
    public Image[] shieldImages;
    public Image shield;
    public Image slowTimeImage;
    public float slowTime;
    public int numOfBullets;
    public int numOfShields;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TMP_Text>();
        slowTime = 0;
        numOfBullets = 0;
        updateDisplay();
        updateSlowtimeDisplay();
        updateBulletDisplay();
        updateShieldDisplay();
    }

    private void updateDisplay()
    {
        display.text =
            "Powerups\n" +
            "\n" +
            "Slowdown\n" +
            Mathf.CeilToInt(slowTime) + "s\n" +
            "\n" +
            "Bullets\n" +
            numOfBullets + "\n" +
            "\n" +
            "Shield\n" +
            (numOfShields >= 3 ? "READY" : "X");
        
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

    private void updateShieldDisplay()
    {
        for (int i = 0; i < shieldImages.Length; i++)
        {
            if (i < numOfShields)
            {
                shieldImages[i].color = Color.white;
            }
            else
            {
                shieldImages[i].color = Color.black;
            }
        }
        if (numOfShields == shieldImages.Length)
        {
            shield.color = Color.white;
        }
        else
        {
            shield.color = Color.black;
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

    public void setShields(int s)
    {
        numOfShields = s;
        updateDisplay();
        updateShieldDisplay();
    }
}
