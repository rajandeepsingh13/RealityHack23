using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VoiceRecording : MonoBehaviour
{
    // Time to trim from beginning to compensate for audio lag
    public static float TIME_TO_SKIP = 0.8f;

    string selectedDevice;
    
    bool micSelected = false;
    private int minFreq, maxFreq;
    private int micFrequency = 16000;

    string filename = "TestAudioFile";

    // Current audio source to record / play
    public static AudioSource audioSource; 
    
    void Awake()
	{
		if (!audioSource) audioSource = GetComponent<AudioSource>();
		if (!audioSource) return;
	}

    private void Start() {
        if (Microphone.devices.Length > 0) {
            selectedDevice = Microphone.devices[0].ToString();
			micSelected = true;
			GetMicCapabilities();
        } 
    }

    bool shouldPlayAudio = false;

    private void Update() {
        /*if (shouldPlayAudio && !audioSource.isPlaying && audioSource.clip.isReadyToPlay)
        {
            Debug.Log("playing after load");
            audioSource.Play();
            shouldPlayAudio = false;
        }
        // Record
        if (OVRInput.GetDown(OVRInput.RawButton.A)) {
            Debug.Log("record");
            ToggleRecordingState();
        }
        // Play
        if (OVRInput.GetDown(OVRInput.RawButton.B)) {
            Debug.Log("play");
            TogglePlayingState();
        }
        // Save
        if (OVRInput.GetDown(OVRInput.RawButton.X)) {
            Debug.Log("running savwav");
            if (File.Exists(Application.persistentDataPath + "/" + filename + ".wav")) {
                Debug.Log("deleting existing save file");
                File.Delete(Application.persistentDataPath + "/" + filename + ".wav");
            }
            SavWav.Save(filename, audioSource.clip);
        }
        // Load
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)) {
            //byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/" + filename + ".wav");
            //audioSource.SetData()
            WWW www = new WWW("file://" + Application.persistentDataPath + "/" + filename + ".wav");
            audioSource.clip = www.GetAudioClip();
            shouldPlayAudio = true;
            //audioSource.Play();
        }*/
    }

    public void ToggleRecordingState() {
        Debug.Log("recording audio");
        if (Microphone.IsRecording(selectedDevice))
            StopMicrophone();
        else if (!Microphone.IsRecording(selectedDevice))
            StartMicrophone();
    }

    public void TogglePlayingState() {
        Debug.Log("playing audio");
        if (audioSource.isPlaying)
            audioSource.Stop();
        else
            audioSource.time = TIME_TO_SKIP;
            audioSource.Play();
    }

    public void StartMicrophone () 
	{
		if (micSelected == false) return;

		audioSource.clip = Microphone.Start(selectedDevice, true, 10, micFrequency);

		while (!(Microphone.GetPosition(selectedDevice) > 0)) {}

		//audioSource.Play();
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
		if (micSelected == false) return;

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
