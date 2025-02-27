using UnityEngine;

namespace _Game.Utils
{
    public static class ExtensionMethods
    {
        public static float FloatMap(this float attackSpeed, float minSpeed, float maxSpeed, float minFireRate, float maxFireRate)
        {
            float t = Mathf.Clamp01((attackSpeed - minSpeed) / (maxSpeed - minSpeed));
            return Mathf.Lerp(maxFireRate, minFireRate, t);
        }
        // public static float FloatMap(this float attackSpeed, float minSpeed, float maxSpeed, float minFireRate, float maxFireRate)
        // {
        //     float t = Mathf.Clamp01((attackSpeed - minSpeed) / (maxSpeed - minSpeed));
        //     float fireRate = Mathf.Lerp(maxFireRate, minFireRate, t);
        //     return 1f / fireRate;  // Convert fire rate to shot delay
        // }
    }
}