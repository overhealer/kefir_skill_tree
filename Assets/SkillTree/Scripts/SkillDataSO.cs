using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Scripts
{
    [CreateAssetMenu(menuName = "Skills/Skill Data")]
    public class SkillDataSO : ScriptableObject
    {
        public Sprite SkillIcon;
        public int LearnCost = 1;
        public string Name;
    }
}