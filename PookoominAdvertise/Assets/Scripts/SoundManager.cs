using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _BgmSource;
    [SerializeField] private AudioSource _PookooSfxSource;
    [SerializeField] private AudioSource _PhoneSfxSource;
    [SerializeField] private AudioSource _LogoSfxSource;
    [SerializeField] private AudioSource _SubSfxSource;
    [SerializeField] private AudioSource _FlowerSfxSource;

    private float fadeDuration = 1f;

    private void Awake()
    {
        _BgmSource.volume = 0f;
    }

    public void FadeInBgm()
    {
        StartCoroutine(FadeBGM(0f, 1f));
    }

    public void FadeOutBgm()
    {
        StartCoroutine(FadeBGM(1f, 0f));
    }

    private IEnumerator FadeBGM(float origin, float target)
    {
        _BgmSource.volume = origin;
        float elapsedTime = 0f;
        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(origin, target, elapsedTime / fadeDuration);
            _BgmSource.volume = newVolume;
            yield return null;
        }
        _BgmSource.volume = target;
    }
}
