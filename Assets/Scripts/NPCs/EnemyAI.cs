using System.Collections;
using UnityEngine;

namespace Assets.Scripts.NPCs
{
    public class EnemyAI : CivAI
    {
        [SerializeField] private LineRenderer lineRenderer;
        
        public void EnterCombat(Vector3 playerPosition)
        {
            movement.Stop();
            StopAllCoroutines();
            SetLaser(playerPosition);
        }

        public override void Kill()
        {
            // animator.SetTrigger("Die");
            Alive = false;
            gameObject.SetActive(false);//Replace this with animation to die
        }

        private void SetLaser(Vector3 playerPosition)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, playerPosition);
        }

        public void Move()
        {
            lineRenderer.enabled = false;
            movement.Restart();
            StartCoroutine(WaitAndNavigate());
        }
    }
}