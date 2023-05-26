using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public delegate void PauseEvent();
    public event PauseEvent OnPauseChange;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneVariables.paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            if(!SceneVariables.paused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }

            PauseChange();
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        SceneVariables.paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Unpause()
    {
        Time.timeScale = 1;
        SceneVariables.paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseChange()
    {
        if (OnPauseChange != null) OnPauseChange();
    }

    public void Return()
    {
        Unpause();
        PauseChange();
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
