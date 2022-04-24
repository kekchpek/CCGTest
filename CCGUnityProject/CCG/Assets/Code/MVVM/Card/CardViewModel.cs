using System;
using CCG.Core.Input;
using CCG.Core.MVVM;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace CCG.MVVM.Card
{
    public class CardViewModel : ViewModel, ICardViewModel
    {
        private readonly IInputController _inputController;

        private Vector2 _position;
        private int _health;
        private int _attack;
        private int _mana;
        private string _description;
        private string _title;
        private Texture2D _icon;
        private bool _isSelected;
        private float _rotation;
        private bool _isOverBoard;
        
        private Vector2 _positionInHand;
        private float _rotationInHand;
        private bool _isPlayed;

        public Vector2 Position
        {
            get => _position;
            set => SetAndRaiseIfChanged(nameof(Position), value, ref _position);
        }
        
        public float Rotation
        {
            get => _rotation;
            set => SetAndRaiseIfChanged(nameof(Rotation), value, ref _rotation);
        }
        
        public int Health
        {
            get => _health;
            set => SetAndRaiseIfChanged(nameof(Health), value, ref _health);
        }
        
        public int Attack
        {
            get => _attack;
            set => SetAndRaiseIfChanged(nameof(Attack), value, ref _attack);
        }
        
        public int Mana
        {
            get => _mana;
            set => SetAndRaiseIfChanged(nameof(Mana), value, ref _mana);
        }
        
        public string Description
        {
            get => _description;
            set => SetAndRaiseIfChanged(nameof(Description), value, ref _description);
        }
        
        public string Title
        {
            get => _title;
            set => SetAndRaiseIfChanged(nameof(Title), value, ref _title);
        }
        
        public Texture2D Icon
        {
            get => _icon;
            set => SetAndRaiseIfChanged(nameof(Icon), value, ref _icon);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => SetAndRaiseIfChanged(nameof(IsSelected), value, ref _isSelected);
        }

        public bool IsOverBoard
        {
            get => _isOverBoard;
            set => SetAndRaiseIfChanged(nameof(IsOverBoard), value, ref _isOverBoard);
        }

        public event Action Played;

        public CardViewModel(IInputController inputController)
        {
            _inputController = inputController;
        }

        public void OnMouseClickDown()
        {
            if (_isPlayed)
                return;
            _inputController.MousePositionChanged += OnMousePositionChanged;
            _inputController.MouseUp += MouseUp;
            IsSelected = true;
            Rotation = 0;
            Position = _inputController.MousePosition;
        }

        public void OnCardEnterBoard()
        {
            IsOverBoard = true;
        }

        public void OnCardExitBoard()
        {
            IsOverBoard = false;
        }

        public void SetPositionAndRotationInHand(Vector2 position, float rotation)
        {
            _positionInHand = position;
            _rotationInHand = rotation;
            if (!_isSelected)
            {
                Position = _positionInHand;
                Rotation = _rotationInHand;
            }
        }

        private void OnMousePositionChanged(Vector2 pos)
        {
            Position = pos;
        }

        private void MouseUp()
        {
            _inputController.MousePositionChanged -= OnMousePositionChanged;
            _inputController.MouseUp -= MouseUp;
            IsSelected = false;
            if (_isOverBoard)
            {
                _isPlayed = true;
                Played?.Invoke();
            }
            else
            {
                Position = _positionInHand;
                Rotation = _rotationInHand;
            }
        }
    }
}