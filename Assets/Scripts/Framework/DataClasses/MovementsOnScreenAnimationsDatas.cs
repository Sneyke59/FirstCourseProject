namespace Framework.DataClasses
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System;
    using UnityEngine;

    [System.Serializable]
    public class MovementsOnScreenAnimationsDatas
    {
        [SerializeField, FoldoutGroup("Movement Datas")]
        private float duration = 0.35f;

        [SerializeField, FoldoutGroup("Movement Datas")]
        private Ease easing = Ease.InQuad;

        [SerializeField, ReadOnly]
        private RectTransform rectTransform = null;

        [SerializeField, ReadOnly]
        private RectTransform screenSizeCanvasRectTransform = null;

        [SerializeField, ReadOnly]
        private Vector3 displayedPosition = new Vector3();

        public float Duration => this.duration;

        public Ease Easing => this.easing;

        public Vector3 DisplayedPosition => this.displayedPosition;

        public void Init(RectTransform rectTransform, Canvas canvas)
        {
            this.rectTransform = rectTransform;
            this.screenSizeCanvasRectTransform = canvas.transform as RectTransform;
            this.displayedPosition = rectTransform.position;
        }

        public Vector3 ComputeHiddenPosition(E_MovementDirection movementDirection)
        {
            return (int)movementDirection switch
            {
                (int)E_MovementDirection.Top => new Vector3(this.rectTransform.position.x, this.ComputeHiddenTopPosition(), this.rectTransform.position.z),
                (int)E_MovementDirection.Bottom => new Vector3(this.rectTransform.position.x, this.ComputeHiddenBottomPosition(), this.rectTransform.position.z),
                (int)E_MovementDirection.Right => new Vector3(this.ComputeHiddenRightPosition(), this.rectTransform.position.y, this.rectTransform.position.z),
                (int)E_MovementDirection.Left => new Vector3(this.ComputeHiddenLeftPosition(), this.rectTransform.position.y, this.rectTransform.position.z),
                (int)(E_MovementDirection.Top | E_MovementDirection.Right) => new Vector3(this.ComputeHiddenRightPosition(), this.ComputeHiddenTopPosition(), this.rectTransform.position.z),
                (int)(E_MovementDirection.Top | E_MovementDirection.Left) => new Vector3(this.ComputeHiddenLeftPosition(), this.ComputeHiddenTopPosition(), this.rectTransform.position.z),
                (int)(E_MovementDirection.Bottom | E_MovementDirection.Right) => new Vector3(this.ComputeHiddenRightPosition(), this.ComputeHiddenBottomPosition(), this.rectTransform.position.z),
                (int)(E_MovementDirection.Bottom | E_MovementDirection.Left) => new Vector3(this.ComputeHiddenLeftPosition(), this.ComputeHiddenBottomPosition(), this.rectTransform.position.z),
                _ => new Vector3(this.rectTransform.position.x, this.ComputeHiddenTopPosition(), this.rectTransform.position.z),
            };
        }

        private float ComputeHiddenTopPosition()
        {
            return this.screenSizeCanvasRectTransform.sizeDelta.y + (this.rectTransform.pivot.y * this.rectTransform.sizeDelta.y);
        }

        private float ComputeHiddenBottomPosition()
        {
            return (1f - this.rectTransform.pivot.y) * (0f - this.rectTransform.sizeDelta.y);
        }

        private float ComputeHiddenRightPosition()
        {
            return this.screenSizeCanvasRectTransform.sizeDelta.x + (this.rectTransform.pivot.x * this.rectTransform.sizeDelta.x);
        }

        private float ComputeHiddenLeftPosition()
        {
            return (1f - this.rectTransform.pivot.x) * (0f - this.rectTransform.sizeDelta.x);
        }

        [Flags]
        public enum E_MovementDirection
        {
            None = 0,
            Top = 2,
            Bottom = 4,
            Right = 8,
            Left = 16
        }
    }
}
