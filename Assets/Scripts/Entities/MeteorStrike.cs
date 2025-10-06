using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Director;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Entities
{
    public class MeteorStrike : MonoBehaviour
    {
        [SerializeField] Transform meteor;
        [SerializeField] AudioSource explosionSfx;
        [SerializeField] ParticleSystem explosionVfx;

        [SerializeField] float meteorSpeed;
        [SerializeField] float spawnHeight;
        [SerializeField] float spawnRadius;
        [SerializeField] float impactThreshold;
        [SerializeField] float lifeTime;
        [SerializeField] float killRadius;

        void Start()
        {
            var xz = Random.insideUnitCircle * spawnRadius;
            var meteorStartPoint = new Vector3(xz.x, spawnHeight, xz.y);

            meteor.localPosition = meteorStartPoint;
            meteor.LookAt(transform.position);
        }

        void Update()
        {
            meteor.Translate(Vector3.forward * meteorSpeed * Time.deltaTime);
            if (meteor.localPosition.magnitude < impactThreshold) 
                Impact();
        }

        void Impact()
        {
            enabled = false;
            Destroy(meteor.gameObject);
            explosionSfx.Play();
            explosionVfx.Play();

            FindFirstObjectByType<Shake>().Trigger();
            FindFirstObjectByType<SoulProvider>().KillInZone(transform.position, killRadius);

            Invoke("Dispose", lifeTime);
        }

        void Dispose() => Destroy(gameObject);
    }
}
