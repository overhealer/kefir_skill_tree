using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Scripts
{
    public class SkillNodeModel
    {
        public bool IsUnlearnable { get; }
        public int LearnCost { get; }
        public string SkillName { get; }

        public bool IsLearned = false;

        public SkillNodePresenter[] ChildPresenters { get; private set;}
        public SkillNodePresenter[] ParentPresenters { get; private set; }


        public SkillNodeModel(bool isUnlearnable, int learnCost, string name)
        {
            IsUnlearnable = isUnlearnable;
            LearnCost = learnCost;
            SkillName = name;
        }

        public void SetConnections(SkillNodeView[] childViews, SkillNodeView[] parentViews)
        {
            ChildPresenters = new SkillNodePresenter[childViews.Length];
            ParentPresenters = new SkillNodePresenter[parentViews.Length];
            for (int i = 0; i < childViews.Length; i++)
            {
                ChildPresenters[i] = childViews[i].Presenter;
            }
            for (int i = 0; i < parentViews.Length; i++)
            {
                ParentPresenters[i] = parentViews[i].Presenter;
            }
        }
    }
}