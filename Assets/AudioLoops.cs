using UnityEngine;
using System.Linq;

public class AudioLoops : MonoBehaviour
{
    private class Loop
    {
        private AudioSource[] audioSources;
        private int currentAudioSourceIdx
            = 0;

        public Loop(AudioSource audioSrc)
        {
            audioSources = new[] { audioSrc, CloneAudioSource(audioSrc) };
        }

        public AudioSource CurrentAudioSource
        {
            get { return audioSources[currentAudioSourceIdx]; }
        }

        public AudioSource OtherAudioSource
        {
            get { return audioSources[1 - currentAudioSourceIdx]; }
        }

        public void Swap()
        {
            currentAudioSourceIdx = 1 - currentAudioSourceIdx;
        }
    }

    private double nextEventTime;
    private Loop[] loops;
    private int currentLoopIdx 
        = 0;

    private Loop CurrentLoop
    {
        get { return loops[currentLoopIdx]; }
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
        loops = gameObject.GetComponents<AudioSource>().Select(audioSrc => new Loop(audioSrc)).ToArray();
        CurrentLoop.CurrentAudioSource.Play();
        nextEventTime = AudioSettings.dspTime + (double)CurrentLoop.CurrentAudioSource.clip.length;
    }

    private void FixedUpdate()
    {
        var time = AudioSettings.dspTime;
        if (time + 1.0 > nextEventTime)
        {
            CurrentLoop.OtherAudioSource.PlayScheduled(nextEventTime);
            nextEventTime += (double)CurrentLoop.OtherAudioSource.clip.length;
            CurrentLoop.Swap();
        }
    }

    public int LoopIndex
    {
        get { return currentLoopIdx; }
        set { currentLoopIdx = value; }
    }
}
