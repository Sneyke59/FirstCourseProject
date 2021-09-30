namespace FirstCourse.Manager
{
    using UnityEngine;
    using UnityEngine.Events;

    public class UIManager : Framework.Singleton.Manager<UIManager>
    {
        [SerializeField]
        private UnityEvent onSceneOpened = new UnityEvent();

        // Start is called before the first frame update
        void Start()
        {
            this.onSceneOpened?.Invoke();
        }
    }
}