namespace FirstCourse.View
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class Animable : MonoBehaviour
    {
        [SerializeField]
        protected List<Model.AnimationsDatas> animations = new List<Model.AnimationsDatas>();

        [SerializeField]
        protected UnityEvent onFinishAnimations = new UnityEvent();

        protected List<Tween> tweens = new List<Tween>();

        protected RectTransform rectTransform = null;
        protected Image image = null;
        protected Button button = null;

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

        public Image Image
        {
            get
            {
                if (this.image == null)
                {
                    this.image = this.GetComponent<Image>();
                }

                return this.image;
            }
        }

        public virtual void PlayAnimations()
        {
            this.tweens.Clear();

            Tween tween;

            for (int i = 0; i < this.animations.Count; i++)
            {
                Model.AnimationsDatas animationsDatas = this.animations[i];

                switch (animationsDatas.AnimationType)
                {
                    case Model.AnimationType.Scale:
                        tween = this.RectTransform.DOScale(animationsDatas.TargetScale, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                        this.tweens.Add(tween);
                        break;
                    case Model.AnimationType.Move:
                        tween = this.RectTransform.DOMove(animationsDatas.TargetPosition, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                        this.tweens.Add(tween);
                        break;
                    case Model.AnimationType.Rotation:
                        tween = this.RectTransform.DORotate(this.RectTransform.localRotation.eulerAngles, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
                        this.tweens.Add(tween);
                        break;
                    case Model.AnimationType.Color:
                        if (this.Image != null)
                        {
                            tween = DOTween.To(() => this.Image.color, x => this.Image.color = x, animationsDatas.TargetColor, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);
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

            this.isAnimated = this.animations.Count > 0;
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
                }
            }
        }

        private void OnDisable()
        {
            this.onFinishAnimations?.RemoveAllListeners();
        }
    }
}