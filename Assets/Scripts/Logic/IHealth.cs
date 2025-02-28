using System;
using UnityEngine;

namespace Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }

        GameObject TextPrefab { get; set; }

        void TakeDamage(float damage, Color color);
        void TakeHP(float HP);
    }
}