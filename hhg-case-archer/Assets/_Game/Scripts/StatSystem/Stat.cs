using System.Collections.Generic;
using _Game.DataStructures;
using _Game.Enums;
using UnityEngine;

namespace _Game.StatSystem
{
    public class Stat
    {
        public float BaseValue;
        private List<StatModifier> _modifiers = new List<StatModifier>();
    
        public Stat(float baseValue)
        {
            BaseValue = baseValue;
        }
    
        public float GetValue()
        {
            float finalValue = BaseValue;
            float percentMultiplier = 1f;
        
            foreach (var mod in _modifiers)
            {
                if (mod.Type == ModifierType.Flat)
                    finalValue += mod.Value;
                else if (mod.Type == ModifierType.Percentage)
                    percentMultiplier += mod.Value / 100f;
            }
            return finalValue * percentMultiplier;
        }
    
        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }
    
        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
        }
    }
}