using System;
using CCG.Core.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CCG.Views.Card
{
    [RequireComponent(typeof(RectTransform))]
    public class CardView : ViewBehaviour<ICardViewModel>, IPointerDownHandler, ICardView
    {

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _manaText;
        [SerializeField] private RawImage _icon;
        
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
            SubscribeForPropertyChange<int>("Health", SetHealth);
            SetHealth(ViewModel.Health);
            
            SubscribeForPropertyChange<int>("Mana", SetMana);
            SetMana(ViewModel.Mana);
            
            SubscribeForPropertyChange<int>("Attack", SetAttack);
            SetAttack(ViewModel.Attack);
            
            SubscribeForPropertyChange<string>("Description", SetDescription);
            SetDescription(ViewModel.Description);
            
            SubscribeForPropertyChange<string>("Title", SetTitle);
            SetTitle(ViewModel.Title);
            
            SubscribeForPropertyChange<Texture2D>("Icon", SetIcon);
            SetIcon(ViewModel.Icon);
        }

        private void Update()
        {
            ChangePosition(Time.deltaTime); 
            ChangeRotation(Time.deltaTime);
        }

        private void ChangePosition(float deltaTime)
        {
            var cachedTransform = transform;
            var position = cachedTransform.position;
            var goal = new Vector3(ViewModel.Position.x, ViewModel.Position.y, 0f);
            var direction = 
                (goal - position).normalized;
            var delta = direction * _moveSpeed * deltaTime;
            if ((position - goal).magnitude < delta.magnitude)
            {
                cachedTransform.position = goal;
            }
            else
            {
                cachedTransform.position = position + delta;
            }
        }

        private void ChangeRotation(float deltaTime)
        {
            var cachedTransform = transform;
            var rotation = cachedTransform.eulerAngles.z;
            if (rotation > 180f)
                rotation -= 360f;
            var speedSign = Mathf.Sign(ViewModel.Rotation - rotation);
            var delta = _rotationSpeed * speedSign * deltaTime;
            if (Math.Abs(rotation - ViewModel.Rotation) < Mathf.Abs(delta))
            {
                cachedTransform.eulerAngles = new Vector3(0f, 0f, ViewModel.Rotation);
            }
            else
            {
                cachedTransform.eulerAngles = new Vector3(0f, 0f, rotation + delta);
            }
        }

        private void SetHealth(int health)
        {
            _healthText.text = health.ToString();
        }

        private void SetAttack(int attack)
        {
            _attackText.text = attack.ToString();
        }

        private void SetMana(int mana)
        {
            _manaText.text = mana.ToString();
        }

        private void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void SetTitle(string title)
        {
            _titleText.text = title;
        }

        private void SetIcon(Texture2D icon)
        {
            _icon.texture = icon;
        }
        
        public void OnEnterToBoard()
        {
            ViewModel.OnCardEnterBoard();
        }

        public void OnExitFromBoard()
        {
            ViewModel.OnCardExitBoard();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ViewModel.OnMouseClickDown();
        }
    }
}