using UnityEngine;
using System.Collections;

public class standardFlyer : enemy
{
    public float[] verticalForces = new float[0];
    public float forceSwitchInterval;
    public float forceSwitchSmooth;
    public bool loopForceSwitch;

    public class virtualInput : IinputListener
    {
        //Movement Pattern Control
        private float[] verticalForces = new float[0];
        private float currentVerticalForce;
        private float forceSwitchInterval;
        private float currentSwitchInterval;
        private float forceSwitchSmooth;
        private int index;
        private int prevIndex;
        private bool loop;

        //Firing Control

        public virtualInput(float forceSwitch, float[] forces, bool looping, float forceSmooth)
        {
            verticalForces = forces;
            forceSwitchInterval = forceSwitch;
            currentSwitchInterval = forceSwitch;
            loop = looping;
            forceSwitchSmooth = forceSmooth;
        }
        
        public float horAxis()
        {            
            return 1;
        }

        float verticalForce()
        {
            if (verticalForces == null || verticalForces.Length == 0)
            {
                return 0;
            }
            else if (verticalForces.Length == 1)
            {
                return verticalForces[0];
            }
            else
            {
                currentSwitchInterval -= Time.deltaTime;
                if (currentSwitchInterval <= 0)
                {
                    index++;
                    if (index > verticalForces.Length - 1)
                    {
                        if (loop)
                        {
                            index = 0;
                            prevIndex = verticalForces.Length - 1;
                        }
                        else
                        {
                            index--;
                        }
                    }

                    prevIndex = index - 1;
                    if (prevIndex < 0)
                    {
                        prevIndex = verticalForces.Length - 1;
                    }
                    currentSwitchInterval = forceSwitchInterval;
                }
            }
            return verticalForces[index];
        }

        public float verAxis()
        {
            currentVerticalForce = Mathf.Lerp(currentVerticalForce, verticalForce(), forceSwitchSmooth);
            return currentVerticalForce;
        }
        public bool firePrimary()
        {
            return true;
        }
        public bool fireSecondary()
        {
            return true;
        }
        public bool raiseShields()
        {
            return true;
        }
    }

    void Start()
    {
        foreach(actionController action in availableActions)
        {
            action.input = new virtualInput(forceSwitchInterval, verticalForces, loopForceSwitch, forceSwitchSmooth);
        }
    }

    void Update()
    {
        Debug.Log(GetComponent<Renderer>().isVisible);
        if(GetComponent<Renderer>().isVisible)
        {
            foreach (actionController action in availableActions)
            {
                action.isActive = true;
            }
        }
        else
        {
            foreach (actionController action in availableActions)
            {
                action.isActive = false;
            }
        }
        if(currentStatus)
        {
            if(currentStatus.destroyed)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogWarning("No Status Component Attached, Intentional?");
        }
    }
}
