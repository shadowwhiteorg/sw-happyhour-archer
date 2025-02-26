using _Game.CombatSystem;
using UnityEngine;

namespace _Game.SkillSystem.ProjectileSkills
{
    [CreateAssetMenu(fileName = "ProjectileSkillEffect", menuName = "SkillSystem/ Projectile Skill Effect", order = 0)]
    public class ProjectileSkillEffect : SkillEffectData
    {
        [SerializeField] private ProjectileBehavior projectileBehavior;
        [SerializeField] private float duration;
        public override void ApplyEffect(BaseCharacter character)
        {
            character.AttackingActor.Weapon.AddExtraProjectileBehavior(projectileBehavior);
        }

        public override void RemoveEffect(BaseCharacter character)
        {
            character.AttackingActor.Weapon.RemoveProjectileBehavior(projectileBehavior);
        }
    }
}