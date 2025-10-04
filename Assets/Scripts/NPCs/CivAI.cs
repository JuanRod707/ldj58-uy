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
            var interval = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(interval);

            currentTargetPoint = navigation.GetRandomPoint().position;
            movement.GoTo(currentTargetPoint);
            StartCoroutine(WaitForArrival());
        }

        IEnumerator WaitForArrival()
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, currentTargetPoint) < reachDistance);
            StartCoroutine(WaitAndNavigate());
        }

        public void Kill()
        {
            Alive = false;
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}
