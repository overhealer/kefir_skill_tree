using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace SkillTree.Scripts
{
    public class SkillTreeView : MonoBehaviour
    {
        public SkillNodeView[] SkillNodeViews;

        [SerializeField] private TMP_Text _skillPointsText;
        [SerializeField] private TMP_Text _chosenSkillName;
        [SerializeField] private TMP_Text _chosenSkillCost;
        [SerializeField] private TMP_Text _chosenSkillStatus;

        [SerializeField] private Button _learnButton;
        [SerializeField] private Button _unlearnButton;

        public void SetupButtonsOnChooseSkill(bool canLearn, bool enoughPoints, bool isLearned, bool isChildsLearned)
        {
            _learnButton.interactable = canLearn && enoughPoints;
            _unlearnButton.interactable = isLearned && !isChildsLearned;
        }

        public void UpdateSkillInfo(string name, int cost)
        {
            _chosenSkillName.text = name;
            _chosenSkillCost.text = $"{cost} POINT TO LEARN";
        }

        public void UpdateSkillStatus(bool isLearned)
        {
            _chosenSkillStatus.text = isLearned ? "LEARNED" : "NOT LEARNED";
        }

        public void UpdateSkillPointsText(int skillPoints)
        {
            _skillPointsText.text = $"SKILL POINTS: {skillPoints}";
        }

#if UNITY_EDITOR
        [ContextMenu("Set Skill Node Views")]
        public void SetSkillNodeViews()
        {
            SkillNodeViews = GetComponentsInChildren<SkillNodeView>();
            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
#endif
    }
}