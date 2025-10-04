using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Common
{
    public class Shake : MonoBehaviour
    {
        [SerializeField] bool playOnAwake;
        [SerializeField] bool loops;
        [SerializeField] bool resetLocalWhenDone;

        [SerializeField] float violence;
        [SerializeField] float shakeTime;
        [SerializeField] int frameFrequency;

        Vector3 baseLocalPos;
        float elapsed;
        int elapsedFrames;

        void Start()
        {
            baseLocalPos = transform.localPosition;
            enabled = playOnAwake;
        }

        public void OverrideViolence(float amount) => violence = amount;

        void Update()
        {
            if (elapsedFrames == 0)
            {
                var offset = Random.insideUnitSphere;
                transform.localPosition = baseLocalPos + offset * violence;
            }


            if (!loops)
            {
                if (elapsed > shakeTime)
                {
                    enabled = false;
                    if (resetLocalWhenDone)
                        transform.localPosition = baseLocalPos;
                }

                elapsed += Time.deltaTime;
            }

            elapsedFrames++;

            if (elapsedFrames > frameFrequency)
                elapsedFrames = 0;
        }

        public void Trigger()
        {
            baseLocalPos = transform.localPosition;
            elapsed = 0;
            elapsedFrames = 0;
            enabled = true;
        }
    }
}
