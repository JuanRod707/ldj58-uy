using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource player;
        [SerializeField] AudioClip[] loops;
        
        public void OnRoundChanged(int currentRound)
        {
            var index = Mathf.Clamp(currentRound, 0, loops.Length) -1;
            var nextClip = loops[index];

            player.loop = false;
            StartCoroutine(WaitForEndOfClip(nextClip));
        }

        IEnumerator WaitForEndOfClip(AudioClip nextClip)
        {
            yield return new WaitUntil(() => !player.isPlaying);

            player.clip = nextClip;
            player.loop = true;
            player.Play();
        }
    }
}
