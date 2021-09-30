namespace FirstCourse.View
{
    using UnityEngine;
    using UnityEngine.UI;

    public class StatusBar : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;

        [SerializeField]
        private TMPro.TextMeshProUGUI text = null;

        [SerializeField]
        private float moneyGainedOnClick = 50f;

        private float amount = 0;

        private RectTransform rectTransform = null;

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

        private void Start()
        {
            this.button.onClick.AddListener(() =>
            {
                this.amount += this.moneyGainedOnClick;
                this.text.text = this.amount.ToString();
                LayoutRebuilder.ForceRebuildLayoutImmediate(this.RectTransform);
            });
        }

    }
}