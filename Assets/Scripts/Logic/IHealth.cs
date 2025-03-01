using System;
using UnityEngine;

namespace Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        int CurrentHP { get; set; }
        int MaxHP { get; set; }

        GameObject TextPrefab { get; set; }

        void TakeDamage(int damage, Color color);
        void TakeHP(int HP);
    }
}