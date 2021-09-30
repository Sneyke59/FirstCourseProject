namespace Framework.MonoBehaviours
{
    using DataClasses;
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using static Framework.DataClasses.MovementsOnScreenAnimationsDatas;

#if ODIN_INSPECTOR
    public class UIMonobehaviour : Sirenix.OdinInspector.SerializedMonoBehaviour
#else
    public class UIMonobehaviour : MonoBehaviour
#endif
    {
        [SerializeField]
        protected Canvas canvas = null;

        [SerializeField, FoldoutGroup("Animation Datas")]
        protected MovementsOnScreenAnimationsDatas movementsOnScreenAnimationsDatas = new MovementsOnScreenAnimationsDatas();

        protected RectTransform rectTransform = null;

        protected bool initialized = false;

        public Canvas Canvas => this.canvas;

        public RectTransform RectTransform => this.rectTransform;

        public void HideTo(E_MovementDirection movementDirection, bool instant = false)
        {
            Tween tween = this.HideTo(this.movementsOnScreenAnimationsDatas.ComputeHiddenPosition(movementDirection));

            if (instant)
            {
                tween.Complete();
            }
        }

        [ContextMenu("Display")]
        public void Display(bool instant = false)
        {
            Tween tween = this.RectTransform.DOMove(this.movementsOnScreenAnimationsDatas.DisplayedPosition, this.movementsOnScreenAnimationsDatas.Duration).SetEase(this.movementsOnScreenAnimationsDatas.Easing);

            if (instant)
            {
                tween.Complete();
            }
        }

        private Tween HideTo(Vector3 position)
        {
            return this.rectTransform.DOMove(position, this.movementsOnScreenAnimationsDatas.Duration).SetEase(this.movementsOnScreenAnimationsDatas.Easing);
        }

        [ContextMenu("Hide To Top")]
        private void HideToTop()
        {
            this.HideTo(E_MovementDirection.Top);
        }

        [ContextMenu("Hide To Bottom")]
        private void HideToBottom()
        {
            this.HideTo(E_MovementDirection.Bottom);
        }

        [ContextMenu("Hide To Right")]
        private void HideToRight()
        {
            this.HideTo(E_MovementDirection.Right);
        }

        [ContextMenu("Hide To Left")]
        private void HideToLeft()
        {
            this.HideTo(E_MovementDirection.Left);
        }

        [ContextMenu("Hide To Top Right")]
        private void HideToTopRight()
        {
            this.HideTo(E_MovementDirection.Top | E_MovementDirection.Right);
        }

        [ContextMenu("Hide To Top Left")]
        private void HideToTopLeft()
        {
            this.HideTo(E_MovementDirection.Top | E_MovementDirection.Left);
        }

        [ContextMenu("Hide To Bottom Right")]
        private void HideToBottomRight()
        {
            this.HideTo(E_MovementDirection.Bottom | E_MovementDirection.Right);
        }

        [ContextMenu("Hide To Bottom Left")]
        private void HideToBottomLeft()
        {
            this.HideTo(E_MovementDirection.Bottom | E_MovementDirection.Left);
        }

        protected virtual void Awake()
        {
            if (this.initialized)
            {
                return;
            }

            this.InitializeComponents();

            this.initialized = true;
        }

        protected virtual void InitializeComponents()
        {
            this.rectTransform = this.transform as RectTransform;

            this.movementsOnScreenAnimationsDatas.Init(this.rectTransform, this.canvas);
        }

        protected virtual void Start()
        {

        }
    }
}