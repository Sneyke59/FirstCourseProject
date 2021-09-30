namespace FirstCourse.Model
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public enum AnimationType
    {
        Scale,
        Move,
        Rotation,
        Color,
        Height,
        Width
    }

    [System.Serializable]
    public class AnimationsDatas
    {
        [SerializeField]
        private AnimationType animationType = AnimationType.Scale;

        [SerializeField, Range(0f, 10f)]
        private float delay = 0f;

        [SerializeField, Range(0f, 10f)]
        private float duration = 0f;

        [SerializeField]
        private Ease easing = Ease.Flash;

        [SerializeField, ShowIf("animationType", AnimationType.Scale)]
        private Vector3 targetScale = Vector3.zero;

        [SerializeField, ShowIf("animationType", AnimationType.Move)]
        private Vector3 targetPosition = Vector3.zero;

        [SerializeField, ShowIf("animationType", AnimationType.Rotation)]
        private Vector3 targetRotation = Vector3.zero;

        [SerializeField, ShowIf("animationType", AnimationType.Color)]
        private Color targetColor = new Color();

        [SerializeField, ShowIf("animationType", AnimationType.Height)]
        private float targetHeight = 0f;

        [SerializeField, ShowIf("animationType", AnimationType.Width)]
        private float targetWidth = 0f;

        public AnimationType AnimationType => this.animationType;

        public float Delay => this.delay;

        public float Duration => this.duration;

        public Ease Easing => this.easing;

        public Vector3 TargetScale => this.targetScale;

        public Vector3 TargetPosition => this.targetPosition;

        public Vector3 TargetRotation => this.targetPosition;

        public Color TargetColor => this.targetColor;

        public float TargetHeight => this.targetHeight;

        public float TargetWidth => this.targetWidth;
    }

    [System.Serializable]
    public class MirorAnimationsDatas
    {
        [SerializeField]
        private AnimationType animationType = AnimationType.Scale;

        [SerializeField, Range(0f, 10f)]
        private float delay = 0f;

        [SerializeField, Range(0f, 10f)]
        private float duration = 0f;

        [SerializeField]
        private Ease easing = Ease.Flash;

        [SerializeField, ShowIf("animationType", AnimationType.Scale)]
        private Vector3 fromScale = Vector3.zero;

        [SerializeField, ShowIf("animationType", AnimationType.Scale)]
        private Vector3 toScale = Vector3.zero;
        

        [SerializeField, ShowIf("animationType", AnimationType.Move)]
        private Vector2 fromPosition = Vector3.zero;

        [SerializeField, ShowIf("animationType", AnimationType.Move)]
        private Vector2 toPosition = Vector3.zero;


        [SerializeField, ShowIf("animationType", AnimationType.Rotation)]
        private Vector3 fromRotation = Vector3.zero;

        [SerializeField, ShowIf("animationType", AnimationType.Rotation)]
        private Vector3 toRotation = Vector3.zero;


        [SerializeField, ShowIf("animationType", AnimationType.Color)]
        private Color fromColor = new Color();

        [SerializeField, ShowIf("animationType", AnimationType.Color)]
        private Color toColor = new Color();


        [SerializeField, ShowIf("animationType", AnimationType.Height)]
        private float fromHeight = 0f;

        [SerializeField, ShowIf("animationType", AnimationType.Height)]
        private float toHeight = 0f;


        [SerializeField, ShowIf("animationType", AnimationType.Width)]
        private float fromWidth = 0f;

        [SerializeField, ShowIf("animationType", AnimationType.Width)]
        private float toWidth = 0f;

        public AnimationType AnimationType => this.animationType;

        public float Delay => this.delay;

        public float Duration => this.duration;

        public Ease Easing => this.easing;

        public Vector3 FromScale => this.fromScale;

        public Vector3 ToScale => this.toScale;

        public Vector2 FromPosition => this.fromPosition;

        public Vector2 ToPosition => this.toPosition;

        public Vector3 FromRotation => this.fromRotation;

        public Vector3 ToRotation => this.toRotation;


        public Color FromColor => this.fromColor;

        public Color ToColor => this.toColor;


        public float FromHeight => this.fromHeight;

        public float ToHeight => this.toHeight;


        public float FromWidth => this.fromWidth;
        public float ToWidth => this.toWidth;
    }
}