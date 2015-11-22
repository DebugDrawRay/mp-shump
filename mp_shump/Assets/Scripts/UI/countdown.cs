using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class countdown : MonoBehaviour
{
    public string[] countdownLabels;

    public bool isActive = true;
    public bool runCountdown;

    private float labelHold = 1;
    private float maxLabelHold = 1;
    private int labelCount;
    private int labelIndex = 0;

    void Awake()
    {
        labelCount = countdownLabels.Length;
        GetComponent<Text>().text = countdownLabels[0];
    }

	void Update ()
    {
        playCountdown();
        GetComponent<Text>().enabled = isActive;
	}

    void playCountdown()
    {
        if(labelIndex < labelCount)
        {
            isActive = true;
            GetComponent<Text>().text = countdownLabels[labelIndex];
            labelHold -= Time.deltaTime;
            if(labelHold <= 0)
            {
                labelIndex++;
                labelHold = maxLabelHold;
            }
        }
        else
        {
            isActive = false;
        }
    }
}
