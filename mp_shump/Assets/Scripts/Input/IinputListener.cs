using UnityEngine;
using System.Collections;

public interface IinputListener
{
    float horAxis();
    float verAxis();
    bool firePrimary();
    bool fireSecondary();
    bool raiseShields();
}
