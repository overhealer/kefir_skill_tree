using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Scripts
{
    public class SkillNodePresenter
    {
        public Action<int> OnSkillUnlearn;
        public Action<int> OnSkillLearn;

        public int LearnCost { get { return _model.LearnCost; } }
        public string SkillName { get { return _model.SkillName; } }
        public bool IsLearned { get { return _model.IsLearned; } }
        public bool IsCanBeLearned 
        { 
            get
            {
                if (IsLearned)
                    return false;

                for (int i = 0; i < _model.ParentPresenters.Length; i++)
                {
                    if(_model.ParentPresenters[i].IsLearned)
                    {
                        return true;
                    }
                }
                return false;
            } 
        }

        public bool IsChildsLearned
        {
            get
            {
                for (int i = 0; i < _model.ChildPresenters.Length; i++)
                {
                    if (_model.ChildPresenters[i].IsLearned)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private SkillNodeView _view;
        private SkillNodeModel _model;

        public void Init(SkillNodeView view, SkillNodeModel model)
        {
            _view = view;
            _model = model;

            //_view.Presenter = this;
            _view.SetIcon();
            _view.SetIconLearnState(_view.IsBaseSkill ? true : model.IsLearned);
            _view.SetSkillName(_model.SkillName);
            if (_view.IsBaseSkill)
            {
                LearnSkill();
            }
        }

        public void SetConnections(SkillNodeView[] childViews, SkillNodeView[] parentViews)
        {
            _model.SetConnections(childViews, parentViews);
        }

        public void LearnSkill()
        {
            _model.IsLearned = true;
            _view.SetIconLearnState(_model.IsLearned);
            OnSkillLearn?.Invoke(-_model.LearnCost);
        }

        public void UnlearnSkill()
        {
            if (_model.IsUnlearnable)
                return;

            _model.IsLearned = false;
            _view.SetIconLearnState(_model.IsLearned);
            OnSkillUnlearn?.Invoke(_model.LearnCost);
        }

        public void UnlearnSkillRecursively()
        {
            UnlearnSkill();
            for (int i = 0; i < _model.ParentPresenters.Length; i++)
            {
                if(_model.ParentPresenters[i].IsLearned)
                {
                    _model.ParentPresenters[i].UnlearnSkillRecursively();
                }
            }
        }
    }
}