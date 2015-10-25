using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class countdown : MonoBehaviour, IcanvasObject
{
    public string[] countdownLabels;

    public bool isActive
    {
        get;
        set;
    }

    private float labelHold = 1;
    private float maxLabelHold = 1;
    private int labelCount;
    private int labelIndex = 0;
    void Awake()
    {
        labelCount = countdownLabels.Length;
        GetComponent<Text>().text = countdownLabels[0];
        isActive = false;
    }

	void Update ()
    {
        if (isActive)
        {
            playCountdown();
        }
        GetComponent<Text>().enabled = isActive;
	}

    void playCountdown()
    {
        if(labelIndex < labelCount)
        {
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
