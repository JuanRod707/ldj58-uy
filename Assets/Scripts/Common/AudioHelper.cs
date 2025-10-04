using UnityEngine;

namespace Assets.Scripts.Common
{
    public class AudioHelper : MonoBehaviour
    {
        [SerializeField] AudioSource player;
        [SerializeField] AudioClip[] clips;
        [SerializeField] float minPitch = 1f, maxPitch = 1f;
        [SerializeField] bool playOnAwake;

        void Start()
        {
            if(playOnAwake)
                PlayRandom();
        }

        public void PlayRandom()
        {
            player.pitch = Random.Range(minPitch, maxPitch);
            player.clip = clips.PickOne();
            player.Play();
        }
    }
}
