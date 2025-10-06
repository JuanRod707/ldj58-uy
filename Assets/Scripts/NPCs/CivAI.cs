using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Director;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.NPCs
{
    public class CivAI : MonoBehaviour
    {
        [SerializeField] float minWait, maxWait;
        [SerializeField] float reachDistance;
        [SerializeField] protected Movement movement;
        [SerializeField] private GameObject spriteObject;
        [SerializeField] Animator animator;

        [SerializeField] AudioHelper deathSfx;
        
        Vector3 currentTargetPoint;
        NavigationProvider navigation;
        Action onDie;
        public bool Alive { get; protected set; }

        public void Initialize(NavigationProvider navigation, Action onDie)
        {
            Alive = true;
            this.onDie = onDie;
            this.navigation = navigation;
            StartCoroutine(WaitAndNavigate());
        }

        protected virtual IEnumerator WaitAndNavigate()
        {
            animator.SetBool("Walk", false);
            var interval = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(interval);

            currentTargetPoint = navigation.GetRandomPoint().position;
            movement.GoTo(currentTargetPoint);
            animator.SetBool("Walk", true);
            StartCoroutine(WaitForArrival());
        }

        protected virtual IEnumerator WaitForArrival()
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, currentTargetPoint) < reachDistance);
            StartCoroutine(WaitAndNavigate());
        }

        public virtual void Kill()
        {
            animator.SetTrigger("Die");
            Alive = false;
            StopAllCoroutines();
            movement.Stop();
            deathSfx.PlayRandom();
            onDie();
        }
    }
}
