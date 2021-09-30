namespace FirstCourse.View
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class Popup : MonoBehaviour
    {
        [SerializeField]
        protected List<Model.AnimationsDatas> openAnimations = new List<Model.AnimationsDatas>();

        [SerializeField]
        protected List<Model.AnimationsDatas> closeAnimations = new List<Model.AnimationsDatas>();

        [SerializeField]
        private Image backgroundImage = null;

        [SerializeField]
        protected UnityEvent onFinishAnimations = new UnityEvent();

        private bool isOpen = false;

        protected RectTransform rectTransform = null;

        protected List<Tween> tweens = new List<Tween>();

        protected bool isAnimated = false;

        public RectTransform RectTransform
        {
            get
            {
                if (this.rectTransform == null)
                {
                    this.rectTransform = this.GetComponent<RectTransform>();
                }

                return this.rectTransform;
            }
        }

        public void OpenOrClose()
        {
            if (this.isOpen)
            {
                this.PlayAnimations(this.closeAnimations);
            }
            else
            {
                this.PlayAnimations(this.openAnimations);
            }
        }

        public virtual void PlayAnimations(List<Model.AnimationsDatas> animations)
        {
            if (this.isAnimated)
            {
                return;
            }

            for (int i = 0; i < this.tweens.Count; i++)
            {
                this.tweens[i]?.Kill();
            }

            this.tweens.Clear();

            Tween tween;

            for (int i = 0; i < animations.Count; i++)
            {
                Model.AnimationsDatas animationsDatas = animations[i];

                switch (animationsDatas.AnimationType)
                {
                    case Model.AnimationType.Scale:
                        tween = this.RectTransform.DOScale(animationsDatas.TargetScale, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                        this.tweens.Add(tween);
                        break;
                    case Model.AnimationType.Move:
                        tween = DOTween.To(() => this.RectTransform.anchoredPosition, x => this.RectTransform.anchoredPosition = x, new Vector2(animationsDatas.TargetPosition.x, animationsDatas.TargetPosition.y), animationsDatas.Duration).SetEase(animationsDatas.Easing);
                        this.tweens.Add(tween);
                        break;
                    case Model.AnimationType.Rotation:
                        tween = this.RectTransform.DORotate(this.RectTransform.localRotation.eulerAngles, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                        this.tweens.Add(tween);
                        break;
                    case Model.AnimationType.Color:
                        if (this.backgroundImage != null)
                        {
                            tween = DOTween.To(() => this.backgroundImage.color, x => this.backgroundImage.color = x, animationsDatas.TargetColor, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                            this.tweens.Add(tween);
                        }
                        break;
                    case Model.AnimationType.Height:
                        tween = DOTween.To(() => this.RectTransform.sizeDelta.y, y => this.RectTransform.sizeDelta = new Vector2(this.RectTransform.sizeDelta.x, y), animationsDatas.TargetHeight, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                        this.tweens.Add(tween);
                        break;
                    case Model.AnimationType.Width:
                        tween = DOTween.To(() => this.RectTransform.sizeDelta.x, x => this.RectTransform.sizeDelta = new Vector2(x, this.RectTransform.sizeDelta.y), animationsDatas.TargetWidth, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                        this.tweens.Add(tween);
                        break;
                }
            }

            this.isAnimated = animations.Count > 0;
        }

        protected virtual void Update()
        {
            if (this.tweens.Count != 0)
            {
                for (int i = this.tweens.Count - 1; i >= 0; i--)
                {
                    if (!this.tweens[i].active)
                    {
                        this.tweens.RemoveAt(i);
                    }
                }

                if (this.tweens.Count == 0
                    && this.isAnimated)
                {
                    this.isAnimated = false;

                    this.onFinishAnimations?.Invoke();

                    this.isOpen = !this.isOpen;
                }
            }
        }
    }
}