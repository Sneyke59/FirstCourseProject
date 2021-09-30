namespace FirstCourse.Manager
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneManager : Framework.Singleton.Manager<SceneManager>
    {
        public static void ChangeScene(string name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name, LoadSceneMode.Single);
        }
    }
}