using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceRecording : MonoBehaviour
{
    string selectedDevice;
    AudioSource audioSource;
    bool micSelected = false;
    private int minFreq, maxFreq;
    private int micFrequency = 16000;
    
    void Awake()
	{
		if (!audioSource) audioSource = GetComponent<AudioSource>();
		if (!audioSource) return;
	}

    private void Start() {
        audioSource.loop = true;
		audioSource.mute = false;

        if (Microphone.devices.Length > 0) {
            selectedDevice = Microphone.devices[0].ToString();
			micSelected = true;
			GetMicCapabilities();
        } 
    }

    private void Update() {
        if (OVRInput.GetDown(OVRInput.Button.One)) {
            if (Microphone.IsRecording(selectedDevice))
                StopMicrophone();
            else if (!Microphone.IsRecording(selectedDevice))
                StartMicrophone();
        }
        if (OVRInput.GetDown(OVRInput.Button.Two)) {
            if (audioSource.isPlaying)
                audioSource.Stop();
            else
                audioSource.Play();
        }
    }

    public void StartMicrophone () 
	{
		if (micSelected == false) return;

		audioSource.clip = Microphone.Start(selectedDevice, true, 1, micFrequency);

		while (!(Microphone.GetPosition(selectedDevice) > 0)){}

		audioSource.Play();
    }

    public void StopMicrophone () 
	{
		if(micSelected == false) return;

		if ((audioSource != null) && (audioSource.clip != null) && (audioSource.clip.name == "Microphone"))
			audioSource.Stop();

		Microphone.End(selectedDevice);
	}  

    public void GetMicCapabilities () 
	{
		if(micSelected == false) return;

		Microphone.GetDeviceCaps(selectedDevice, out minFreq, out maxFreq);

		if ( minFreq == 0 && maxFreq == 0 )
		{
			Debug.LogWarning ("GetMicCaps warning:: min and max frequencies are 0");
			minFreq = 44100;
			maxFreq = 44100;
		}
	
		if (micFrequency > maxFreq)
			micFrequency = maxFreq;
	}  
}
