using System.Collections;
using System.Collections.Generic;
using Control;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OnHitVFX : MonoBehaviour
{
    private ColorAdjustments colorAdjustments;
    public float effectInLength = 1;
    public float effectKeepLength = 2;
    public float effectOutLength = 1;

    private void Start()
    {
        var volume = GetComponent<Volume>();
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        YoumuController.instance.onYoumuHit += () => { StartCoroutine(ApplyEffect()); };
    }

    //Coroutine implementation
    private IEnumerator ApplyEffect()
    {
        while (colorAdjustments.saturation.value > -100f)
        {
            colorAdjustments.saturation.value -= 100/effectInLength*Time.deltaTime;
            yield return null;
        }

        var t = 0f;
        while (t < effectKeepLength)
        {
            t += Time.deltaTime;
            yield return null;
        }

        while (colorAdjustments.saturation.value < 0)
        {
            colorAdjustments.saturation.value += 100/effectOutLength*Time.deltaTime;
            yield return null;
        }
    }
}
