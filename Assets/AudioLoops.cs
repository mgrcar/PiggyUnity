using UnityEngine;
using System.Linq;

public class AudioLoops : MonoBehaviour
{
    private class Loop
    {
        private AudioSource[] AudioSources;
        private int CurrentAudioSourceIdx
            = 0;

        public Loop(AudioSource audioSrc)
        {
            AudioSources = new[] { audioSrc, CloneAudioSource(audioSrc) };
        }

        public AudioSource CurrentAudioSource
        {
            get { return AudioSources[CurrentAudioSourceIdx]; }
        }

        public AudioSource OtherAudioSource
        {
            get { return AudioSources[1 - CurrentAudioSourceIdx]; }
        }

        public void Swap()
        {
            CurrentAudioSourceIdx = 1 - CurrentAudioSourceIdx;
        }
    }

    private double NextEventTime;
    private Loop[] Loops;
    private int CurrentLoopIdx 
        = 0;

    private Loop CurrentLoop
    {
        get { return Loops[CurrentLoopIdx]; }
    }

    private static AudioSource CloneAudioSource(AudioSource audioSrc)
    {
        AudioSource audioSrcClone = new GameObject(null, typeof(AudioSource)).GetComponent<AudioSource>();
        audioSrcClone.clip = audioSrc.clip;
        audioSrcClone.playOnAwake = audioSrc.playOnAwake;
        // TODO: copy other properties if needed
        return audioSrcClone;
    }

    private void Start()
    {
        Loops = gameObject.GetComponents<AudioSource>().Select(audioSrc => new Loop(audioSrc)).ToArray();
        CurrentLoop.CurrentAudioSource.Play();
        NextEventTime = AudioSettings.dspTime + (double)CurrentLoop.CurrentAudioSource.clip.length;
    }

    private void FixedUpdate()
    {
        var time = AudioSettings.dspTime;
        if (time + 1.0 > NextEventTime)
        {
            CurrentLoop.OtherAudioSource.PlayScheduled(NextEventTime);
            NextEventTime += (double)CurrentLoop.OtherAudioSource.clip.length;
            CurrentLoop.Swap();
        }
    }

    public int LoopIndex
    {
        get { return CurrentLoopIdx; }
        set { CurrentLoopIdx = value; }
    }
}
