using UnityEngine;
using System.Collections;
using DG.Tweening;
public class reactionShake : MonoBehaviour
{

    [Header("Reactions")]
    private Tween shakeTween;
    public float shakeDuration;
    public float shakeStrength;
    public int shakeVibrado;
    public float shakeRandomness;

    public void shake()
    {
        if (shakeTween == null || !shakeTween.IsPlaying())
        {
            if (GetComponent<RectTransform>())
            {
                shakeTween = GetComponent<RectTransform>().DOShakePosition(shakeDuration, shakeStrength, shakeVibrado, shakeRandomness);
            }
            else
            {
                shakeTween = transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrado, shakeRandomness);
            }
        }
    }
}
