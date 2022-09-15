using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkillTree.Scripts
{
    public class SkillNodeView : MonoBehaviour, IPointerDownHandler
    {
        public event Action<SkillNodePresenter> OnClick;

        public bool IsBaseSkill;
        public SkillNodeView[] ParentConnections;
        public SkillNodeView[] ChildConnections;
        public SkillDataSO SkillData;

        public SkillNodePresenter Presenter;

        [SerializeField] private Image _skillIconImage;
        [SerializeField] private Color _skillColorUnlearnState;
        [SerializeField] private TMP_Text _skillNameText;

        public void SetIcon()
        {
            _skillIconImage.sprite = SkillData.SkillIcon;
        }

        public void SetIconLearnState(bool isLearned)
        {
            _skillIconImage.color = isLearned ? Color.white : _skillColorUnlearnState;
        }

        public void SetSkillName(string name)
        {
            _skillNameText.text = name;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke(Presenter);
        }
    }
}