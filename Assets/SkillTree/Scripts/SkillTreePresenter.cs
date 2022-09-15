using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Scripts
{
    public class SkillTreePresenter : MonoBehaviour
    {
        [SerializeField] private SkillTreeView _skillTreeView;
        private SkillTreeModel _skillTreeModel;

        private void Awake()
        {
            SetupSkillTree();
            _skillTreeView.UpdateSkillPointsText(_skillTreeModel.SkillPoints);
        }

        private void SetupSkillTree()
        {
            _skillTreeModel = new SkillTreeModel();

            SkillNodeView[] skillNodeViews = GetComponentsInChildren<SkillNodeView>();
            _skillTreeModel.SkillPresenters = new SkillNodePresenter[skillNodeViews.Length];
            for (int i = 0; i < _skillTreeModel.SkillPresenters.Length; i++)
            {
                _skillTreeModel.SkillPresenters[i] = new SkillNodePresenter();
                skillNodeViews[i].Presenter = _skillTreeModel.SkillPresenters[i];
                _skillTreeModel.SkillPresenters[i].Init(skillNodeViews[i], new SkillNodeModel(
                    skillNodeViews[i].IsBaseSkill,
                    skillNodeViews[i].SkillData.LearnCost,
                    skillNodeViews[i].SkillData.Name));
                _skillTreeModel.SkillPresenters[i].OnSkillLearn += AddSkillPoint;
                _skillTreeModel.SkillPresenters[i].OnSkillUnlearn += AddSkillPoint;
                skillNodeViews[i].OnClick += ChooseSkill;
            }
            for (int i = 0; i < _skillTreeModel.SkillPresenters.Length; i++)
            {
                _skillTreeModel.SkillPresenters[i].SetConnections(skillNodeViews[i].ChildConnections, skillNodeViews[i].ParentConnections);
            }
        }

        public void AddSkillPoint(int points)
        {
            _skillTreeModel.SkillPoints += points;
            _skillTreeView.UpdateSkillPointsText(_skillTreeModel.SkillPoints);

            if(_skillTreeModel.ChoosenSkill != null)
            {
                _skillTreeView.SetupButtonsOnChooseSkill(_skillTreeModel.ChoosenSkill.IsCanBeLearned,
                _skillTreeModel.SkillPoints >= _skillTreeModel.ChoosenSkill.LearnCost,
                _skillTreeModel.ChoosenSkill.IsLearned,
                _skillTreeModel.ChoosenSkill.IsChildsLearned);
            }
        }

        public void LearnSkill()
        {
            if (_skillTreeModel.ChoosenSkill == null || !_skillTreeModel.ChoosenSkill.IsCanBeLearned)
                return;

            _skillTreeModel.ChoosenSkill.LearnSkill();
            _skillTreeView.SetupButtonsOnChooseSkill(_skillTreeModel.ChoosenSkill.IsCanBeLearned,
                _skillTreeModel.SkillPoints >= _skillTreeModel.ChoosenSkill.LearnCost,
                _skillTreeModel.ChoosenSkill.IsLearned,
                _skillTreeModel.ChoosenSkill.IsChildsLearned);
            _skillTreeView.UpdateSkillStatus(_skillTreeModel.ChoosenSkill.IsLearned);
        }

        public void UnlearnSkill()
        {
            if (_skillTreeModel.ChoosenSkill == null || !_skillTreeModel.ChoosenSkill.IsLearned)
                return;

            _skillTreeModel.ChoosenSkill.UnlearnSkill();
            _skillTreeView.SetupButtonsOnChooseSkill(_skillTreeModel.ChoosenSkill.IsCanBeLearned,
                _skillTreeModel.SkillPoints >= _skillTreeModel.ChoosenSkill.LearnCost,
                _skillTreeModel.ChoosenSkill.IsLearned,
                _skillTreeModel.ChoosenSkill.IsChildsLearned);
            _skillTreeView.UpdateSkillStatus(_skillTreeModel.ChoosenSkill.IsLearned);
        }

        public void UnlearnAllSkills()
        {
            for (int i = 0; i < _skillTreeModel.SkillPresenters.Length; i++)
            {
                if (!_skillTreeModel.SkillPresenters[i].IsChildsLearned && _skillTreeModel.SkillPresenters[i].IsLearned)
                {
                    _skillTreeModel.SkillPresenters[i].UnlearnSkillRecursively();
                }
            }
        }

        private void ChooseSkill(SkillNodePresenter presenter)
        {
            _skillTreeModel.ChoosenSkill = presenter;
            _skillTreeView.UpdateSkillInfo(_skillTreeModel.ChoosenSkill.SkillName, _skillTreeModel.ChoosenSkill.LearnCost);
            _skillTreeView.UpdateSkillStatus(_skillTreeModel.ChoosenSkill.IsLearned);
            _skillTreeView.SetupButtonsOnChooseSkill(_skillTreeModel.ChoosenSkill.IsCanBeLearned, 
                _skillTreeModel.SkillPoints >= _skillTreeModel.ChoosenSkill.LearnCost, 
                _skillTreeModel.ChoosenSkill.IsLearned,
                _skillTreeModel.ChoosenSkill.IsChildsLearned);
        }
    }
}