using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    // Start is called before the first frame update

    static string micName = "Microphone Array (Realtek(R) Audio)";
    static AudioSource audioSource;

    static float[] samplesData;
    /// <summary>
    /// WAV file header size
    /// </summary>
    const int HEADER_SIZE = 44;

    #region Private Variables

    /// <summary>
    /// Is Recording
    /// </summary>
    public static bool isRecording = false;
    /// <summary>
    /// Recording Time
    /// </summary>
    private float recordingTime = 0f;
    /// <summary>
    /// Recording Time Minute and Seconds
    /// </summary>
    private int minute = 0, second = 0;

    #endregion

    #region Editor Exposed Variables

    /// <summary>
    /// Audio Player Script for Playing Audio Files
    /// </summary>
    [Tooltip("Set max duration of the audio file in seconds")]
    public int timeToRecord = 30;

    #endregion

    void Start()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log(device);
        }

        Debug.LogWarning(Microphone.devices[2]);


        audioSource = GetComponent<AudioSource>();

        isRecording = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SaveRecording("try1");
        }

    }


    public void StartRecording()
    {
        recordingTime = 0f;
        isRecording = true;

        Microphone.End(Microphone.devices[2]);
        audioSource.clip = Microphone.Start(Microphone.devices[2], false, timeToRecord, 44100);
    }

    public void SaveRecording(string fileName = "Audio")
    {
        if (isRecording)
        {

            while (!(Microphone.GetPosition(null) > 0)) { }
            samplesData = new float[audioSource.clip.samples * audioSource.clip.channels];
            audioSource.clip.GetData(samplesData, 0);

            // Trim the silence at the end of the recording
            var samples = samplesData.ToList();
            int recordedSamples = (int)(samplesData.Length * (recordingTime / (float)timeToRecord));

            if (recordedSamples < samplesData.Length - 1)
            {
                samples.RemoveRange(recordedSamples, samplesData.Length - recordedSamples);
                samplesData = samples.ToArray();
            }

            // Create the audio file after removing the silence
            AudioClip audioClip = AudioClip.Create(fileName, samplesData.Length, audioSource.clip.channels, 44100, false);
            audioClip.SetData(samplesData, 0);

            // Assign Current Audio Clip to Audio Player
            audioSource.clip = audioClip;
            audioSource.Play();

            string filePath = Path.Combine(Application.persistentDataPath, fileName + " " + DateTime.UtcNow.ToString("yyyy_MM_dd HH_mm_ss_ffff") + ".wav");

            // Delete the file if it exists.
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            try
            {
                WriteWAVFile(audioClip, filePath);
                Debug.Log("File Saved Successfully at " + filePath);
            }
            catch (DirectoryNotFoundException)
            {
                Debug.LogError("Persistent Data Path not found!");
            }

            isRecording = false;
            Microphone.End(Microphone.devices[2]);
        }

    }

    public static byte[] ConvertWAVtoByteArray(string filePath)
    {
        //Open the stream and read it back.
        byte[] bytes = new byte[audioSource.clip.samples + HEADER_SIZE];
        using (FileStream fs = File.OpenRead(filePath))
        {
            fs.Read(bytes, 0, bytes.Length);
        }
        return bytes;
    }

    // WAV file format from http://soundfile.sapp.org/doc/WaveFormat/
    static void WriteWAVFile(AudioClip clip, string filePath)
    {
        float[] clipData = new float[clip.samples];

        //Create the file.
        using (Stream fs = File.Create(filePath))
        {
            int frequency = clip.frequency;
            int numOfChannels = clip.channels;
            int samples = clip.samples;
            fs.Seek(0, SeekOrigin.Begin);

            //Header

            // Chunk ID
            byte[] riff = Encoding.ASCII.GetBytes("RIFF");
            fs.Write(riff, 0, 4);

            // ChunkSize
            byte[] chunkSize = BitConverter.GetBytes((HEADER_SIZE + clipData.Length) - 8);
            fs.Write(chunkSize, 0, 4);

            // Format
            byte[] wave = Encoding.ASCII.GetBytes("WAVE");
            fs.Write(wave, 0, 4);

            // Subchunk1ID
            byte[] fmt = Encoding.ASCII.GetBytes("fmt ");
            fs.Write(fmt, 0, 4);

            // Subchunk1Size
            byte[] subChunk1 = BitConverter.GetBytes(16);
            fs.Write(subChunk1, 0, 4);

            // AudioFormat
            byte[] audioFormat = BitConverter.GetBytes(1);
            fs.Write(audioFormat, 0, 2);

            // NumChannels
            byte[] numChannels = BitConverter.GetBytes(numOfChannels);
            fs.Write(numChannels, 0, 2);

            // SampleRate
            byte[] sampleRate = BitConverter.GetBytes(frequency);
            fs.Write(sampleRate, 0, 4);

            // ByteRate
            byte[] byteRate = BitConverter.GetBytes(frequency * numOfChannels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
            fs.Write(byteRate, 0, 4);

            // BlockAlign
            ushort blockAlign = (ushort)(numOfChannels * 2);
            fs.Write(BitConverter.GetBytes(blockAlign), 0, 2);

            // BitsPerSample
            ushort bps = 16;
            byte[] bitsPerSample = BitConverter.GetBytes(bps);
            fs.Write(bitsPerSample, 0, 2);

            // Subchunk2ID
            byte[] datastring = Encoding.ASCII.GetBytes("data");
            fs.Write(datastring, 0, 4);

            // Subchunk2Size
            byte[] subChunk2 = BitConverter.GetBytes(samples * numOfChannels * 2);
            fs.Write(subChunk2, 0, 4);

            // Data

            clip.GetData(clipData, 0);
            short[] intData = new short[clipData.Length];
            byte[] bytesData = new byte[clipData.Length * 2];

            int convertionFactor = 32767;

            for (int i = 0; i < clipData.Length; i++)
            {
                intData[i] = (short)(clipData[i] * convertionFactor);
                byte[] byteArr = new byte[2];
                byteArr = BitConverter.GetBytes(intData[i]);
                byteArr.CopyTo(bytesData, i * 2);
            }

            fs.Write(bytesData, 0, bytesData.Length);
        }
    }


}