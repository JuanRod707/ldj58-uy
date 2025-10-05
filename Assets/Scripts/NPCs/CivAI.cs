using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [SerializeField] Movement movement;
        [SerializeField] private GameObject spriteObject;
        [SerializeField] Animator animator;
        
        Vector3 currentTargetPoint;
        NavigationProvider navigation;
        public bool Alive { get; private set; }

        public void Initialize(NavigationProvider navigation)
        {
            Alive = true;
            this.navigation = navigation;
            StartCoroutine(WaitAndNavigate());
        }

        IEnumerator WaitAndNavigate()
        {
            animator.SetBool("Walk", false);
            var interval = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(interval);

            currentTargetPoint = navigation.GetRandomPoint().position;
            movement.GoTo(currentTargetPoint);
            animator.SetBool("Walk", true);
            StartCoroutine(WaitForArrival());
        }

        IEnumerator WaitForArrival()
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, currentTargetPoint) < reachDistance);
            StartCoroutine(WaitAndNavigate());
        }

        public void Kill()
        {
            animator.SetTrigger("Die");
            Alive = false;
            StopAllCoroutines();
            movement.Stop();
        }
    }
}
