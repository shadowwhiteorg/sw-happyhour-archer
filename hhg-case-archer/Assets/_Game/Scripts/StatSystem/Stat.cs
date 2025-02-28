using System.Collections.Generic;
using _Game.DataStructures;
using _Game.Enums;
using UnityEngine;

namespace _Game.StatSystem
{
    public class Stat
    {
        public float BaseValue { get; private set; }
        private float _currentValue;
        private List<StatModifier> _modifiers = new List<StatModifier>();

        public Stat(float baseValue)
        {
            BaseValue = baseValue;
            _currentValue = baseValue;
        }

        public float GetValue()
        {
            return _currentValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            RecalculateValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
            RecalculateValue();
        }
        
        private void RecalculateValue()
        {
            // _currentValue = BaseValue;
            float percentMultiplier = 1f;

            foreach (var mod in _modifiers)
            {
                if (mod.Type == ModifierType.Flat)
                    _currentValue += mod.Value;
                else if (mod.Type == ModifierType.Percentage)
                    percentMultiplier *= (1f + mod.Value / 100f); // Multiplicative stacking
            }

            _currentValue *= percentMultiplier;
        }
    }
}