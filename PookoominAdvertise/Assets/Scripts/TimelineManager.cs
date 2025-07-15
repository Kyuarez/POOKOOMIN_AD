using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class TimelineManager : MonoBehaviour
{
    private PlayableDirector _timeline;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void InitTimeline()
    {
        var manager = GameObject.FindFirstObjectByType<TimelineManager>();
        if (manager != null)
        {
            manager.OnPreload();
        }
    }

    public void OnPreload()
    {
        _timeline = GetComponent<PlayableDirector>();
        StartCoroutine(PreloadAudio());
    }

    private IEnumerator PreloadAudio()
    {
        var timelineAsset = _timeline.playableAsset as TimelineAsset;
        if (timelineAsset == null)
        {
            yield break;
        }

        var clipsToPreload = new List<AudioClip>();

        foreach (var track in timelineAsset.GetOutputTracks().Where(a => a is AudioTrack))
        {
            foreach (var clip in track.GetClips())
            {
                var audioAsset = clip.asset as AudioPlayableAsset;
                if (audioAsset != null && audioAsset.clip != null)
                    clipsToPreload.Add(audioAsset.clip);
            }
        }

        // 2. ��� AudioClip �̸� �ε� �� ���ڵ�
        foreach (var clip in clipsToPreload)
        {
            clip.LoadAudioData();
            while (clip.loadState == AudioDataLoadState.Loading)
            {
                yield return null;
            }
        }

        // 3. ���� ���־� �÷��̿� AudioSource ���� & ���
        var go = new GameObject("WarmupAudioSource");
        var src = go.AddComponent<AudioSource>();
        src.playOnAwake = false;

        foreach (var clip in clipsToPreload)
        {
            src.clip = clip;
            src.volume = 0f;
            src.Play();
            // DSP ���ۿ� ä�������� �� ������ ���
            yield return null;
            src.Stop();
        }

        Destroy(go);
        yield return null;
        _timeline?.Play();
    }
}