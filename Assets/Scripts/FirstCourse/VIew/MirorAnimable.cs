namespace FirstCourse.View
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class MirorAnimable : MonoBehaviour
    {
        [SerializeField]
        protected List<Model.MirorAnimationsDatas> mirrorAnimations = new List<Model.MirorAnimationsDatas>();

        protected List<Tween> mirrorTweens = new List<Tween>();

        [SerializeField]
        protected UnityEvent onFinishAnimations = new UnityEvent();

        protected List<Tween> tweens = new List<Tween>();

        protected RectTransform rectTransform = null;
        protected LayoutGroup parentLayoutGroup = null;
        protected Image image = null;
        protected Button button = null;

        protected bool isAnimated = false;

        protected TMPro.TextMeshProUGUI textMeshProUGUI;

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

        public LayoutGroup ParentLayoutGroup
        {
            get
            {
                if (this.parentLayoutGroup == null)
                {
                    this.parentLayoutGroup = this.GetComponentInParent<LayoutGroup>();
                }

                return this.parentLayoutGroup;
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

        public TMPro.TextMeshProUGUI TextMeshProUGUI
        {
            get
            {
                if (this.textMeshProUGUI == null)
                {
                    this.textMeshProUGUI = this.GetComponent<TMPro.TextMeshProUGUI>();
                }

                return this.textMeshProUGUI;
            }
        }

        public virtual void PlayAnimations()
        {
            Tween tween;
            Tween mirrortween;

            for (int i = 0; i < this.tweens.Count; i++)
            {
                this.tweens[i]?.Kill();
            }

            for (int i = 0; i < this.mirrorTweens.Count; i++)
            {
                this.mirrorTweens[i]?.Kill();
            }

            this.tweens.Clear();
            this.mirrorTweens.Clear();

            for (int i = 0; i < this.mirrorAnimations.Count; i++)
            {
                Model.MirorAnimationsDatas animationsDatas = this.mirrorAnimations[i];

                switch (animationsDatas.AnimationType)
                {
                    case Model.AnimationType.Scale:
                        tween = this.RectTransform.DOScale(animationsDatas.ToScale, animationsDatas.Duration).SetEase(animationsDatas.Easing).SetDelay(animationsDatas.Delay);

                        mirrortween = this.RectTransform.DOScale(animationsDatas.FromScale, animationsDatas.Duration).SetEase(animationsDatas.Easing);
                        mirrortween.Pause();

                        this.tweens.Add(tween);
                        this.mirrorTweens.Add(mirrortween);
                        break;
                    case Model.AnimationType.Move:
                        tween = DOTween.To(() => this.RectTransform.anchoredPosition, x => this.RectTransform.anchoredPosition = x, animationsDatas.ToPosition, animationsDatas.Duration).SetEase(animationsDatas.Easing);

                        mirrortween = DOTween.To(() => this.RectTransform.anchoredPosition, x => this.RectTransform.anchoredPosition = x, animationsDatas.FromPosition, animationsDatas.Duration).SetEase(animationsDatas.Easing);
                        mirrortween.Pause();

                        this.tweens.Add(tween);
                        this.mirrorTweens.Add(mirrortween);
                        break;
                    case Model.AnimationType.Rotation:
                        tween = this.RectTransform.DORotate(animationsDatas.ToRotation, animationsDatas.Duration).SetEase(animationsDatas.Easing);

                        mirrortween = this.RectTransform.DORotate(animationsDatas.FromRotation, animationsDatas.Duration).SetEase(animationsDatas.Easing);
                        mirrortween.Pause();

                        this.tweens.Add(tween);
                        this.mirrorTweens.Add(mirrortween);
                        break;
                    case Model.AnimationType.Color:
                        if (this.Image != null)
                        {
                            tween = DOTween.To(() => this.Image.color, x => this.Image.color = x, animationsDatas.ToColor, animationsDatas.Duration).SetEase(animationsDatas.Easing);

                            mirrortween = DOTween.To(() => this.Image.color, x => this.Image.color = x, animationsDatas.FromColor, animationsDatas.Duration).SetEase(animationsDatas.Easing);
                            mirrortween.Pause();

                            this.tweens.Add(tween);
                            this.mirrorTweens.Add(mirrortween);
                        }

                        if (this.TextMeshProUGUI != null)
                        {
                            tween = DOTween.To(() => this.TextMeshProUGUI.color, x => this.TextMeshProUGUI.color = x, animationsDatas.ToColor, animationsDatas.Duration).SetEase(animationsDatas.Easing);

                            mirrortween = DOTween.To(() => this.TextMeshProUGUI.color, x => this.TextMeshProUGUI.color = x, animationsDatas.FromColor, animationsDatas.Duration).SetEase(animationsDatas.Easing);
                            mirrortween.Pause();

                            this.tweens.Add(tween);
                            this.mirrorTweens.Add(mirrortween);
                        }
                        break;
                    case Model.AnimationType.Height:
                        tween = DOTween.To(() => this.RectTransform.sizeDelta.y, y => this.RectTransform.sizeDelta = new Vector2(this.RectTransform.sizeDelta.x, y), animationsDatas.ToHeight, animationsDatas.Duration).SetEase(animationsDatas.Easing);

                        mirrortween = DOTween.To(() => this.RectTransform.sizeDelta.y, y => this.RectTransform.sizeDelta = new Vector2(this.RectTransform.sizeDelta.x, y), animationsDatas.FromHeight, animationsDatas.Duration).SetEase(animationsDatas.Easing);
                        mirrortween.Pause();

                        this.tweens.Add(tween);
                        this.mirrorTweens.Add(mirrortween);
                        break;
                    case Model.AnimationType.Width:
                        tween = DOTween.To(() => this.RectTransform.sizeDelta.x, x => this.RectTransform.sizeDelta = new Vector2(x, this.RectTransform.sizeDelta.y), animationsDatas.ToWidth, animationsDatas.Duration).SetEase(animationsDatas.Easing);

                        mirrortween = DOTween.To(() => this.RectTransform.sizeDelta.x, x => this.RectTransform.sizeDelta = new Vector2(x, this.RectTransform.sizeDelta.y), animationsDatas.FromWidth, animationsDatas.Duration).SetEase(animationsDatas.Easing);
                        mirrortween.Pause();
                        this.tweens.Add(tween);
                        this.mirrorTweens.Add(mirrortween);
                        break;
                }
            }

            this.isAnimated = this.mirrorAnimations.Count > 0;
        }

        protected void Update()
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
                    for (int i = 0; i < this.mirrorTweens.Count; i++)
                    {
                        this.mirrorTweens[i].Play();
                    }
                }
            }
            else if (this.mirrorTweens.Count != 0)
            {
                for (int i = this.mirrorTweens.Count - 1; i >= 0; i--)
                {
                    if (!this.mirrorTweens[i].active)
                    {
                        this.mirrorTweens.RemoveAt(i);
                    }
                }

                if (this.mirrorTweens.Count == 0
                    && this.isAnimated)
                {
                    this.isAnimated = false;

                    this.onFinishAnimations?.Invoke();

                    if (this.ParentLayoutGroup != null)
                    {
                        this.ParentLayoutGroup.enabled = false;
                        this.ParentLayoutGroup.enabled = true;
                    }
                }
            }
        }
    }
}