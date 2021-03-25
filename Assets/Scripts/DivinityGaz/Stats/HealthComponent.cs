using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.HealthSystem
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        private float currentHealth = 0f;
        [SerializeField] private bool isDead = false;

        public bool IsDead { get { return isDead; } }
        public float MaxHealth { get { return maxHealth; } }
        public float CurrentHealth { get { return currentHealth; } }

        private void Start() => currentHealth = maxHealth;

        public bool TakeDamage (float damageToTake)
        {
            currentHealth -= damageToTake;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth < 0)
            {
                isDead = true;
            }

            return currentHealth <= 0;
        }
    }
}