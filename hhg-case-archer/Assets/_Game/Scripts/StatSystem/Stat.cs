using System.Collections.Generic;
using System.Linq;
using _Game.DataStructures;
using _Game.Enums;
using Mono.Cecil.Cil;
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

        public float GetValue() => _currentValue;

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            RecalculateValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            // remove modifier with the same value and same type from the list
            var existingModifier = _modifiers.FirstOrDefault(m => m.Value == modifier.Value && m.Type == modifier.Type);
            if (existingModifier != null)
            {
                Debug.Log("remove modifier at stat "+_modifiers.Contains(existingModifier));
                _modifiers.Remove(existingModifier);
            }
            RecalculateValue();
        }

        private void RecalculateValue()
        {
            Debug.Log("Number of modifiers: " + _modifiers.Count);
            // 1️⃣ RESET TO BASE VALUE FIRST - MOST CRITICAL FIX
            _currentValue = BaseValue;

            float percentMultiplier = 1f;

            // 2️⃣ PROCESS FLAT MODIFIERS FIRST
            foreach (var mod in _modifiers.Where(m => m.Type == ModifierType.Flat))
            {
                _currentValue += mod.Value;
            }

            // 3️⃣ PROCESS PERCENTAGE MODIFIERS SECOND (multiplicative)
            foreach (var mod in _modifiers.Where(m => m.Type == ModifierType.Percentage))
            {
                percentMultiplier *= 1f + mod.Value / 100f;
            }

            // 4️⃣ APPLY PERCENTAGE MULTIPLIER
            Debug.Log("Percent Multiplier: " + percentMultiplier + " Current Value: " + _currentValue);
            _currentValue *= percentMultiplier;
        }
    }
}