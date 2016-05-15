using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class PauseManager : MonoBehaviour {
	//Audio source for when music is paused and unpaused
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	
	Canvas canvas;
	
	void Start()
	{
        //get canvas for display
		canvas = GetComponent<Canvas>();
	}

    public void OnPressed()
    {
        //Open menu when the function is called
        canvas.enabled = !canvas.enabled;
        Pause();
    }
	
	public void Pause()
	{
        //pause game
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();
		
	}

	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}
		
		else
			
		{
			unpaused.TransitionTo(.01f);
		}
	}
	
	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
