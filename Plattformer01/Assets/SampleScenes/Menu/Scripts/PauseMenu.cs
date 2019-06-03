using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Toggle m_MenuToggle;
	private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused;


    void Awake()
    {
        m_MenuToggle = GetComponent<Toggle>();
	}


    private void MenuOn ()
    {
        Debug.Log("this is pause");
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        m_Paused = true;
    }


    public void MenuOff ()
    {
        Debug.Log("normal");
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        m_Paused = false;
    }


    public void OnMenuStatusChange ()
    {
        Debug.Log("normal1" + m_MenuToggle.isOn + m_Paused);

        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
        }
    }


#if !MOBILE_INPUT
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
            if(m_Paused == true)
            {
                m_MenuToggle.isOn = false;
                Debug.Log(m_MenuToggle.isOn);
            }
            else
            {
                m_MenuToggle.isOn = true;
            }
            Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
            OnMenuStatusChange();
        }
	}
#endif

}
